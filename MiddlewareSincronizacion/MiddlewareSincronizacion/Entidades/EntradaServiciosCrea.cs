using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class EntradaServiciosCrea
    {
        public string FOLIO_SAM { get; set; }
        public string EBELN { get; set; }
        public string EBELP { get; set; }
        public string TABIX { get; set; }
        public string WERKS { get; set; }
        public string SERVICE { get; set; }
        public string QUANTITY { get; set; }
        public string SHORTTEX { get; set; }
        public string GR_PRICE { get; set; }
        public string PRICEUNI { get; set; }
        public string DATUM { get; set; }
        public string UZEIT { get; set; }
        public string FOLIO_SAP { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string ERROR { get; set; }
        public string EXTROW { get; set; }


        public EntradaServiciosCrea()
        {
            FOLIO_SAM = string.Empty;
            EBELN = string.Empty;
            EBELP = string.Empty;
            TABIX = string.Empty;
            WERKS = string.Empty;
            SERVICE = string.Empty;
            QUANTITY = string.Empty;
            SHORTTEX = string.Empty;
            GR_PRICE = string.Empty;
            PRICEUNI = string.Empty;
            DATUM = string.Empty;
            UZEIT = string.Empty;
            FOLIO_SAP = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            ERROR = string.Empty;
            EXTROW = string.Empty;
        }
    }
}
