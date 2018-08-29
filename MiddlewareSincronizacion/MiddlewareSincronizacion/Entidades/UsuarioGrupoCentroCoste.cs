using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class UsuarioGrupoCentroCoste
    {
        public string PERNR { get; set; }
        public string ZSAM_GK { get; set; }
        public UsuarioGrupoCentroCoste()
        {
            PERNR = string.Empty;
            ZSAM_GK = string.Empty;
        }
    }
}
