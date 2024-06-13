using KomalliClienteEscritorio.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliClienteEscritorio.Productos.Model
{
    public class Producto : ISerializable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int PorcentajeDescuento { get; set; }
        public int CategoriaProductoId { get; set; }
    }
}
