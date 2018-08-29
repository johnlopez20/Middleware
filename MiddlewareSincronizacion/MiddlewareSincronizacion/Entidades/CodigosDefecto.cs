using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class CodigosDefecto
    {
        public string KATALOGART { get; set; }
        public string CODEGRUPPE { get; set; }
        public string CODE { get; set; }
        public string KURZTEXT { get; set; }
        public CodigosDefecto()
        {
            KATALOGART = string.Empty;
            CODEGRUPPE = string.Empty;
            CODE = string.Empty;
            KURZTEXT = string.Empty;
        }
    }
}
