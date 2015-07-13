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
    /// Interaction logic for VentanaListaPujas.xaml
    /// </summary>
    /// 


    public partial class VentanaListaPujas : Window
    {
        public VentanaListaPujas(ItemPuja p, Usuario u)
        {
            InitializeComponent();

            colIdPujador.DisplayMemberBinding = new Binding("pujadorId");
            colIdVenta.DisplayMemberBinding = new Binding("ventaId");
            colPuja.DisplayMemberBinding = new Binding("cantidad");

            obtenerPujas(p);
        }

        public void obtenerPujas(ItemPuja p)
        {
            List<Puja> lv = new List<Puja>();
            var javaScriptSerializer = new JavaScriptSerializer();
            string jsonPujas = "";

            Ventas serv = new Ventas();
            serv.Url = new Juddi().getServiceUrl("Ventas");
            jsonPujas = serv.getPujasVenta(p.id);
            lv = javaScriptSerializer.Deserialize<List<Puja>>(jsonPujas);
            for (int i = 0; i < lv.Count; i++)
            {
                lvPujas.Items.Add(new Puja { ventaId = lv[i].ventaId, pujadorId = lv[i].pujadorId, cantidad = lv[i].cantidad});
            }
        }

        private void btnVender_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Puja p = button.DataContext as Puja;
            int id = p.ventaId;
            Ventas serv = new Ventas();
            serv.Url = new Juddi().getServiceUrl("Ventas");
            serv.finalizarVenta(id, 4, p.pujadorId);
            this.Close();
        }
    }
}
