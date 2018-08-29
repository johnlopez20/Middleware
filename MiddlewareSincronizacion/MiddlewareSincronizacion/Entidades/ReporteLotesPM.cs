using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ReporteLotesPM
    {
        public string FOLIO_SAM { get; set; }
        public string PRUEFLOS { get; set; }
        public string DATUM { get; set; }
        public string UZEIT { get; set; }
        public string AUFNR { get; set; }
        public string VORNR { get; set; }
        public string KTEXTLOS { get; set; }
        public string WERK { get; set; }
        public string ERSTELLER { get; set; }
        public string ENSTEHDAT { get; set; }
        public string ENTSTEZEIT { get; set; }
        public string AENDERER { get; set; }
        public string AENDERDAT { get; set; }
        public string AENDERZEIT { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string MENSAJE { get; set; }
        public string FECHA_RECIBIDO { get; set; }
        public string HORA_RECIBIDO { get; set; }
        public string PRUEFER { get; set; }
        public string UNAME { get; set; }
        public string FOLIO_ORD { get; set; }
        public ReporteLotesPM()
        {
            FOLIO_SAM = string.Empty;
            PRUEFLOS = string.Empty;
            DATUM = string.Empty;
            UZEIT = string.Empty;
            AUFNR = string.Empty;
            VORNR = string.Empty;
            KTEXTLOS = string.Empty;
            WERK = string.Empty;
            ERSTELLER = string.Empty;
            ENSTEHDAT = string.Empty;
            ENTSTEZEIT = string.Empty;
            AENDERER = string.Empty;
            AENDERDAT = string.Empty;
            AENDERZEIT = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            MENSAJE = string.Empty;
            FECHA_RECIBIDO = string.Empty;
            HORA_RECIBIDO = string.Empty;
            PRUEFER = string.Empty;
            UNAME = string.Empty;
            FOLIO_ORD = string.Empty;
        }
    }
}
