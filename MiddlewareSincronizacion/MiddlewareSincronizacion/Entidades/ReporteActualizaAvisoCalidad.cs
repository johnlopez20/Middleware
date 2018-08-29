using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ReporteActualizaAvisoCalidad
    {
        public string FOLIO_SAM { get; set; }
        public string NOTIF_NO { get; set; }
        public string TASK_KEY { get; set; }
        public string TASK_CODE { get; set; }
        public string UNAME { get; set; }
        public string DATUM { get; set; }
        public string UZEIT { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string MENSAJE { get; set; }
        public string HORA_RECIBIDO { get; set; }
        public string FECHA_RECIBIDO { get; set; }
        public string WERKS { get; set; }
        public ReporteActualizaAvisoCalidad()
        {
            FOLIO_SAM = string.Empty;
            NOTIF_NO = string.Empty;
            TASK_KEY = string.Empty;
            TASK_CODE = string.Empty;
            UNAME = string.Empty;
            DATUM = string.Empty;
            UZEIT = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            MENSAJE = string.Empty;
            HORA_RECIBIDO = string.Empty;
            FECHA_RECIBIDO = string.Empty;
            WERKS = string.Empty;
        }
    }
}
