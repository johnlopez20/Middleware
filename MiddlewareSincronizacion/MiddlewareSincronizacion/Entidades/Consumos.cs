using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Consumos
    {
        public string FOLIO_SAP { get; set; }
        public string FOLIO_ORD { get; set; }
        public string FOLIO_SAM { get; set; }
        public string UZEIT { get; set; }
        public string DATUM { get; set; }
        public string AUFNR { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string ERROR { get; set; }
        public string WERKS { get; set; }

        public Consumos()
        {
            FOLIO_SAP = string.Empty;
            FOLIO_ORD = string.Empty;
            FOLIO_SAM = string.Empty;
            UZEIT = string.Empty;
            DATUM = string.Empty;
            AUFNR = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            ERROR = string.Empty;
            WERKS = string.Empty;
        }                
    }
}
