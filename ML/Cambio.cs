using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Cambio
    {
        public int IdCambio { get; set; }
        public int Billetes100 { get; set; }
        public int Billetes50 { get; set; }
        public int Monedas10 { get; set; }
        public List<object> Cambios { get; set; }

    }
}
