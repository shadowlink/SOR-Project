using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSOR
{
    public class Usuario
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public int Privilegios { get; set; }
    }
}
