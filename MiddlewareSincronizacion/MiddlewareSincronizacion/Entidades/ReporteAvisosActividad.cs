using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ReporteAvisosActividad
    {
        public string FOLIO_SAM { get; set; }
        public string QMNUM { get; set; }
        public string FECHA { get; set; }
        public string HORA { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string MENSAJE { get; set; }
        public string HORA_RECIBIDO { get; set; }
        public string FECHA_RECIBIDO { get; set; }
        public string UNAME { get; set; }
        public string WERKS { get; set; }
        public ReporteAvisosActividad()
        {
            FOLIO_SAM = string.Empty;
            QMNUM = string.Empty;
            FECHA = string.Empty;
            HORA = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            MENSAJE = string.Empty;
            HORA_RECIBIDO = string.Empty;
            FECHA_RECIBIDO = string.Empty;
            UNAME = string.Empty;
            WERKS = string.Empty;
        }
    }
}
