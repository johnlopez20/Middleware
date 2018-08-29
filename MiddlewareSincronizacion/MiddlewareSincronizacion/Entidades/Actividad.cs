using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Actividad
    {
        public string QMNUM { get; set; }
        public string FENUM { get; set; }
        public string MNGRP { get; set; }
        public string MNCOD { get; set; }
        public string KURZTEXT_ES { get; set; }
        public string KURZTEXT_EN { get; set; }
        public string MATXT { get; set; }
        public string MNGFA { get; set; }
        public string PSTER { get; set; }
        public string PSTUR { get; set; }
        public string PETER { get; set; }
        public string PETUR { get; set; }
        public string MANUM { get; set; }
        public string FOLIO_SAM { get; set; }

        public Actividad()
        {
            QMNUM = string.Empty;
            FENUM = string.Empty;
            MNGRP = string.Empty;
            MNCOD = string.Empty;
            KURZTEXT_ES = string.Empty;
            KURZTEXT_EN = string.Empty;
            MATXT = string.Empty;
            MNGFA = string.Empty;
            PSTER = string.Empty;
            PSTUR = string.Empty;
            PETER = string.Empty;
            PETUR = string.Empty;
            MANUM = string.Empty;
            FOLIO_SAM = string.Empty;
        }
    }
}
