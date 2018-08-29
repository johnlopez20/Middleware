using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class QM01_1
    {
        public string AUFNR { get; set; }
        public string MERKNR { get; set; }
        public string KURZTEXT { get; set; }
        public string MASSEINHSW { get; set; }
        public string ISTSTPUMF { get; set; }
        public string ISTSTPANZ { get; set; }

        public QM01_1()
        {
            AUFNR = string.Empty;
            MERKNR = string.Empty;
            KURZTEXT = string.Empty;
            MASSEINHSW = string.Empty;
            ISTSTPUMF = string.Empty;
            ISTSTPANZ = string.Empty;
        }
    }
}
