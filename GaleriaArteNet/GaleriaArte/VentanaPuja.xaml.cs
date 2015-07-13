using Apache.NMS;
using Apache.NMS.Util;
using GaleriaArte.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Interaction logic for VentanaPuja.xaml
    /// </summary>
    public partial class VentanaPuja : Window
    {

        //ActiveMQ Colas 
        Uri connecturi = new Uri("activemq:tcp://localhost:61616");
        IConnectionFactory factory;

        ItemPuja itemPuja = new ItemPuja();
        Usuario user = new Usuario();

        public VentanaPuja(ItemPuja ip, Usuario u)
        {
            factory = new NMSConnectionFactory(connecturi);
            user = u;
            itemPuja = ip;
            InitializeComponent();
        }

        private void btnPuja_Click(object sender, RoutedEventArgs e)
        {
            string jsonPuja = "";
            Auxiliar aux = new Auxiliar();
            var javaScriptSerializer = new JavaScriptSerializer();

            Puja p = new Puja();
            p.pujadorId = user.id;
            p.ventaId = itemPuja.id;
            p.cantidad = int.Parse(tbCantidad.Text);

            jsonPuja = javaScriptSerializer.Serialize(p);

            using (IConnection connection = factory.CreateConnection())
            using (ISession session = connection.CreateSession())
            {
                IDestination destination = SessionUtil.GetDestination(session, "queue://Pujas");
                using (IMessageProducer producer = session.CreateProducer(destination))
                {
                    connection.Start();
                    ITextMessage request = session.CreateTextMessage(jsonPuja);
                    producer.Send(request);
                    connection.Close();
                }
            }

            tbCantidad.Clear();
        }


    }
}
