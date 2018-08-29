using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class OrganizacionCompras
    {
        public string WERKS { get; set; }
        public string EKORG { get; set; }
        public string EKOTX_ES { get; set; }
        public string EKOTX_EN { get; set; }

        public OrganizacionCompras()
        {
            WERKS = string.Empty;
            EKORG = string.Empty;
            EKOTX_ES = string.Empty;
            EKOTX_EN = string.Empty;
        }
    }
}
