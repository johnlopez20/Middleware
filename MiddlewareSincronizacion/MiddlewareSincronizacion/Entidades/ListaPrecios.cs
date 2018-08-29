using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ListaPrecios
    {
        public string PLTYP {get;set;}
        public string PTEXT { get; set; }
        public ListaPrecios()
        {
            PLTYP = string.Empty;
            PTEXT = string.Empty;
        }
    }
}
