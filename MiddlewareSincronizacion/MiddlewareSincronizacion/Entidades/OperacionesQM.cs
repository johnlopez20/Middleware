using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class OperacionesQM
    {
        public string MATNR { get; set; }
        public string WERKS { get; set; }
        public string PLNTY { get; set; }
        public string PLNNR { get; set; }
        public string PLNAL { get; set; }
        public string ZKRIZ { get; set; }
        public string ZAEHL { get; set; }
        public string PLNFL { get; set; }
        public string PLNKN { get; set; }
        public string VORNR { get; set; }
        public string STEUS { get; set; }
        public string LTXA1 { get; set; }
        public string QPPKTABS { get; set; }
        public string MAKTX { get; set; }
        public string RENGLON { get; set; }

        public OperacionesQM()
        {
            MATNR = string.Empty;
            WERKS = string.Empty;
            PLNTY = string.Empty;
            PLNNR = string.Empty;
            PLNAL = string.Empty;
            ZKRIZ = string.Empty;
            ZAEHL = string.Empty;
            PLNFL = string.Empty;
            PLNKN = string.Empty;
            VORNR = string.Empty;
            STEUS = string.Empty;
            LTXA1 = string.Empty;
            QPPKTABS = string.Empty;
            MAKTX = string.Empty;
            RENGLON = string.Empty;
        }
    }
}
