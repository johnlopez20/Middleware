using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ReporteLotesInsp
    {
        public string FOLIO_SAM { get; set; }
        public string PRUEFLOS { get; set; }
        public string DATUM { get; set; }
        public string UZEIT { get; set; }
        public string MATNR { get; set; }
        public string MAKTX { get; set; }
        public string EBELN { get; set; }
        public string FOLIO_MOV { get; set; }
        public string MBLNR { get; set; }
        public string WERKS { get; set; }
        public string ERSTELLER { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string MENSAJE { get; set; }
        public string FECHA_RECIBIDO { get; set; }
        public string HORA_RECIBIDO { get; set; }
        public string PRUEFER { get; set; }
        public string UNAME { get; set; }
        public ReporteLotesInsp()
        {
            FOLIO_SAM = string.Empty;
            PRUEFLOS = string.Empty;
            DATUM = string.Empty;
            UZEIT = string.Empty;
            MATNR = string.Empty;
            MAKTX = string.Empty;
            EBELN = string.Empty;
            FOLIO_MOV = string.Empty;
            MBLNR = string.Empty;
            WERKS = string.Empty;
            ERSTELLER = string.Empty;
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
