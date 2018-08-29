using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ReporteDefectos
    {
        public string FOLIO_SAM { get; set; }
        public string PRUEFLOS { get; set; }
        public string WERKS { get; set; }
        public string DATUM { get; set; }
        public string MATNR { get; set; }
        public string UZEIT { get; set; }
        public string EBELN { get; set; }
        public string FOLIO_MOV { get; set; }
        public string MBLNR { get; set; }
        public string FEART { get; set; }
        public string QKURZTEXT { get; set; }
        public string RBNR { get; set; }
        public string RBNRX { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string MENSAJE { get; set; }
        public string FECHA_RECIBIDO { get; set; }
        public string HORA_RECIBIDO { get; set; }
        public string PRUEFER { get; set; }
        public string UNAME { get; set; }
        public ReporteDefectos()
        {
            FOLIO_SAM = string.Empty;
            PRUEFLOS = string.Empty;
            WERKS = string.Empty;
            DATUM = string.Empty;
            MATNR = string.Empty;
            UZEIT = string.Empty;
            EBELN = string.Empty;
            FOLIO_MOV = string.Empty;
            MBLNR = string.Empty;
            FEART = string.Empty;
            QKURZTEXT = string.Empty;
            RBNR = string.Empty;
            RBNRX = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            MENSAJE = string.Empty;
            FECHA_RECIBIDO = string.Empty;
            HORA_RECIBIDO = string.Empty;
            PRUEFER = string.Empty;
            UNAME = string.Empty;
        }
    }
}
