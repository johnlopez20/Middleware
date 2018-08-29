using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class GrupoCodigos
    {
        public string KATALOGART { get; set; }
        public string CODEGRUPPE { get; set; }
        public string KURZTEXT_ES { get; set; }
        public string KURZTEXT_EN { get; set; }
        public string CODE { get; set; }
        public string KURZTEXT_E { get; set; }
        public string KURZTEXT_N { get; set; }

        public GrupoCodigos()
        {
            KATALOGART = string.Empty;
            CODEGRUPPE = string.Empty;
            KURZTEXT_ES = string.Empty;
            KURZTEXT_EN = string.Empty;
            CODE = string.Empty;
            KURZTEXT_E = string.Empty;
            KURZTEXT_N = string.Empty;
        }
    }
}
