using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ResponsableTrab
    {
        public string VERAN { get; set; }
        public string WERKS { get; set; }
        public string ARBPL { get; set; }
        public string KTEXT_UP { get; set; }
        public string KTEXT { get; set; }
        public string SPRAS { get; set; }

        public ResponsableTrab()
        {
            VERAN = string.Empty;
            WERKS = string.Empty;
            ARBPL = string.Empty;
            KTEXT_UP = string.Empty;
            KTEXT = string.Empty;
            SPRAS = string.Empty;
        }
    }
}
