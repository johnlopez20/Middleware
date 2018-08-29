using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class TextoPosicionOrdenesCrea
    {
        public string FOLIO_SAM { get; set; }
        public string VORNR { get; set; }
        public string INDICE { get; set; }
        public string TDFORMAT { get; set; }
        public string TDLINE { get; set; }
        public TextoPosicionOrdenesCrea()
        {
            FOLIO_SAM = string.Empty;
            VORNR = string.Empty;
            INDICE = string.Empty;
            TDFORMAT = string.Empty;
            TDLINE = string.Empty;
        }
    }
}
