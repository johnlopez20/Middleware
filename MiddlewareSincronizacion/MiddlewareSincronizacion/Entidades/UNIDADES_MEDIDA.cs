using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class UNIDADES_MEDIDA
    {
        public string MSEHI { get; set; }
        public string MSEHL_ES { get; set; }
        public string MSEHL_EN { get; set; }
        public string DECIMALES { get; set; }

        public UNIDADES_MEDIDA()
        {
            MSEHI = string.Empty;
            MSEHL_ES = string.Empty;
            MSEHL_EN = string.Empty;
            DECIMALES = string.Empty;
        }
    }
}
