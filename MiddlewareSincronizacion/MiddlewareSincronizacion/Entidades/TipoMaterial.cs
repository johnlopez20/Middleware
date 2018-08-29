using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class TipoMaterial
    {
        public string MTART { get; set; }
        public string MTBEZ_ES { get; set; }
        public string MTBEZ_EN { get; set; }

        public TipoMaterial()
        {
            MTART = string.Empty;
            MTBEZ_ES = string.Empty;
            MTBEZ_EN = string.Empty;
        }
    }
}
