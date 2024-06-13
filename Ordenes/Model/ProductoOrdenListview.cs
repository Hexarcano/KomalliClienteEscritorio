using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliClienteEscritorio.Ordenes.Model
{
    public class ProductoOrdenListview
    {
        public Guid OrdenId { get; set; }
        public int ProductoId { get; set; }
        public double PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public double SubtotalProductos { get; set; }
        public string NombreProducto { get; set; }
    }
}
