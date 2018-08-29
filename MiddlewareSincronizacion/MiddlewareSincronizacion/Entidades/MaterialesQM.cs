using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class MaterialesQM
    {
        public string MATNR { get; set; }
        public string WERKS { get; set; }
        public string PLNTY { get; set; }
        public string PLNNR { get; set; }
        public string PLNAL { get; set; }
        public string ZKRIZ { get; set; }
        public string ZAEHL { get; set; }
        public string SPRAS { get; set; }
        public string MAKTX { get; set; }
        public string LOEKZ { get; set; }
        public string VERWE { get; set; }
        public string STATU { get; set; }

        public MaterialesQM()
        {
            MATNR = string.Empty;
            WERKS = string.Empty;
            PLNTY = string.Empty;
            PLNNR = string.Empty;
            PLNAL = string.Empty;
            ZKRIZ = string.Empty;
            ZAEHL = string.Empty;
            SPRAS = string.Empty;
            MAKTX = string.Empty;
            LOEKZ = string.Empty;
            VERWE = string.Empty;
            STATU = string.Empty;
        }
    }
}
