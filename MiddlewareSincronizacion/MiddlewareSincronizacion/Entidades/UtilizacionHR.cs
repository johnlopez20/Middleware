using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class UtilizacionHR
    {
        public string PLNST { get; set; }
        public string TXTH_ES { get; set; }
        public string TXTH_EN { get; set; }

        public UtilizacionHR()
        {
            PLNST = string.Empty;
            TXTH_ES = string.Empty;
            TXTH_EN = string.Empty;
        }
    }
}
