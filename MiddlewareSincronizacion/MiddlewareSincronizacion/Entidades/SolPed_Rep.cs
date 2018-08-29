using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class SolPed_Rep
    {
        public string FOLIO_SAM { get; set; }
        public string FOLIO_SAP { get; set; }
        public string MATERIAL { get; set; }
        public string DATUM { get; set; }
        public string UZEIT { get; set; }
        public string DOC_TYPE { get; set; }
        public string TEXTO_CAB { get; set; }
        public string SHORT_TEXT { get; set; }
        public string QUANTITY { get; set; }
        public string PLANT { get; set; }
        public string PREQ_DATE { get; set; }
        public string DELIV_DATE { get; set; }
        public string PREIS { get; set; }
        public string WAERS { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string ERROR { get; set; }

        public SolPed_Rep()
        {
            FOLIO_SAM = string.Empty;
            FOLIO_SAP = string.Empty;
            MATERIAL = string.Empty;
            DATUM = string.Empty;
            UZEIT = string.Empty;
            DOC_TYPE = string.Empty;
            TEXTO_CAB = string.Empty;
            SHORT_TEXT = string.Empty;
            QUANTITY = string.Empty;
            PLANT = string.Empty;
            PREQ_DATE = string.Empty;
            DELIV_DATE = string.Empty;
            PREIS = string.Empty;
            WAERS = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            ERROR = string.Empty;
        }
    }
}
