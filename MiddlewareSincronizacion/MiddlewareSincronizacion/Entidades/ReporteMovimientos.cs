using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ReporteMovimientos
    {
        public string FOLIO_SAM { get; set; }
        public string UZEIT { get; set; }
        public string DATUM { get; set; }
        public string FOLIO_SAP { get; set; }
        public string WERKS { get; set; }
        public string LGORT { get; set; }
        public string ANULA { get; set; }
        public string TEXTO { get; set; }
        public string TEXCAB { get; set; }
        public string TIPO { get; set; }
        public string UMLGO { get; set; }
        public string FACTURA { get; set; }
        public string PEDIM { get; set; }
        public string LFSNR { get; set; }
        public string HU { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string ERROR { get; set; }
        public string IBORRADO { get; set; }

        public ReporteMovimientos()
        {
            FOLIO_SAM = string.Empty;
            UZEIT = string.Empty;
            DATUM = string.Empty;
            FOLIO_SAP = string.Empty;
            WERKS = string.Empty;
            LGORT = string.Empty;
            ANULA = string.Empty;
            TEXTO = string.Empty;
            TEXCAB = string.Empty;
            TIPO = string.Empty;
            UMLGO = string.Empty;
            FACTURA = string.Empty;
            PEDIM = string.Empty;
            LFSNR = string.Empty;
            HU = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            ERROR = string.Empty;
            IBORRADO = string.Empty;
        }
    }
}
