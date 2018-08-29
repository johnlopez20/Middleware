using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ReporteReservas
    {
        public string FOLIO_SAM { get; set; }
        public string TABIX { get; set; }
        public string FOLIO_SAP { get; set; }
        public string DATUM { get; set; }
        public string UZEIT { get; set; }
        public string WERKS { get; set; }
        public string BWART { get; set; }
        public string LGORT { get; set; }
        public string KOSTL { get; set; }
        public string AUFNR { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string ERROR { get; set; }

        public ReporteReservas()
        {
            FOLIO_SAM = string.Empty;
            TABIX = string.Empty;
            FOLIO_SAP = string.Empty;
            DATUM = string.Empty;
            UZEIT = string.Empty;
            WERKS = string.Empty;
            BWART = string.Empty;
            LGORT = string.Empty;
            KOSTL = string.Empty;
            AUFNR = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            ERROR = string.Empty;
        }
    }
}
