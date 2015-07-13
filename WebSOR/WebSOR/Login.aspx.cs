using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSOR
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Id"] != null)
            {
                Response.Redirect("/");
            }
        }

        protected void btEntrar_Click(object sender, EventArgs e)
        {
            Usuario user = new Usuario();
            Auxiliar aux = new Auxiliar();
            string jsonUser = "";

            Usuarios serv = new Usuarios();
            serv.Url = new Juddi().getServiceUrl("Usuarios");
            try
            {
                jsonUser = serv.getUser(tbMail.Text);
            }
            catch
            {
                //labelInfo.Content = "El servidor no responde";
            }
            var javaScriptSerializer = new JavaScriptSerializer();
            user = javaScriptSerializer.Deserialize<Usuario>(jsonUser);

            if (user.Password == aux.CalculateSha1(tbPass.Text, Encoding.Default).ToLower())
            {
                if (user.Privilegios == 2)
                {
                    //Añadir usuario a la sesion y redirigir
                    Session["Nombre"] = user.Nombre;
                    Session["Mail"] = user.Mail;
                    Session["Id"] = user.id;
                    Response.Redirect("/");
                }
                else
                {
                    lbMessage.ForeColor = Color.Red;
                    lbMessage.Text = "Error en la autentificación";
                }

            }
            else
            {
                lbMessage.ForeColor = Color.Red;
                lbMessage.Text = "Error en la autentificación";
            }
        }
    }
}