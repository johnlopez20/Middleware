using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Sector
    {
        public string SPART { get; set; }
        public string VTEXT_ES { get; set; }
        public string VTEXT_EN { get; set; }

        public Sector()
        {
            SPART = string.Empty;
            VTEXT_ES = string.Empty;
            VTEXT_EN = string.Empty;
        }
    }
}
