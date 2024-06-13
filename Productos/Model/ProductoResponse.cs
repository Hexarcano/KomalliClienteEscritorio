using KomalliClienteEscritorio.Categorias.Model;
using KomalliClienteEscritorio.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliClienteEscritorio.Productos.Model
{
    public class ProductoResponse : ISerializable
    {
        public string Mensaje { get; set; }
        public List<Producto>? Productos { get; set; }
    }
}
