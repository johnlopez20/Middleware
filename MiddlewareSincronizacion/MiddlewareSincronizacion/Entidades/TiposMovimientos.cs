using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class TiposMovimientos
    {
        public string BWART { get; set; }
        public string BTEXT { get; set; }
        public TiposMovimientos()
        {
            BWART = string.Empty;
            BTEXT = string.Empty;
        }
    }
}
