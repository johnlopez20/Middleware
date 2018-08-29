using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class EstatusCreacion
    {
        public string WERKS { get; set; }
        public string ESTATUS { get; set; }
        public EstatusCreacion()
        {
            WERKS = string.Empty;
            ESTATUS = string.Empty;
        }
    }
}
