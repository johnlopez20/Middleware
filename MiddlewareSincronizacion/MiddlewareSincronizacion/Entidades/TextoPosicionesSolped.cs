using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class TextoPosicionesSolped
    {
        public string FOLIO_SAM { get; set; }
        public string FOLIO_SAP { get; set; }
        public string PREQ_ITEM { get; set; }
        public string INDICE { get; set; }
        public string TDFORMAT { get; set; }
        public string TDLINE { get; set; }
        public TextoPosicionesSolped()
        {
            FOLIO_SAM = string.Empty;
            FOLIO_SAP = string.Empty;
            PREQ_ITEM = string.Empty;
            INDICE = string.Empty;
            TDFORMAT = string.Empty;
            TDLINE = string.Empty;
        }
    }
}
