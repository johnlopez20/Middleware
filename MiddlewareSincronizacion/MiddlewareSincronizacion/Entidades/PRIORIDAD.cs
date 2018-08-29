using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class PRIORIDAD
    {
        public string ARTPR { get; set; }
        public string PRIOK { get; set; }
        public string PRIOKX_ES { get; set; }
        public string PRIOKX_EN { get; set; }

        public PRIORIDAD()
        {
            ARTPR = string.Empty;
            PRIOK = string.Empty;
            PRIOKX_ES = string.Empty;
            PRIOKX_EN = string.Empty;
        }
    }
}
