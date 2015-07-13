using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Apache.NMS.Util;
using gestor.Clases;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace gestor
{
    /// <summary>
    /// Descripción breve de Ventas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Ventas : System.Web.Services.WebService
    {
        //Cadena de mySql
        private static string connString = "Server=127.0.0.1;Port=3306;Database=sordb;Uid=root;password=lizerg";

        [WebMethod]
        public void nuevaVenta(string ventaJson)
        {

            Venta venta = new Venta();
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            var javaScriptSerializer = new JavaScriptSerializer();
            venta = javaScriptSerializer.Deserialize<Venta>(ventaJson);
            venta.fecha_I = DateTime.UtcNow.ToString();
            
            Debug.WriteLine(venta.fecha_F);
            conn.Open();
            command.CommandText = "insert into ventas (vendedor, tipo, autor, año, estado, fecha_I, fecha_F, precio, negociado) values (?vendedor, ?tipo, ?autor, ?año, ?estado, ?fecha_I, ?fecha_F,?precio,?negociado)";
            command.Parameters.Add("?vendedor", venta.vendedor);
            command.Parameters.Add("?tipo", venta.tipo);
            command.Parameters.Add("?autor", venta.autor);
            command.Parameters.Add("?año", venta.año);
            command.Parameters.Add("?estado", venta.estado);
            command.Parameters.Add("?fecha_I", Convert.ToDateTime(venta.fecha_I).ToUniversalTime());
            command.Parameters.Add("?fecha_F", Convert.ToDateTime(venta.fecha_F).ToUniversalTime());
            command.Parameters.Add("?precio", venta.precio);
            command.Parameters.Add("?negociado", venta.negociado);
            command.ExecuteNonQuery();
            conn.Close();

            LogMessageToFile("Nueva venta con id " + venta.id);
        }

        [WebMethod]
        public string getVentas(int idUsuario)
        {
            List<Venta> lv = new List<Venta>();
            Venta v = new Venta();

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "Select * from ventas where finalizada = 0 and vendedor!="+idUsuario;
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
                v.fecha_I = reader["fecha_I"].ToString();
                v.fecha_F = reader["fecha_F"].ToString();
                v.finalizada = int.Parse(reader["finalizada"].ToString());
                v.vendedor = int.Parse(reader["vendedor"].ToString());
                lv.Add(v);
            }

            conn.Close();

            var javaScriptSerializer = new JavaScriptSerializer();

            return javaScriptSerializer.Serialize(lv);

        }

        [WebMethod]
        public string getVentasActivas(int userId)
        {
            HistoricoVentas hv = new HistoricoVentas();
            hv.ListaVentas = new List<Venta>();
            Venta v;
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "Select * from ventas where vendedor=" + "'" + userId + "'" + "and (finalizada=0 or finalizada=3)";
            conn.Open();

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                v = new Venta();
                v.id = int.Parse(reader["id"].ToString());
                v.tipo = reader["tipo"].ToString();
                v.precio = int.Parse(reader["precio"].ToString());
                v.fecha_I = reader["fecha_I"].ToString();
                v.fecha_F = reader["fecha_F"].ToString();
                v.negociado = int.Parse(reader["negociado"].ToString());
                hv.ListaVentas.Add(v);
            }

            conn.Close();

            var javaScriptSerializer = new JavaScriptSerializer();

            return javaScriptSerializer.Serialize(hv.ListaVentas);
        }


        [WebMethod]
        public string getVentasEspera(int userId)
        {
            HistoricoVentas hv = new HistoricoVentas();
            hv.ListaVentas = new List<Venta>();
            Venta v;
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "Select * from ventas where vendedor=" + "'" + userId + "'" + "and finalizada=3";
            conn.Open();

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                v = new Venta();
                v.id = int.Parse(reader["id"].ToString());
                v.tipo = reader["tipo"].ToString();
                v.precio = int.Parse(reader["precio"].ToString());
                v.fecha_I = reader["fecha_I"].ToString();
                v.fecha_F = reader["fecha_F"].ToString();
                hv.ListaVentas.Add(v);
            }

            conn.Close();

            var javaScriptSerializer = new JavaScriptSerializer();

            return javaScriptSerializer.Serialize(hv.ListaVentas);
        }

        [WebMethod]
        public string getPujasVenta(int idVenta)
        {
            List<Puja> lv = new List<Puja>();
            Puja p = new Puja();

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "Select * from pujas where idVenta =" + idVenta;
            conn.Open();

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                p = new Puja();
                p.pujadorId = int.Parse(reader["idUsuario"].ToString());
                p.ventaId = int.Parse(reader["idVenta"].ToString());
                p.cantidad = int.Parse(reader["precioPuja"].ToString());
                lv.Add(p);
            }

            conn.Close();

            var javaScriptSerializer = new JavaScriptSerializer();

            return javaScriptSerializer.Serialize(lv);
        }

        [WebMethod]
        public void editarVenta(int id, DateTime newDate)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            DateTime date = Convert.ToDateTime(newDate).ToUniversalTime();
            string fecha = date.ToString("yyyy/MM/dd/HH:mm:ss", CultureInfo.InvariantCulture);
            Debug.WriteLine(fecha);
            command.CommandText = "update ventas set fecha_F ='" + fecha + "' where id =" + id;
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            LogMessageToFile("Item ("+ id + "): Editado, nueva fecha de finalización " + fecha);
        }

        [WebMethod]
        public void finalizarVenta(int ventaId, int motiv, int idComprador)
        {
            if (motiv == 4) //Negociado manual
            {
                MySqlConnection conn = new MySqlConnection(connString);
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "update ventas set finalizada =" + motiv + ", idComprador =" + idComprador + " where id =" + ventaId;
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                MySqlConnection conn = new MySqlConnection(connString);
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "update ventas set finalizada =" + motiv + " where id =" + ventaId;
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
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
