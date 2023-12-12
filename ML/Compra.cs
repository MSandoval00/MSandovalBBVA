using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public ML.Producto Producto { get; set; }
        public int Total { get; set; }
        public int Ingreso{ get; set; }
        public int Cambio { get; set; }
        public List<object> Compras { get; set; }
    }
}
