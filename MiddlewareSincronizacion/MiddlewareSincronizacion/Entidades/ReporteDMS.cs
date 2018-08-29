using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ReporteDMS
    {
        public string FOLIO_DMS { get; set; }
        public string TABIX { get; set; }
        public string DOCNUMBER { get; set; }
        public string DATUM { get; set; }
        public string UZEIT { get; set; }
        public string WSAPPLICATION { get; set; }
        public string OBJECTTYPE { get; set; }
        public string DOCUMENTTYPE { get; set; }
        public string DESCRIPTION { get; set; }
        public string UNAME { get; set; }
        public string DOCFILE { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string MENSAJE { get; set; }
        public string FECHA_RECIBIDO { get; set; }
        public string HORA_RECIBIDO { get; set; }
        public string WERKS { get; set; }
        public ReporteDMS()
        {
            FOLIO_DMS = string.Empty;
            TABIX = string.Empty;
            DOCNUMBER = string.Empty;
            DATUM = string.Empty;
            UZEIT = string.Empty;
            WSAPPLICATION = string.Empty;
            OBJECTTYPE = string.Empty;
            DOCUMENTTYPE = string.Empty;
            DESCRIPTION = string.Empty;
            UNAME = string.Empty;
            DOCFILE = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            MENSAJE = string.Empty;
            FECHA_RECIBIDO = string.Empty;
            HORA_RECIBIDO = string.Empty;
            WERKS = string.Empty;
        }
    }
}
