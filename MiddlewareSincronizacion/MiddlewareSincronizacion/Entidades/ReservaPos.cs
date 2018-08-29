using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ReservaPos
    {
        public string FOLIO_SAM { get; set; }
        public string RSNUM { get; set; }
        public string MATNR { get; set; }
        public string WERKS { get; set; }
        public string LGORT { get; set; }
        public string BDMNG { get; set; }
        public string MEINS { get; set; }
        public string KOSTL { get; set; }
        public string AUFNR { get; set; }
        public string BWART { get; set; }
        public string SGTXT { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string ERROR { get; set; }
        public string FECHA_RECIBIDO { get; set; }
        public string HORA_RECIBIDO { get; set; }
        public string UMLGO { get; set; }

        public ReservaPos()
        {
            FOLIO_SAM = string.Empty;
            RSNUM = string.Empty;
            MATNR = string.Empty;
            WERKS = string.Empty;
            LGORT = string.Empty;
            BDMNG = string.Empty;
            MEINS = string.Empty;
            KOSTL = string.Empty;
            AUFNR = string.Empty;
            BWART = string.Empty;
            SGTXT = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            ERROR = string.Empty;
            FECHA_RECIBIDO = string.Empty;
            HORA_RECIBIDO = string.Empty;
            UMLGO = string.Empty;
        }
    }
}
