using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class TextosCaracteristicasOrdenes
    {
        public string AUFNR { get; set; }
        public string PRUEFLOS { get; set; }
        public string MERKNR { get; set; }
        public string TDLINE { get; set; }
        public TextosCaracteristicasOrdenes()
        {
            AUFNR = string.Empty;
            PRUEFLOS = string.Empty;
            MERKNR = string.Empty;
            TDLINE = string.Empty;
        }
    }
}
