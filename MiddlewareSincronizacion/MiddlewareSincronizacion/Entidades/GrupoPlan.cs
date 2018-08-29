using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class GrupoPlan
    {
        public string IWERK { get; set; }
        public string INGRP { get; set; }
        public string INNAM { get; set; }
        public string INTEL { get; set; }
        public string AUART_WP { get; set; }

        public GrupoPlan()
        {
            IWERK = string.Empty;
            INGRP = string.Empty;
            INNAM = string.Empty;
            INTEL = string.Empty;
            AUART_WP = string.Empty;
        }
    }
}
