using KomalliClienteEscritorio.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliClienteEscritorio.Categorias.Model
{
    public class Categoria : ISerializable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ImagenBase64 { get; set; }
    }
}
