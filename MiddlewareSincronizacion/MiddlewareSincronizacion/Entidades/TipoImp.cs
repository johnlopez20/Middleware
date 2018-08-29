using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class TipoImp
    {
        public string KNTTP { get; set; }
        public string KNTTX_ES { get; set; }
        public string KNTTX_EN { get; set; }

        public TipoImp()
        {
            KNTTP = string.Empty;
            KNTTX_ES = string.Empty;
            KNTTX_EN = string.Empty;
        }
    }
}
