using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.Util;
using GaleriaArte.Clases;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace GaleriaArte
{
    /// <summary>
    /// Interaction logic for Pujas.xaml
    /// </summary>
    /// 

    public class ItemPuja
    {
        public int negociado { get; set; }
        public int id { get; set; }
        public string tipo { get; set; }
        public string estado { get; set; }
        public string autor { get; set; }
        public string tiempoActual { get; set; }
        public int pujaMax { get; set; }
    }

    public partial class Pujas : UserControl
    {
        Usuario user;

        public Pujas(Usuario u)
        {
            user = new Usuario();
            user = u;

            InitializeComponent();
            colId.DisplayMemberBinding = new Binding("id");
            colTipo.DisplayMemberBinding = new Binding("tipo");
            colAutor.DisplayMemberBinding = new Binding("autor");
            colEstado.DisplayMemberBinding = new Binding("estado");
            colTiempo.DisplayMemberBinding = new Binding("tiempoActual");
            colPujaMax.DisplayMemberBinding = new Binding("pujaMax");

            obtenerVentas();
        }

        public void obtenerVentas()
        {
            List<Venta> lv = new List<Venta>();
            var javaScriptSerializer = new JavaScriptSerializer();
            string jsonVentas = "";

            Ventas serv = new Ventas();
            serv.Url = new Juddi().getServiceUrl("Ventas");
            jsonVentas = serv.getVentas(user.id);
            lv = javaScriptSerializer.Deserialize<List<Venta>>(jsonVentas);
            for (int i = 0; i < lv.Count; i++)
            {
                DateTime utcDate = DateTime.SpecifyKind(Convert.ToDateTime(lv[i].fecha_F), DateTimeKind.Utc);
                var localTime = utcDate.ToLocalTime();
                lvPujas.Items.Add(new ItemPuja { id = lv[i].id, tipo = lv[i].tipo, estado = lv[i].estado, autor = lv[i].autor, tiempoActual = localTime.ToString(), pujaMax = lv[i].precio });
            }
        }

        public void topicObrasMessage(string message)
        {
            List<Venta> lv = new List<Venta>();
            var javaScriptSerializer = new JavaScriptSerializer();
            string jsonVentas = "";
            
            this.Dispatcher.Invoke((Action)(() =>
            {
                Ventas serv = new Ventas();
                lvPujas.Items.Clear();
                serv.Url = new Juddi().getServiceUrl("Ventas");
                jsonVentas = serv.getVentas(user.id);
                lv = javaScriptSerializer.Deserialize<List<Venta>>(jsonVentas);
                for (int i = 0; i < lv.Count; i++)
                {
                    DateTime utcDate = DateTime.SpecifyKind(Convert.ToDateTime(lv[i].fecha_F), DateTimeKind.Utc);
                    var localTime = utcDate.ToLocalTime();
                    lvPujas.Items.Add(new ItemPuja { id = lv[i].id, tipo = lv[i].tipo, estado = lv[i].estado, autor = lv[i].autor, tiempoActual = localTime.ToString(), pujaMax = lv[i].precio });
                }
            }));
        }

        public void topicPujasMessage(string message)
        {
            Puja p = new Puja();
            var javaScriptSerializer = new JavaScriptSerializer();
            p = javaScriptSerializer.Deserialize<Puja>(message);

            this.Dispatcher.Invoke((Action)(() =>
            {
                List<Venta> lv = new List<Venta>();
                string jsonVentas = "";
                lvPujas.Items.Clear();
                Ventas serv = new Ventas();
                serv.Url = new Juddi().getServiceUrl("Ventas");
                jsonVentas = serv.getVentas(user.id);
                lv = javaScriptSerializer.Deserialize<List<Venta>>(jsonVentas);
                for (int i = 0; i < lv.Count; i++)
                {
                    DateTime utcDate = DateTime.SpecifyKind(Convert.ToDateTime(lv[i].fecha_F), DateTimeKind.Utc);
                    var localTime = utcDate.ToLocalTime();
                    lvPujas.Items.Add(new ItemPuja { id = lv[i].id, tipo = lv[i].tipo, estado = lv[i].estado, autor = lv[i].autor, tiempoActual = localTime.ToString(), pujaMax = lv[i].precio });
                }
            }));
        }

        public void topicVentasMessage(string message)
        {
            Venta v = new Venta();
            var javaScriptSerializer = new JavaScriptSerializer();
            v = javaScriptSerializer.Deserialize<Venta>(message);

            //Actualizamos la lista para eliminar la venta finalizada
            this.Dispatcher.Invoke((Action)(() =>
            {
                List<Venta> lv = new List<Venta>();
                string jsonVentas = "";
                lvPujas.Items.Clear();
                Ventas serv = new Ventas();
                serv.Url = new Juddi().getServiceUrl("Ventas");
                jsonVentas = serv.getVentas(user.id);
                lv = javaScriptSerializer.Deserialize<List<Venta>>(jsonVentas);
                for (int i = 0; i < lv.Count; i++)
                {
                    DateTime utcDate = DateTime.SpecifyKind(Convert.ToDateTime(lv[i].fecha_F), DateTimeKind.Utc);
                    var localTime = utcDate.ToLocalTime();
                    lvPujas.Items.Add(new ItemPuja { id = lv[i].id, tipo = lv[i].tipo, estado = lv[i].estado, autor = lv[i].autor, tiempoActual = localTime.ToString(), pujaMax = lv[i].precio });
                }
            }));
        }

        private void btnPuja_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ItemPuja p = button.DataContext as ItemPuja;
            int id = p.id;

            VentanaPuja vp = new VentanaPuja(p, user);
            vp.Show();
        }
        

    }
}
