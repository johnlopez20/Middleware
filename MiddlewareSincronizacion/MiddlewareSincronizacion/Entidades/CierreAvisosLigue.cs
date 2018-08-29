using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class CierreAvisosLigue
    {
        public string FOLIO_SAM { get; set; }
        public string FOLIO_SAP { get; set; }
        public string AUFNR { get; set; }
        public string DATUM { get; set; }
        public string UZEIT { get; set; }
        public string FECHA_RECIBIDO { get; set; }
        public string HORA_RECIBIDO { get; set; }
        public string STATUS { get; set; }
        public string ESTATUS { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string ERROR { get; set; }
        public string UNAME { get; set; }
        public CierreAvisosLigue()
        {
            FOLIO_SAM = string.Empty;
            FOLIO_SAP = string.Empty;
            AUFNR = string.Empty;
            DATUM = string.Empty;
            UZEIT = string.Empty;
            FECHA_RECIBIDO = string.Empty;
            HORA_RECIBIDO = string.Empty;
            STATUS = string.Empty;
            ESTATUS = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            ERROR = string.Empty;
            UNAME = string.Empty;

        }
    }
}
