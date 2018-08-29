using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Inforecords
    {
        public string VKORG { get; set; }
        public string VTWEG { get; set; }
        public string KUNNR { get; set; }
        public string MATNR { get; set; }
        public string POSTX { get; set; }
        public Inforecords()
        {
            VKORG = string.Empty;
            VTWEG = string.Empty;
            KUNNR = string.Empty;
            MATNR = string.Empty;
            POSTX = string.Empty;
        }
    }
}
