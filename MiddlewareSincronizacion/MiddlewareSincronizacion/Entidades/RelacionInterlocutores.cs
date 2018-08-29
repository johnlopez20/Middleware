using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class RelacionInterlocutores
    {
        public string KUNNR { get; set; }
        public string VKORG { get; set; }
        public string VTWEG { get; set; }
        public string SPART { get; set; }
        public string PARVW { get; set; }
        public string KUNN2 { get; set; }
        public RelacionInterlocutores()
        {
            KUNNR = string.Empty;
            VKORG = string.Empty;
            VTWEG = string.Empty;
            SPART = string.Empty;
            PARVW = string.Empty;
            KUNN2 = string.Empty;
        }
    }
}
