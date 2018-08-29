using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class UtilizacionL
    {
        public string STLAN { get; set; }
        public string PMPFE { get; set; }
        public string PMPKO { get; set; }
        public string PMPKA { get; set; }
        public string PMPRV { get; set; }
        public string PMPVS { get; set; }
        public string PMPIN { get; set; }
        public string PMPER { get; set; }
        public string ANTXT_ES { get; set; }
        public string ANTXT_EN { get; set; }

        public UtilizacionL()
        {
            STLAN = string.Empty;
            PMPFE = string.Empty;
            PMPKO = string.Empty;
            PMPKA = string.Empty;
            PMPRV = string.Empty;
            PMPVS = string.Empty;
            PMPIN = string.Empty;
            PMPER = string.Empty;
            ANTXT_ES = string.Empty;
            ANTXT_EN = string.Empty;
        }
    }
}
