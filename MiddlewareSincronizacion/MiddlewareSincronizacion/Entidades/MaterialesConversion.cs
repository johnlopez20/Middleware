using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class MaterialesConversion
    {
        public string MATNR { get; set; }
        public string UMREZ { get; set; }
        public string MEINS { get; set; }
        public string UMREN { get; set; }
        public string BSTME { get; set; }
        public string LVORM { get; set; }
        public string VRKME { get; set; }
        public MaterialesConversion()
        {
            MATNR = string.Empty;
            UMREZ = string.Empty;
            MEINS = string.Empty;
            UMREN = string.Empty;
            BSTME = string.Empty;
            LVORM = string.Empty;
            VRKME = string.Empty;
        }
    }
}
