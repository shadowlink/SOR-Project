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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GaleriaArte.Clases;

namespace GaleriaArte
{
    /// <summary>
    /// Interaction logic for HistorialVentas.xaml
    /// </summary>
    /// 

    public class ItemVenta
    {
        public int id { get; set; }
        public string tipo { get; set; }
        public string estado { get; set; }
        public int precio { get; set; }
    }

    public partial class HistorialVentas : UserControl
    {

        Usuario user;

        public HistorialVentas(Usuario u)
        {
            user = new Usuario();
            user = u;
            InitializeComponent();
            actualizarVentas();
            colId.DisplayMemberBinding = new Binding("id");
            colTipo.DisplayMemberBinding = new Binding("tipo");
            colEstado.DisplayMemberBinding = new Binding("estado");
            colPrecio.DisplayMemberBinding = new Binding("precio");
        }

        public void actualizarVentas()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                Auxiliar aux = new Auxiliar();
                HistoricoVentas hv = new HistoricoVentas();
                string jsonVentas = "";
                Historico serv = new Historico();
                serv.Url = new Juddi().getServiceUrl("Historico");
                lvHistorico.Items.Clear();
                jsonVentas = serv.devolverTodo(user.id);
                var javaScriptSerializer = new JavaScriptSerializer();
                hv = javaScriptSerializer.Deserialize<HistoricoVentas>(jsonVentas);

                for (int i = 0; i < hv.ListaVentas.Count; i++)
                {
                    lvHistorico.Items.Add(new ItemVenta { id = hv.ListaVentas[i].id, tipo = hv.ListaVentas[i].tipo, estado = hv.ListaVentas[i].estado, precio = hv.ListaVentas[i].precio});
                }
            }));

        }

    }
}
