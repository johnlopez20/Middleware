using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class CentroBeneficio
    {
        public string KOKRS { get; set; }
        public string PRCTR { get; set; }
        public string KTEXT { get; set; }
        public string BUKRS { get; set; }

        public CentroBeneficio()
        {
            BUKRS = string.Empty;
            KOKRS = string.Empty;
            PRCTR = string.Empty;
            KTEXT = string.Empty;
        }
    }
}
