using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class TextoPosicionesOrdenesVis
    {
        public string AUFNR { get; set; }
        public string FOLIO_SAM { get; set; }
        public string VORNR { get; set; }
        public string INDICE { get; set; }
        public string TDLINE { get; set; }
        public TextoPosicionesOrdenesVis()
        {
            AUFNR = string.Empty;
            FOLIO_SAM = string.Empty;
            VORNR = string.Empty;
            INDICE = string.Empty;
            TDLINE = string.Empty;
        }
    }
}
