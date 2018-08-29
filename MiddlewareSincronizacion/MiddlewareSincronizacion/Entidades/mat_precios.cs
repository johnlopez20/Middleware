using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class mat_precios
    {
        public string material { get; set; }
        public string antiguo { get; set; }
        public string centro { get; set; }
        public string texto_breve { get; set; }

        public mat_precios()
        {
            material = string.Empty;
            antiguo = string.Empty;
            centro = string.Empty;
            texto_breve = string.Empty;
        }
    }
}
