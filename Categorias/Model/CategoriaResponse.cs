using KomalliClienteEscritorio.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliClienteEscritorio.Categorias.Model
{
    public class CategoriaResponse : ISerializable
    {
        public string Mensaje { get; set; }
        public List<Categoria>? Categorias { get; set; }
    }
}
