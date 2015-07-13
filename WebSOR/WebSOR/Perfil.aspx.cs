using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSOR
{
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Id"] != null)
            {
                tbNombre.Text = (string)Session["Nombre"];
                tbMail.Text = (string)Session["Mail"];
            }
            else
            {
                Response.Redirect("/");
            }
        }


        protected void btConfirm_Click(object sender, EventArgs e)
        {
            Boolean valido = true;
            Usuario u = new Usuario();
            Auxiliar aux = new Auxiliar();
            string jsonUser = "";
            u.id = (int)Session["Id"];
            u.Nombre = tbNombre.Text;
            u.Mail = tbMail.Text;
            u.Password = "";

            if (tbPass1.Text == tbPass2.Text)
            {
                if (tbPass1.Text != "" && tbPass2.Text != "")
                {
                    u.Password = aux.CalculateSha1(tbPass1.Text, Encoding.Default).ToLower();
                }
            }
            else
            {
                lbMessage.ForeColor = Color.Red;
                lbMessage.Text = "Las contaseñas son distintas";
                valido = false;
            }

            if (u.Nombre == "")
            {
                lbMessage.ForeColor = Color.Red;
                lbMessage.Text = "El campo nombre no puede estar vacio";
                valido = false;
            }

            if (u.Mail == "")
            {
                lbMessage.ForeColor = Color.Red;
                lbMessage.Text = "El campo e-mail no puede estar vacio";
                valido = false;
            }

            if (valido)
            {
                Usuarios serv = new Usuarios();
                serv.Url = new Juddi().getServiceUrl("Usuarios");
                var javaScriptSerializer = new JavaScriptSerializer();
                jsonUser = javaScriptSerializer.Serialize(u);
                serv.updateUser(jsonUser);
                lbMessage.ForeColor = Color.Green;
                lbMessage.Text = "Cambios realizados";
            }
        }
    }
}