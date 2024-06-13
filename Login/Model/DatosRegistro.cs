using KomalliClienteEscritorio.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliClienteEscritorio.Login.Model
{
    public class DatosRegistro : ISerializable
    {
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public string Email { get; set; }
    }
}
