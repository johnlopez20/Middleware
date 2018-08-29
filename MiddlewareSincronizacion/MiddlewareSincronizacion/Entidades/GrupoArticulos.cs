using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class GrupoArticulos
    {
        public string MATKL { get; set; }
        public string WGBEZ_ES { get; set; }
        public string WGBEZ_EN { get; set; }
        public string WGBEZ60_ES { get; set; }
        public string WGBEZ60_EN { get; set; }

        public GrupoArticulos()
        {
            MATKL = string.Empty;
            WGBEZ_ES = string.Empty;
            WGBEZ_EN = string.Empty;
            WGBEZ60_ES = string.Empty;
            WGBEZ60_EN = string.Empty;
        }
    }
}
