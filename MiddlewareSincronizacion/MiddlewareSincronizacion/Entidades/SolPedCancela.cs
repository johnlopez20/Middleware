using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class SolPedCancela
    {
        public string PREQ_ITEM { get; set; }
        public string FOLIO_SAM { get; set; }
        public string FECHA_M { get; set; }
        public string HORA_M { get; set; }
        public string FOLIO_SAP { get; set; }
        public string DELETE_IND { get; set; }
        public string CLOSED { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string MESSAGE { get; set; }
        public string FECHA_R { get; set; }
        public string HORA_R { get; set; }
        public SolPedCancela()
        {
            PREQ_ITEM = string.Empty;
            FOLIO_SAM = string.Empty;
            FECHA_M = string.Empty;
            HORA_M = string.Empty;
            FOLIO_SAP = string.Empty;
            DELETE_IND = string.Empty;
            CLOSED = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            MESSAGE = string.Empty;
            FECHA_R = string.Empty;
            HORA_R = string.Empty;
        }
    }
}
