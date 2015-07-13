using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSOR
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btCrear_Click(object sender, EventArgs e)
        {
            Usuario user = new Usuario();
            Auxiliar aux = new Auxiliar();
            string jsonUser = "";

            Usuarios serv = new Usuarios();
            serv.Url = new Juddi().getServiceUrl("Usuarios");
            var javaScriptSerializer = new JavaScriptSerializer();

            if (tbPass1.Text == tbPass2.Text)
            {
                if (tbPass1.Text != "" && tbPass2.Text != "")
                {

                    Regex rx = new Regex(@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
                    if (rx.IsMatch(tbMail.Text))
                    {
                        if (user.Nombre != "")
                        {
                            user.Nombre = tbUser.Text;
                            user.Password = aux.CalculateSha1(tbPass1.Text, Encoding.Default).ToLower();
                            user.Privilegios = 2;
                            user.Mail = tbMail.Text;
                            jsonUser = javaScriptSerializer.Serialize(user);

                            if (!serv.newUser(jsonUser))
                            {
                                lbMessage.ForeColor = Color.Red;
                                lbMessage.Text = "El usuario ya existe";

                            }
                            else
                            {
                                lbMessage.ForeColor = Color.Green;
                                lbMessage.Text = "Usuario creado con éxito";
                                Response.Redirect("/");
                            }
                        }
                        else
                        {
                            lbMessage.ForeColor = Color.Red;
                            lbMessage.Text = "El campo Nombre no puede estar vacio";
                        }

                    }
                    else
                    {
                        lbMessage.ForeColor = Color.Red;
                        lbMessage.Text = "Mail incorrecto";
                    }
                }
                else
                {
                    lbMessage.ForeColor = Color.Red;
                    lbMessage.Text = "Las contraseñas no pueden estar en blanco";
                }
            }
            else
            {
                lbMessage.ForeColor = Color.Red;
                lbMessage.Text = "Las contraseñas no coinciden";
            }
        }
    }
}