using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class StatusOrdenes
    {
        public string FOLIO_ORD { get; set; }
        public string FOLIO_SAM { get; set; }
        public string DATUM { get; set; }
        public string UZEIT { get; set; }
        public string AUFNR { get; set; }
        public string OPERACION { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string MENSAJE { get; set; }
        public string WERKS { get; set; }

        public StatusOrdenes()
        {
            FOLIO_ORD = string.Empty;
            FOLIO_SAM = string.Empty;
            DATUM = string.Empty;
            UZEIT = string.Empty;
            AUFNR = string.Empty;
            OPERACION = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            MENSAJE = string.Empty;
            WERKS = string.Empty;
        }
    }
}
