using GaleriaArte.Clases;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GaleriaArte
{
    /// <summary>
    /// Interaction logic for NuevaVenta.xaml
    /// </summary>

    public partial class NuevaVenta : UserControl
    {
        Usuario user;

        public NuevaVenta(Usuario u)
        {
            user = new Usuario();
            user = u;
            InitializeComponent();

            colId.DisplayMemberBinding = new Binding("id");
            colNegociado.DisplayMemberBinding = new Binding("negociado");
            colTipo.DisplayMemberBinding = new Binding("tipo");
            colTiempo.DisplayMemberBinding = new Binding("tiempoActual");
            colPujaMax.DisplayMemberBinding = new Binding("pujaMax");

            obtenerVentas();
        }

        public void obtenerVentas()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                List<Venta> lv = new List<Venta>();
                var javaScriptSerializer = new JavaScriptSerializer();
                string jsonVentas = "";

                lstVentas.Items.Clear();
                Ventas serv = new Ventas();
                serv.Url = new Juddi().getServiceUrl("Ventas");
                jsonVentas = serv.getVentasActivas(user.id);
                lv = javaScriptSerializer.Deserialize<List<Venta>>(jsonVentas);
                for (int i = 0; i < lv.Count; i++)
                {
                    string fecha = lv[i].fecha_F;
                    if (Convert.ToDateTime(fecha) < DateTime.UtcNow)
                    {
                        lstVentas.Items.Add(new ItemPuja { id = lv[i].id, negociado = lv[i].negociado, tipo = lv[i].tipo, tiempoActual = "FIN", pujaMax = lv[i].precio });
                    }
                    else
                    {
                        DateTime utcDate = DateTime.SpecifyKind(Convert.ToDateTime(lv[i].fecha_F), DateTimeKind.Utc);
                        var localTime = utcDate.ToLocalTime();
                        lstVentas.Items.Add(new ItemPuja { id = lv[i].id, negociado = lv[i].negociado, tipo = lv[i].tipo, tiempoActual = localTime.ToString(), pujaMax = lv[i].precio });
                    }
                }
            }));

        }


        private void btnAñadirVenta_Click(object sender, RoutedEventArgs e)
        {
            bool validData = true;
            Venta venta = new Venta();
            Auxiliar aux = new Auxiliar();
            string jsonVenta = "";
            
            //Comprobar que las horas y minutos son valores correctos
            int i = 0;
            bool success = int.TryParse(tbHora.Text, out i);
            if (!success)
            {
                validData = false;
                lbMessage.Foreground = Brushes.Red;
                lbMessage.Content = "El campo 'Hora' debe contener un valor numérico";
            }
            else
            {
                if (int.Parse(tbHora.Text) < 0 || int.Parse(tbHora.Text) > 23)
                {
                    validData = false;
                    lbMessage.Foreground = Brushes.Red;
                    lbMessage.Content = "El campo 'Hora' debe estar comprendido entre 0 y 23";
                }
            }

            success = int.TryParse(tbMinutos.Text, out i);
            if (!success)
            {
                validData = false;
                lbMessage.Foreground = Brushes.Red;
                lbMessage.Content = "El campo 'Minutos' debe contener un valor numérico";
            }
            else
            {
                if (int.Parse(tbMinutos.Text) < 0 || int.Parse(tbMinutos.Text) > 60)
                {
                    validData = false;
                    lbMessage.Foreground = Brushes.Red;
                    lbMessage.Content = "El campo 'Minutos' debe estar comprendido entre 0 y 60";
                }
            }

            DateTime date = new DateTime() ;
            DateTime? dateAux = datePicker.SelectedDate;
            if (dateAux != null)
            {
                date = datePicker.SelectedDate.Value.Date;
                if (validData)
                {
                    date = date.AddHours(int.Parse(tbHora.Text)).AddMinutes(int.Parse(tbMinutos.Text));
                }
            }
            else
            {
                validData = false;
                lbMessage.Foreground = Brushes.Red;
                lbMessage.Content = "Debes escoger una fecha";
            }


            Ventas serv = new Ventas();
            serv.Url = new Juddi().getServiceUrl("Ventas");
            var javaScriptSerializer = new JavaScriptSerializer();

            //Comprobación de campos
            //Año numérico
            success = int.TryParse(tbAño.Text, out i);
            if (!success)
            {
                validData = false;
                lbMessage.Foreground = Brushes.Red;
                lbMessage.Content = "El campo 'Año' debe contener un valor numérico";
            }

            //Fecha posterior a la actual
            int result = DateTime.Compare(date, DateTime.Now);
            if (result < 0)
            {
                validData = false;
                lbMessage.Foreground = Brushes.Red;
                lbMessage.Content = "La fecha indicada ya ha pasado";
            }

            //Precio numérico
            success = int.TryParse(tbPrecio.Text, out i);
            if (!success)
            {
                validData = false;
                lbMessage.Foreground = Brushes.Red;
                lbMessage.Content = "El campo 'Precio' debe contener un valor numérico";
            }

            if (validData)
            {
                int negociado = 0;
                if (cbNegociado.Text == "Automático")
                {
                    negociado = 1;
                }
                else
                {
                    negociado = 2;
                }

                venta.vendedor = user.id;
                venta.tipo = tbTipo.Text;
                venta.autor = tbAutor.Text;
                venta.año = int.Parse(tbAño.Text);
                venta.estado = tbEstado.Text;
                venta.fecha_F = date.ToString();
                venta.precio = int.Parse(tbPrecio.Text);
                venta.negociado = negociado;
                venta.idComprador = 0;

                jsonVenta = javaScriptSerializer.Serialize(venta);
                serv.nuevaVenta(jsonVenta);
                lbMessage.Foreground = Brushes.Green;
                lbMessage.Content = "Venta añadida correctamente";
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ItemPuja p = button.DataContext as ItemPuja;
            int id = p.id;
            Ventas serv = new Ventas();
            serv.Url = new Juddi().getServiceUrl("Ventas");
            serv.finalizarVenta(id, 2, 0);
        }

        private void btnVender_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ItemPuja p = button.DataContext as ItemPuja;
            int id = p.id;
            Debug.WriteLine(p.negociado);
            if (p.negociado == 2) //Negociado Manual
            {
                VentanaListaPujas vp = new VentanaListaPujas(p, user);
                vp.Show();
            }
            else
            {
                Ventas serv = new Ventas();
                serv.Url = new Juddi().getServiceUrl("Ventas");
                serv.finalizarVenta(id, 1, 0);
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ItemPuja p = button.DataContext as ItemPuja;
            int id = p.id;

            VentanaEdicion ve = new VentanaEdicion(p);
            ve.Show();
            
            //Ventas serv = new Ventas();
            //serv.Url = new Juddi().getServiceUrl("Ventas");
            //serv.finalizarVenta(id, 2, 0);
        }

    }
}
