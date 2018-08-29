using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class CalidadPosNot
    {
        public string FOLIO_SAM { get; set; }
        public string RECIBIDO { get; set; }
        public CalidadPosNot()
        {
            FOLIO_SAM = string.Empty;
            RECIBIDO = string.Empty;
        }
    }
}
