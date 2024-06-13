using KomalliClienteEscritorio.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliClienteEscritorio.Login.Model
{
    public class LoginResponse : ISerializable
    {
        public string? TokenType { get; set; }
        public string? AccessToken { get; set; }
        public string? Mensaje { get; set; }
    }
}
