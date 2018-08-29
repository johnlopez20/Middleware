using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Interlocutores
    {
        public string KUNNR { get; set; }
        public string VKORG { get; set; }
        public string VTWEG { get; set; }
        public string SPART { get; set; }
        public string PARVW { get; set; }
        public string PARZA { get; set; }
        public string KUNN2 { get; set; }
        public string LIFNR { get; set; }
        public string PERNR { get; set; }
        public string PARNR { get; set; }
        public string KNREF { get; set; }
        public string DEFPA { get; set; }
        public Interlocutores()
        {
            KUNNR = string.Empty;
            VKORG = string.Empty;
            VTWEG = string.Empty;
            SPART = string.Empty;
            PARVW = string.Empty;
            PARZA = string.Empty;
            KUNN2 = string.Empty;
            LIFNR = string.Empty;
            PERNR = string.Empty;
            PARNR = string.Empty;
            KNREF = string.Empty;
            DEFPA = string.Empty;
        }
    }
}
