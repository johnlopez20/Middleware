using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class PM03_1
    {
        public string AUFNR { get; set; }
        public string EXTROW { get; set; }
        public string SRVPOS { get; set; }
        public string KTEXT1 { get; set; }
        public string MENGE { get; set; }
        public string MEINS { get; set; }
        public string NETWR { get; set; }
        public string PEINH { get; set; }
        public string MATKL { get; set; }
        public string KSTAR { get; set; }

        public PM03_1()
        {
            AUFNR = string.Empty;
            EXTROW = string.Empty;
            SRVPOS = string.Empty;
            KTEXT1 = string.Empty;
            MENGE = string.Empty;
            MEINS = string.Empty;
            NETWR = string.Empty;
            PEINH = string.Empty;
            MATKL = string.Empty;
            KSTAR = string.Empty;
        }
    }
}
