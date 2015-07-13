using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_Pujas
{
    class Venta
    {
        public int id { get; set; }
        public int vendedor { get; set; }
        public string tipo { get; set; }
        public string autor { get; set; }
        public int año { get; set; }
        public string estado { get; set; }
        public DateTime fecha_I { get; set; }
        public DateTime fecha_F { get; set; }
        public int precio { get; set; }
        public int finalizada { get; set; }
        public string comprador { get; set; }
        public int negociado { get; set; }
        public int idComprador { get; set; }
    }
}
