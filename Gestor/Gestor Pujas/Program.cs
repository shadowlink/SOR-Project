using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Gestor_Pujas
{
    class Program
    {
        private static string connString = "Server=127.0.0.1;Port=3306;Database=sordb;Uid=root;password=lizerg";
        static GestorPujas gp;
        static List<Venta> listaVentas;
        static Hashtable idHash = new Hashtable();

        //ActiveMQ Topics

        //Obras
        const string TOPIC_NAME = "Obras";
        const string BROKER = "tcp://localhost:61616";
        static TopicPublisher publisher;
        private readonly StringBuilder builder = new StringBuilder();
        private delegate void SetTextCallback(string text);

        static void Main(string[] args)
        {
            gp = new GestorPujas();
            ThreadStart job1 = new ThreadStart(gp.escucharPujas);
            ThreadStart job2 = new ThreadStart(gp.vigilarTiempos);

            Thread thread1 = new Thread(job1);
            Thread thread2 = new Thread(job2);

            thread1.Start();
            thread2.Start();

            InicializaVentas();
        }


        //Vigila la BD en busca de nuevas ventas
        static void VigilarBD()
        {
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            Venta v;
            List<int> toDelete = new List<int>();
            List<int> toFalse = new List<int>();
            

            while (true)
            {
                //Buscamos ventas en curso que pueden no estar en nuestra lista
                command.CommandText = "Select * from ventas where finalizada = 0";
                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    v = new Venta();
                    v.id = int.Parse(reader["id"].ToString());
                    v.tipo = reader["tipo"].ToString();
                    v.estado = reader["estado"].ToString();
                    v.autor = reader["autor"].ToString();
                    v.año = 0;
                    v.precio = int.Parse(reader["precio"].ToString());
                    v.fecha_I = Convert.ToDateTime(reader["fecha_I"].ToString());
                    v.fecha_F = Convert.ToDateTime(reader["fecha_F"].ToString());
                    v.finalizada = int.Parse(reader["finalizada"].ToString());
                    v.negociado = int.Parse(reader["negociado"].ToString());
                    v.idComprador = int.Parse(reader["idComprador"].ToString());

                    if (isNuevaVenta(v))
                    {
                        //Informar de la nueva venta en el Topic Obras
                        addToTopic(v);
                        listaVentas = gp.getListaVentas();
                        listaVentas.Add(v);
                        gp.insertar(listaVentas);
                        idHash.Add(v.id, true);
                    }
                    else if (isVentaEditada(v)) //Venta existente que ha sido editada
                    {
                        editarVenta(v);
                        addToTopic(v);
                        idHash[v.id] = true;
                    }
                    else //No es nueva
                    {
                        idHash[v.id] = true;
                    }
                    
                }

                //Comprobamos si todos son true, si no lo son, alguna venta ha finalizado
                foreach (int key in idHash.Keys)
                {
                    if ((Boolean)idHash[key] == false)
                    {
                        //Esta id ya no esta por lo que debe haber finalizado o haber sido cancelada, actualizamos la lista
                        isFinalizada(key);
                        //La eliminamos del Hash excepto si finalizada es 3 (esperando autorizacion vendedor)
                        if (!enEspera(key))
                        {
                            toDelete.Add(key);
                        }
                    }
                }

                for(int i=0; i<toDelete.Count; i++){
                    idHash.Remove(toDelete[i]);
                }

                foreach (int key in idHash.Keys)
                {
                    toFalse.Add(key);
                }

                for (int i = 0; i < toFalse.Count; i++)
                {
                    idHash[toFalse[i]] = false;
                }

                toFalse.Clear();
                toDelete.Clear();
                conn.Close();
            }

        }

        public static bool enEspera(int id)
        {
            bool enEspera = false;

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            Venta v = new Venta();

            command.CommandText = "Select * from ventas where id =" + id;
            conn.Open();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                v.id = int.Parse(reader["id"].ToString());
                v.finalizada = int.Parse(reader["finalizada"].ToString());

            }

            if (v.finalizada == 3)
            {
                enEspera = true;
                Debug.WriteLine("La venta: " + v.id + " es 3");
            }
            else if (v.finalizada == 4)
            {
                Debug.WriteLine("La venta: " + v.id + " es 4");
                isFinalizada(v.id);
            }
            conn.Close();

            return enEspera;
        }

        public static void addToTopic(Venta v)
        {
            publisher = new TopicPublisher(TOPIC_NAME, BROKER);

            var javaScriptSerializer = new JavaScriptSerializer();
            String item = javaScriptSerializer.Serialize(v);

            publisher.SendMessage(item);
        }

        //Se llena la lista con las ventas de la BD
        static void InicializaVentas()
        {
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            Venta v;

            command.CommandText = "Select * from ventas where finalizada = 0";
            conn.Open();
            listaVentas = new List<Venta>();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                v = new Venta();
                v.id = int.Parse(reader["id"].ToString());
                v.tipo = reader["tipo"].ToString();
                v.estado = reader["estado"].ToString();
                v.autor = reader["autor"].ToString();
                v.año = 0;
                v.precio = int.Parse(reader["precio"].ToString());
                v.fecha_I = Convert.ToDateTime(reader["fecha_I"].ToString());
                v.fecha_F = Convert.ToDateTime(reader["fecha_F"].ToString());
                v.finalizada = int.Parse(reader["finalizada"].ToString());
                v.negociado = int.Parse(reader["negociado"].ToString());
                v.idComprador = int.Parse(reader["idComprador"].ToString());
                listaVentas.Add(v);
                idHash.Add(v.id, true);
            }
            gp.insertar(listaVentas);
            conn.Close();
            VigilarBD();
        }

        static bool isNuevaVenta(Venta v){
            bool esNueva = true;
            for (int i = 0; i < listaVentas.Count; i++)
            {
                if (listaVentas[i].id == v.id)
                {    
                    esNueva = false;
                }
            }
            return esNueva;
        }

        static bool isVentaEditada(Venta v)
        {
            bool editada = true;
            for (int i = 0; i < listaVentas.Count; i++)
            {
                if (listaVentas[i].id == v.id)
                {
                    if(listaVentas[i].fecha_F == v.fecha_F){
                        editada = false;
                    }
                }
            }
            return editada;
        }

        static void editarVenta(Venta v)
        {
            for (int i = 0; i < listaVentas.Count; i++)
            {
                if (listaVentas[i].id == v.id)
                {
                    listaVentas[i].fecha_F = v.fecha_F;
                }
            }
        }

        static void isFinalizada(int id)
        {
            Debug.WriteLine("La venta: " + id + " esta finalizandose");
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            Venta v = new Venta();

            command.CommandText = "Select * from ventas where id =" + id;
            conn.Open();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                v.id = int.Parse(reader["id"].ToString());
                v.finalizada = int.Parse(reader["finalizada"].ToString());
                v.idComprador = int.Parse(reader["idComprador"].ToString());
            }
            conn.Close();

            for (int i = 0; i < listaVentas.Count; i++)
            {
                if (listaVentas[i].id == id)
                {
                    listaVentas[i].finalizada = v.finalizada;
                    listaVentas[i].idComprador = v.idComprador;
                }
            }
        }
    }
}
