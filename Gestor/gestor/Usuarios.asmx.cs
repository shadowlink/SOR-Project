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
    /// Descripción breve de Usuarios
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]


    public class Usuarios : System.Web.Services.WebService
    {

        private static string connString = "Server=127.0.0.1;Port=3306;Database=sordb;Uid=root;password=lizerg";

        [WebMethod]
        public bool tryLogin(string mail, string pass, int priv)
        {
            bool res = false;
            string dbPass = "";
            int dbPriv = 0;
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "Select password, mail, privilegios from usuarios where mail=" + "'" + mail + "'";
            conn.Open();

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                dbPass = reader["password"].ToString();
                priv = int.Parse(reader["privilegios"].ToString());
            }

            if (dbPass == pass && priv == dbPriv)
            {
                res = true;
            }
            conn.Close();

            return res;
        }

        [WebMethod]
        public string getUser(string mail)
        {
            Usuario user = new Usuario();
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "Select id, nombre, password, mail, privilegios from usuarios where mail="+"'"+mail+"'";
            conn.Open();

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                user.id = int.Parse(reader["id"].ToString());
                user.Nombre = reader["nombre"].ToString();
                user.Password = reader["password"].ToString();
                user.Mail = reader["mail"].ToString();
                user.Privilegios = int.Parse(reader["privilegios"].ToString());
                LogMessageToFile("Usuario: " + user.Nombre + " con nivel de privilegios " + user.Privilegios + " ha accedido al sistema");
            }
            
            conn.Close();

            var javaScriptSerializer = new JavaScriptSerializer();

            return javaScriptSerializer.Serialize(user);

        }

        [WebMethod]
        public bool newUser(string userJson)
        {
            bool res = false;
            Usuario user = new Usuario();
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            var javaScriptSerializer = new JavaScriptSerializer();
            user = javaScriptSerializer.Deserialize<Usuario>(userJson);

            //Comprobamos que el usuario no exista ya mesiante el mail
            conn.Open();
            command.CommandText = "select count(*) from usuarios where mail=?mail";
            command.Parameters.Add("?mail", user.Mail);
            int count = Convert.ToInt32(command.ExecuteScalar());
            conn.Close();

            //Si no existe lo creamos
            if (count == 0)
            {
                command = conn.CreateCommand();
                conn.Open();
                command.CommandText = "insert into usuarios (nombre, password, mail, privilegios) values (?nombre, ?password, ?mail, ?privilegios)";
                command.Parameters.Add("?nombre", user.Nombre);
                command.Parameters.Add("?password", user.Password);
                command.Parameters.Add("?mail", user.Mail);
                command.Parameters.Add("?privilegios", user.Privilegios);
                command.ExecuteNonQuery();
                conn.Close();
                res = true;
                LogMessageToFile("Nuevo usuario creado: " + user.Nombre);
            }

            return res;
        }

        [WebMethod]
        public bool updateUser(string userJson)
        {
            bool res = false;
            Usuario user = new Usuario();
            var javaScriptSerializer = new JavaScriptSerializer();
            user = javaScriptSerializer.Deserialize<Usuario>(userJson);
            Debug.WriteLine(userJson);
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            if (user.Password != "")
            {
                command.CommandText = "update usuarios set nombre = '" + user.Nombre + "', mail = '" + user.Mail + "', password= '" + user.Password + "' where id =" + user.id;
            }
            else
            {
                command.CommandText = "update usuarios set nombre = '" + user.Nombre + "', mail = '" + user.Mail + "' where id =" + user.id;
            }
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            LogMessageToFile("Usuario " + user.id + " ha actualizado sus datos");
            res = true;
            return res;
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
