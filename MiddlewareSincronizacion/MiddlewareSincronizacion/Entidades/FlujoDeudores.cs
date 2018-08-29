using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class FlujoDeudores
    {
        public string KUNNR { get; set; }
        public string GJAHR { get; set; }
        public string BUDAT { get; set; }
        public string WAERS { get; set; }
        public string MONAT { get; set; }
        public string DMBTR { get; set; }
        public string ZFBDT { get; set; }
        public string ZBD3T { get; set; }
        public string VBELN { get; set; }
        public string NAME1 { get; set; }
        public string VKGRP { get; set; }
        public string BEZEI { get; set; }
        public FlujoDeudores()
        {
            KUNNR = string.Empty;
            GJAHR = string.Empty;
            BUDAT = string.Empty;
            WAERS = string.Empty;
            MONAT = string.Empty;
            DMBTR = string.Empty;
            ZFBDT = string.Empty;
            ZBD3T = string.Empty;
            VBELN = string.Empty;
            NAME1 = string.Empty;
            VKGRP = string.Empty;
            BEZEI = string.Empty;
        }
    }
}
