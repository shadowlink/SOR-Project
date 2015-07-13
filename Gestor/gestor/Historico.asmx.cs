using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace gestor
{
    /// <summary>
    /// Descripción breve de Historico
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Historico : System.Web.Services.WebService
    {

        private static string connString = "Server=127.0.0.1;Port=3306;Database=sordb;Uid=root;password=lizerg";

        [WebMethod]
        public string devolverTodo(int userId)
        {
            HistoricoVentas hv = new HistoricoVentas();
            hv.ListaVentas = new List<Venta>();
            Venta v;
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "Select * from ventas where vendedor=" + "'" + userId + "' and finalizada=1";
            conn.Open();

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                v = new Venta();
                v.id = int.Parse(reader["id"].ToString());
                v.tipo = reader["tipo"].ToString();
                v.estado = reader["estado"].ToString();
                v.precio = int.Parse(reader["precio"].ToString());
                v.finalizada = int.Parse(reader["finalizada"].ToString());
                hv.ListaVentas.Add(v);
            }

            conn.Close();

            var javaScriptSerializer = new JavaScriptSerializer();

            return javaScriptSerializer.Serialize(hv);
        }
    }
}
