using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class StatusNotificaciones
    {
        public string FOLIO_SAM { get; set; }
        public string FOLIO_ORD { get; set; }
        public string DATUM { get; set; }
        public string UZEIT { get; set; }
        public string AUFNR { get; set; }
        public string WERKS { get; set; }
        public string OPERACION { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string MENSAJE { get; set; }
        public string FDATUM { get; set; }
        public string FUZEIT { get; set; }
        public string NEWCHARG { get; set; }
        public string FECHA_RECIBIDO { get; set; }
        public string HORA_RECIBIDO { get; set; }

        public StatusNotificaciones()
        {
            FOLIO_SAM = string.Empty;
            FOLIO_ORD = string.Empty;
            DATUM = string.Empty;
            UZEIT = string.Empty;
            AUFNR = string.Empty;
            WERKS = string.Empty;
            OPERACION = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            MENSAJE = string.Empty;
            FDATUM = string.Empty;
            FUZEIT = string.Empty;
            NEWCHARG = string.Empty;
            FECHA_RECIBIDO = string.Empty;
            HORA_RECIBIDO = string.Empty;
        }
    }
}
