using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.Services.Description;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GaleriaArte.Clases;
using System.Text.RegularExpressions;


namespace GaleriaArte
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    ///        

    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void bLogin_Click(object sender, RoutedEventArgs e)
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
                labelInfo.Content = "El servidor no responde";
            }
            var javaScriptSerializer = new JavaScriptSerializer();
            user = javaScriptSerializer.Deserialize<Usuario>(jsonUser);

            Console.WriteLine("Pass: " + user.Password);

            if (user.Password == aux.CalculateSha1(tbPass.Password, Encoding.Default).ToLower())
            {
                if (user.Privilegios == 1)
                {
                    MainWindow main = new MainWindow(user);
                    main.Show();
                    this.Close();
                }
            }else{
                labelInfo.Foreground = Brushes.Red;
                labelInfo.Content = "Error en la autentificación";
            }
            
        }

        private void bNewUser_Click(object sender, RoutedEventArgs e)
        {
        	
            Usuario user = new Usuario();
            Auxiliar aux = new Auxiliar();
            string jsonUser = "";

            Usuarios serv = new Usuarios();
            serv.Url = new Juddi().getServiceUrl("Usuarios");
            var javaScriptSerializer = new JavaScriptSerializer();

            if (tbPass1.Password == tbPass2.Password)
            {
                Regex rx = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = rx.Match(tbNewMail.Text);
                if (match.Success)
                {
                    user.Nombre = tbNewUser.Text;
                    user.Password = aux.CalculateSha1(tbPass1.Password, Encoding.Default).ToLower();
                    user.Mail = tbNewMail.Text;
                    user.Privilegios = 1;
                    jsonUser = javaScriptSerializer.Serialize(user);

                    if (!serv.newUser(jsonUser))
                    {
                        lbNewUser.Foreground = Brushes.Red;
                        lbNewUser.Content = "El usuario ya existe";
                    }
                    else
                    {
                        lbNewUser.Foreground = Brushes.Green;
                        lbNewUser.Content = "Usuario creado con éxito";
                    }
                }
                else
                {
                    lbNewUser.Foreground = Brushes.Red;
                    lbNewUser.Content = "Mail incorrecto";
                }
            }
            else
            {
                lbNewUser.Foreground = Brushes.Red;
                lbNewUser.Content = "Los passwords no coinciden";
            }
        }

    }
}
