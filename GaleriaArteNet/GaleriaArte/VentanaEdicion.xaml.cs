using GaleriaArte.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for VentanaEdicion.xaml
    /// </summary>
    public partial class VentanaEdicion : Window
    {
        ItemPuja puja;

        public VentanaEdicion(ItemPuja p)
        {
            puja = p;

            InitializeComponent();
        }

        private void btGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (tbHora.Text != "" && tbMin.Text != "")
            {
                if (int.Parse(tbHora.Text) < 24 && int.Parse(tbHora.Text) >= 0)
                {
                    if (int.Parse(tbMin.Text) < 60 && int.Parse(tbMin.Text) >= 0)
                    {
                        DateTime date = calendar.SelectedDate.Value.Date;
                        date = date.AddHours(int.Parse(tbHora.Text)).AddMinutes(int.Parse(tbMin.Text));
                        int result = DateTime.Compare(date, DateTime.Now);
                        if (result >= 0)
                        {
                            Ventas serv = new Ventas();
                            serv.Url = new Juddi().getServiceUrl("Ventas");
                            serv.editarVenta(puja.id, date);
                            this.Close();
                        }
                    }
                }
            }
        }
    }
}
