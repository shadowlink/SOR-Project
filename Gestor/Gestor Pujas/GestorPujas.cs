using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Gestor_Pujas
{
    class GestorPujas
    {
        public List<Venta> listaVentas = new List<Venta>();

        //Cadena de mySql
        private static string connString = "Server=127.0.0.1;Port=3306;Database=sordb;Uid=root;password=lizerg";

        //ActiveMQ Queues
        Uri connecturi = new Uri("activemq:tcp://localhost:61616");
        public IConnectionFactory factory;




        public GestorPujas()
        {
            factory = new NMSConnectionFactory(connecturi);

        }

        //Metodo del gestor de pujas que recibe y procesa las pujas que le llegan a traves de la cola Pujas
        public void escucharPujas()
        {
            using (IConnection connection = factory.CreateConnection())
            using (ISession session = connection.CreateSession())
            {
                IDestination destination = SessionUtil.GetDestination(session, "queue://Pujas");
                using (IMessageConsumer consumer = session.CreateConsumer(destination))
                {
                    connection.Start();
                    while (true)
                    {
                        //Console.WriteLine("ESCUCHATASM: " + listaVentas.Count);
                        ITextMessage message = consumer.Receive() as ITextMessage;
                        if (message != null)
                        {
                            procesarMensaje(message.Text);
                        }
                    }
                }
            }
        }

        //Procesa el json de la puja
        public void procesarMensaje(string m)
        {
            Puja p = new Puja();
            string jsonPuja = m;
            var javaScriptSerializer = new JavaScriptSerializer();

            p = javaScriptSerializer.Deserialize<Puja>(jsonPuja);

         
            //Comprobar si la venta no ha terminado
            if (!isVentaTerminada(p.ventaId))
            {
                //Comprobar si supera la puja mas alta
                if (p.cantidad > getVenta(p.ventaId).precio)
                {
                    //Establecer nuevo precio
                    Console.WriteLine("Cantidad antigua: " + getVenta(p.ventaId).precio);
                    LogMessageToFile("Nueva puja superior (Item " + p.ventaId + " ): " + p.cantidad);
                    Console.WriteLine(p.ventaId + ", " + p.cantidad);
                    setNuevoPrecio(p.ventaId, p.cantidad, p.pujadorId);

                    //Informar mediante topic del nuevo precio
                    broadcastPuja(p);

                }
                else
                {
                    LogMessageToFile("Puja descartada (Item " + p.ventaId + " ): " + p.cantidad);
                }
            }
        }

        public void setNuevoPrecio(int id, int precio, int pujador){
            for (int i = 0; i < listaVentas.Count; i++)
            {
                if (listaVentas[i].id == id)
                {
                    listaVentas[i].precio = precio;
                }
            }

            //Cambiar el precio en la BD
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "update ventas set precio = " + precio + " where id =" + id;
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();

            //Añadir la puja a la BD
            //Comprobar si el usuario ya ha pujado
            conn.Open();
            command.CommandText = "select count(*) from pujas where idUsuario=?idUsuario";
            command.Parameters.Add("?idUsuario", pujador);
            int count = Convert.ToInt32(command.ExecuteScalar());
            conn.Close();

            Console.WriteLine("Contado = " + count);

            if (count > 0)
            {
                command.CommandText = "update pujas set precioPuja = " + precio + " where idUsuario =" + pujador;
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                conn.Open();
                command.CommandText = "insert into pujas (idUsuario, idVenta, precioPuja) values (?pujador, ?idVenta, ?precioPuja)";
                command.Parameters.Add("?pujador", pujador);
                command.Parameters.Add("?idVenta", id);
                command.Parameters.Add("?precioPuja", precio);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public Venta getVenta(int id)
        {
            Venta v = new Venta();

            for (int i = 0; i < listaVentas.Count; i++)
            {
                if (listaVentas[i].id == id)
                {
                    v = listaVentas[i];
                }
            }

            return v;
        }

        public bool isVentaTerminada(int id)
        {
            bool terminada = true;

            for (int i = 0; i < listaVentas.Count; i++)
            {
                if (listaVentas[i].id == id)
                {
                    //Si aun esta en la lista, no ha terminado
                    terminada = false;
                }
            }

            return terminada;
        }

        public void vigilarTiempos()
        {
            int tam = 0;
            
            while (true)
            {
                tam = listaVentas.Count;
                DateTime dt = DateTime.UtcNow;
                for (int i = 0; i < tam; i++)
                {
                    if (i < listaVentas.Count)
                    {
                        if (listaVentas[i] != null)
                        {
                            DateTime dt1 = listaVentas[i].fecha_F;
                            int finalizada = listaVentas[i].finalizada;
                            if (dt1 < dt || finalizada == 1)
                            {
                                //Finalizar venta
                                //Debug.WriteLine("Venta finalizada: " + listaVentas[i].id);
                                if (listaVentas[i].negociado == 2)
                                {
                                    if (listaVentas[i].finalizada == 0)
                                    {
                                        listaVentas[i].finalizada = 3;
                                        gestionarNegociadoManual(listaVentas[i]);
                                    }
                                    else if (listaVentas[i].finalizada == 4)
                                    {
                                        gestionarNegociadoManual(listaVentas[i]);
                                    }
                                }
                                else
                                {
                                    listaVentas[i].finalizada = 1;
                                    gestionarVentaFinalizada(listaVentas[i]);
                                }
                            }
                            else if (listaVentas[i].finalizada == 2) //Cancelada
                            {
                                Usuario u = new Usuario();
                                u.Nombre = "Nadie";
                                informarVentaFinalizada(listaVentas[i], u);

                                //Purgar venta de la lista actual
                                LogMessageToFile("Item ( " + listaVentas[i].id + "): Cancelado");
                                listaVentas.RemoveAt(i);
                                
                            }
                            else if (listaVentas[i].finalizada == 4) //Vendida, negociado manual
                            {
                                gestionarNegociadoManual(listaVentas[i]);
                            }
                        }
                    }
                }
            }
        }

        public void gestionarNegociadoManual(Venta v)
        {
            if (v.finalizada == 4) //El negociado ha terminado
            {
                finalizarVentaManual(v);
            }
            else if (v.finalizada == 3) //El tiempo ha terminado y hay que pasar al estado de negociado
            {
               
                //Ponemos la venta en espera en la BD
                MySqlConnection conn = new MySqlConnection(connString);
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "update ventas set finalizada = 3 where id =" + v.id;
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                //Evitamos que se puedan enviar mas pujas 
                informarVentaFinalizada(v, new Usuario());
            }
        }

        public void finalizarVentaManual(Venta v)
        {
            //Dar como finalizada la venta en la BD
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "update ventas set finalizada = 1 where id =" + v.id;
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();

            //Obtener al usuario de la puja maxima
            command.CommandText = "select * from usuarios u where u.id = " + v.idComprador;
            conn.Open();
            Usuario uGanador = new Usuario();

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                uGanador.Nombre = reader["nombre"].ToString();
                uGanador.Mail = reader["mail"].ToString();
            }
            conn.Close();

            //Obtener al usuario vendedor
            command.CommandText = "select * from usuarios u, ventas v where u.id = v.vendedor and v.id =" + v.id;
            conn.Open();
            Usuario uVendedor = new Usuario();

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                uVendedor.Nombre = reader["nombre"].ToString();
                uVendedor.Mail = reader["mail"].ToString();
            }
            conn.Close();

            //Enviar un mail ambos
            enviarMail(uGanador, v, "Le informamos que ha ganado la subasta del articulo: " + v.tipo + " con ID: " + v.id);
            enviarMail(uVendedor, v, "Le informamos que ha vendio el articulo: " + v.tipo + " con ID: " + v.id);

            //Informar a los pujadores de la venta finalizada (TOPIC)
            informarVentaFinalizada(v, uGanador);

            //Purgar venta de la lista actual
            for (int i = 0; i < listaVentas.Count; i++)
            {
                if (listaVentas[i].id == v.id)
                {
                    listaVentas.RemoveAt(i);
                }
            }

            LogMessageToFile(uGanador.Nombre + " ha ganado la subasta del artículo: " + v.tipo + " con ID: " + v.id.ToString() + ". Vendido por:  " + uVendedor.Nombre);

        }

        public void gestionarVentaFinalizada(Venta v)
        {
            //Dar como finalizada la venta en la BD
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "update ventas set finalizada = 1 where id =" + v.id;
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();

            //Informar al vendedor y comprador de que la venta ha finalizado
            //Obtener la puja maxima
            command.CommandText = "select * from pujas p where p.idVenta = " + v.id;
            conn.Open();
            Puja pMax = new Puja();

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Puja p = new Puja();
                p.ventaId = int.Parse(reader["idVenta"].ToString());
                p.pujadorId = int.Parse(reader["idUsuario"].ToString());
                p.cantidad = int.Parse(reader["precioPuja"].ToString());

                if (p.cantidad > pMax.cantidad){
                    pMax = p;
                }
            }
            conn.Close();

            //Obtener al usuario de la puja maxima
            command.CommandText = "select * from usuarios u where u.id = " + pMax.pujadorId;
            conn.Open();
            Usuario uGanador = new Usuario();

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                uGanador.Nombre = reader["nombre"].ToString();
                uGanador.Mail = reader["mail"].ToString();
            }
            conn.Close();

            //Obtener al usuario vendedor
            command.CommandText = "select * from usuarios u, ventas v where u.id = v.vendedor and v.id =" + pMax.ventaId;
            conn.Open();
            Usuario uVendedor = new Usuario();

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                uVendedor.Nombre = reader["nombre"].ToString();
                uVendedor.Mail = reader["mail"].ToString();
            }
            conn.Close();

            //Enviar un mail ambos
            enviarMail(uGanador, v, "Le informamos que ha ganado la subasta del articulo: " + v.tipo + " con ID: " + v.id);
            enviarMail(uVendedor, v, "Le informamos que ha vendido el articulo: " + v.tipo + " con ID: " + v.id);

            //Informar a los pujadores de la venta finalizada (TOPIC)
            informarVentaFinalizada(v, uGanador);


            //Purgar venta de la lista actual
            for (int i = 0; i < listaVentas.Count; i++)
            {
                if (listaVentas[i].id == v.id)
                {
                    listaVentas.RemoveAt(i);
                }
            }

            LogMessageToFile(uGanador.Nombre + " ha ganado la subasta del artículo: " + v.tipo + " con ID: " + v.id.ToString() + ". Vendido por:  " + uVendedor.Nombre);
        }

        public void enviarMail(Usuario u, Venta v, string mensaje)
        {
            if (u.Nombre != null)
            {
                var fromAddress = new MailAddress("sorporject@gmail.com", "SORProject");
                var toAddress = new MailAddress(u.Mail, u.Nombre);
                const string fromPassword = "sorproject";
                const string subject = "¡Has ganado una subasta!";
                string body = mensaje;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    //smtp.Send(message);
                }
            }
        }

        public void informarVentaFinalizada(Venta v, Usuario comprador)
        {
            //Informar a los pujadores de la venta finalizada (TOPIC)
            const string TOPIC_NAME = "Ventas";
            const string BROKER = "tcp://localhost:61616";
            TopicPublisher publisher;

            publisher = new TopicPublisher(TOPIC_NAME, BROKER);
            v.comprador = comprador.Nombre;

            var javaScriptSerializer = new JavaScriptSerializer();
            String item = javaScriptSerializer.Serialize(v);
            //Debug.WriteLine(item);
            publisher.SendMessage(item);
        }

        public void broadcastPuja(Puja p)
        {
            const string TOPIC_NAME = "Pujas";
            const string BROKER = "tcp://localhost:61616";
            TopicPublisher publisher;

            publisher = new TopicPublisher(TOPIC_NAME, BROKER);

            var javaScriptSerializer = new JavaScriptSerializer();
            String item = javaScriptSerializer.Serialize(p);

            publisher.SendMessage(item);
            
        }

        public void insertar(List<Venta> lv)
        {
            listaVentas = lv;
        }

        public List<Venta> getListaVentas()
        {
            return listaVentas;
        }

        public string GetTempPath()
        {
            string path = System.Environment.GetEnvironmentVariable("TEMP");
            if (!path.EndsWith("\\")) path += "\\";
            return path;
        }

        public void LogMessageToFile(string msg)
        {
            System.IO.StreamWriter sw = System.IO.File.AppendText(
                GetTempPath() + "SORLog.txt");
            try
            {
                string logLine = System.String.Format(
                    "{0:G}: {1}.", System.DateTime.Now, msg);
                sw.WriteLine(logLine);
            }
            finally
            {
                sw.Close();
            }
        }
    }
}
