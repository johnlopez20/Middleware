using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class OBYC
    {
        public string KTOPL { get; set; }
        public string KTOSL { get; set; }
        public string KOMOK { get; set; }
        public string BKLAS { get; set; }
        public string KONTS { get; set; }

        public OBYC()
        {
            KTOPL = string.Empty;
            KTOSL = string.Empty;
            KOMOK = string.Empty;
            BKLAS = string.Empty;
            KONTS = string.Empty;
        }
    }
}
