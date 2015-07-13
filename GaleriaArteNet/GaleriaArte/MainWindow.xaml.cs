using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using GaleriaArte.Clases;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace GaleriaArte
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Usuario user;
        HistorialVentas hv;
        NuevaVenta nv;
        Pujas pj;
        Perfil pf;
        const string BROKER = "tcp://192.168.2.16:61616";

        public MainWindow(Usuario u)
        {
            user = new Usuario();
            user = u;
            InitializeComponent();
            lblName.Content = u.Nombre;
            hv = new HistorialVentas(user);
            nv = new NuevaVenta(user);
            pj = new Pujas(user);
            pf = new Perfil(user);
            stkTest.Children.Clear();
            stkTest.Children.Add(hv);

            //Topic Obras
            string CLIENT_ID1 = Guid.NewGuid().ToString();
            string CONSUMER_ID1 = Guid.NewGuid().ToString();
            TopicSubscriber subscriber = new TopicSubscriber("Obras", BROKER, CLIENT_ID1, CONSUMER_ID1);
            subscriber.OnMessageReceived += new MessageReceivedDelegate(subscriber_OnMessageReceived);

            //Topic Pujas
            string CLIENT_ID2 = Guid.NewGuid().ToString();
            string CONSUMER_ID2 = Guid.NewGuid().ToString();
            TopicSubscriber subscriber2 = new TopicSubscriber("Pujas", BROKER, CLIENT_ID2, CONSUMER_ID2);
            subscriber2.OnMessageReceived += new MessageReceivedDelegate(subscriber2_OnMessageReceived);

            //Topic Ventas
            string CLIENT_ID3 = Guid.NewGuid().ToString();
            string CONSUMER_ID3 = Guid.NewGuid().ToString();
            TopicSubscriber subscriber3 = new TopicSubscriber("Ventas", BROKER, CLIENT_ID3, CONSUMER_ID3);
            subscriber3.OnMessageReceived += new MessageReceivedDelegate(subscriber3_OnMessageReceived);
        }

        private void btnVenta_Click(object sender, RoutedEventArgs e)
        {
            stkTest.Children.Clear();
            stkTest.Children.Add(nv);
        }

        private void btHistorial_Click(object sender, RoutedEventArgs e)
        {
            stkTest.Children.Clear();
            stkTest.Children.Add(hv);
        }

        private void btPujas_Click(object sender, RoutedEventArgs e)
        {
            stkTest.Children.Clear();
            stkTest.Children.Add(pj);
        }

        private void btPerfil_Click(object sender, RoutedEventArgs e)
        {
            stkTest.Children.Clear();
            stkTest.Children.Add(pf);
        }

        public void subscriber3_OnMessageReceived(string message)
        {
            Venta v = new Venta();
            var javaScriptSerializer = new JavaScriptSerializer();
            v = javaScriptSerializer.Deserialize<Venta>(message);

            //Informar de venta finalizada y a quien o si ha sido cancelada
            if (v.finalizada == 2) //Cancelada
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    txtEvent.Foreground = Brushes.Red;
                    txtEvent.AppendText("La subasta de la obra " + v.tipo + " con ID " + v.id + " ha sido cancelada por el vendedor\r\n");
                    txtEvent.Focus();
                    txtEvent.ScrollToEnd();

                }));
            }
            else if (v.finalizada == 1) //Vendida
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    if (v.comprador != null)
                    {
                        txtEvent.Foreground = Brushes.Green;
                        txtEvent.AppendText("La obra " + v.tipo + " con ID " + v.id + " ha sido vendida a " + v.comprador + "\r\n");
                        txtEvent.Focus();
                        txtEvent.ScrollToEnd();
                    }
                    else
                    {
                        txtEvent.Foreground = Brushes.Red;
                        txtEvent.AppendText("La subasta de la obra " + v.tipo + " con ID " + v.id + " ha quedado desierta \r\n");
                        txtEvent.Focus();
                        txtEvent.ScrollToEnd();
                    }
                }));
            }
            else if (v.finalizada == 4) //Vendida (Negociado Manual)
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    txtEvent.Foreground = Brushes.Green;
                    txtEvent.AppendText("La obra " + v.tipo + " con ID " + v.id + " ha sido vendida a " + v.comprador + "\r\n");
                    txtEvent.Focus();
                    txtEvent.ScrollToEnd();
                }));
            }

            pj.topicVentasMessage(message);
            nv.obtenerVentas();
            hv.actualizarVentas();
        }

        public void subscriber2_OnMessageReceived(string message)
        {
            pj.topicPujasMessage(message);
            nv.obtenerVentas();
        }

        public void subscriber_OnMessageReceived(string message)
        {
            pj.topicObrasMessage(message);
            nv.obtenerVentas();
        }


    }
}
