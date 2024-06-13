using KomalliClienteEscritorio.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliClienteEscritorio.Ordenes.Model
{
    public class Orden : ISerializable
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public string NombreCliente { get; set; }
        public double PrecioTotal { get; set; }
        public DateTime FechaExpedicion { get; set; }
        public DateTime? FechaPago { get; set; }
        public bool Pagado { get; set; }
        public string Comentario { get; set; }
    }
}
