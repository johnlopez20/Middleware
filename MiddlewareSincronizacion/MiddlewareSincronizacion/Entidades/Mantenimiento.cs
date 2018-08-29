using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Mantenimiento
    {
        public string WARPL { get; set; }
        public string NUMMER { get; set; }
        public string OPERATOR { get; set; }
        public string ZYKL1 { get; set; }
        public string ZEIEH { get; set; }
        public string PAKTEXT_ES { get; set; }
        public string PAKTEXT_EN { get; set; }
        public string LANGU { get; set; }
        public string POINT { get; set; }
        public string OFFSET { get; set; }
        public string INAKTIV { get; set; }
        public string NZAEH { get; set; }
        public string ZAEHL { get; set; }
        public string KTEXTYZK_ES { get; set; }
        public string KTEXTYZK_EN { get; set; }
        public string CYCLESEQIND { get; set; }
        public string SETREPEATIND { get; set; }

        public Mantenimiento()
        {
            WARPL = string.Empty;
            NUMMER = string.Empty;
            OPERATOR = string.Empty;
            ZYKL1 = string.Empty;
            ZEIEH = string.Empty;
            PAKTEXT_ES = string.Empty;
            PAKTEXT_EN = string.Empty;
            LANGU = string.Empty;
            POINT = string.Empty;
            OFFSET = string.Empty;
            INAKTIV = string.Empty;
            NZAEH = string.Empty;
            ZAEHL = string.Empty;
            KTEXTYZK_ES = string.Empty;
            KTEXTYZK_EN = string.Empty;
            CYCLESEQIND = string.Empty;
            SETREPEATIND = string.Empty;
        }
    }
}
