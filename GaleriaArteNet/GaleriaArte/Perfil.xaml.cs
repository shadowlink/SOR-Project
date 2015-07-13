using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GaleriaArte.Clases;

namespace GaleriaArte
{
    /// <summary>
    /// Interaction logic for Perfil.xaml
    /// </summary>
    public partial class Perfil : UserControl
    {
        Usuario user = new Usuario();

        public Perfil(Usuario u)
        {
            InitializeComponent();
            user.id = u.id;
            tbNombre.Text = u.Nombre;
            tbMail.Text = u.Mail;
        }

        private void btConfirm_Click(object sender, RoutedEventArgs e)
        {
            Boolean valido = true;
            Usuario u = new Usuario();
            Auxiliar aux = new Auxiliar();
            string jsonUser = "";
            u.id = user.id;
            u.Nombre = tbNombre.Text;
            u.Mail = tbMail.Text;
            u.Password = "";

            if (tbPass1.Password == tbPass2.Password)
            {
                if (tbPass1.Password != "" && tbPass2.Password != "")
                {
                    u.Password = aux.CalculateSha1(tbPass1.Password, Encoding.Default).ToLower();
                }
            }
            else
            {
                lbMessage.Foreground = Brushes.Red;
                lbMessage.Content = "Las contaseñas son distintas";
                valido = false;
            }

            if (u.Nombre == "")
            {
                lbMessage.Foreground = Brushes.Red;
                lbMessage.Content = "El campo nombre no puede estar vacio";
                valido = false;
            }

            if (u.Mail == "")
            {
                lbMessage.Foreground = Brushes.Red;
                lbMessage.Content = "El campo e-mail no puede estar vacio";
                valido = false;
            }

            if (valido)
            {
                Usuarios serv = new Usuarios();
                serv.Url = new Juddi().getServiceUrl("Usuarios");
                var javaScriptSerializer = new JavaScriptSerializer();
                jsonUser = javaScriptSerializer.Serialize(u);
                serv.updateUser(jsonUser);
                lbMessage.Foreground = Brushes.Green;
                lbMessage.Content = "Cambios realizados";
            }
        }
    }
}
