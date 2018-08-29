using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class DecisionEmpleo
    {
        public string FOLIO_SAM { get; set; }
        public string PRUEFLOS { get; set; }
        public string WERKS { get; set; }
        public string AUFNR { get; set; }
        public string FOLIO_ORD { get; set; }
        public string DATUM { get; set; }
        public string UZEIT { get; set; }
        public string VCODE { get; set; }
        public string VCODEGRP { get; set; }
        public string TEXTO { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string MENSAJE { get; set; }
        public string FECHA_RECIBIDO { get; set; }
        public string HORA_RECIBIDO { get; set; }
        public string UNAME { get; set; }
        public DecisionEmpleo()
        {
            FOLIO_SAM = string.Empty;
            PRUEFLOS = string.Empty;
            WERKS = string.Empty;
            AUFNR = string.Empty;
            FOLIO_ORD = string.Empty;
            DATUM = string.Empty;
            UZEIT = string.Empty;
            VCODE = string.Empty;
            VCODEGRP = string.Empty;
            TEXTO = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            MENSAJE = string.Empty;
            FECHA_RECIBIDO = string.Empty;
            HORA_RECIBIDO = string.Empty;
            UNAME = string.Empty;
        }
    }
}
