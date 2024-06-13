using KomalliClienteEscritorio.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliClienteEscritorio.Ordenes.Model
{
    public class ProductoOrdenRegistro : ISerializable
    {
        public int ProductoId { get; set; }
        public double PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public double SubtotalProductos { get; set; }
    }
}
