using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class TextosAvisosCalidad
    {
        public string NOTIF_NO { get; set; }
        public string TASK_KEY { get; set; }
        public string TASK_SORT_NO { get; set; }
        public string LINEA { get; set; }
        public string TEXTO { get; set; }
        public string FOIO_SAM { get; set; }
        public TextosAvisosCalidad()
        {
            NOTIF_NO = string.Empty;
            TASK_KEY = string.Empty;
            TASK_SORT_NO = string.Empty;
            LINEA = string.Empty;
            TEXTO = string.Empty;
            FOIO_SAM = string.Empty;
        }
    }
}
