using KomalliClienteEscritorio.Productos.Model;
using KomalliClienteEscritorio.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliClienteEscritorio.Ordenes.Model
{
    public class OrdenResponse : ISerializable
    {
        public string Mensaje { get; set; }
        public List<Orden>? Ordenes { get; set; }
        public List<ProductoOrden>? Productos { get; set; }
        
    }
}
