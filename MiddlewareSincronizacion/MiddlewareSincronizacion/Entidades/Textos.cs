using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Textos
    {
        public string QMNUM { get; set; }
        public string TEXTO { get; set; }
        public string FOIO_SAM { get; set; }

        public Textos()
        {
            QMNUM = string.Empty;
            TEXTO = string.Empty;
            FOIO_SAM = string.Empty;
        }
    }
}
