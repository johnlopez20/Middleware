﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ORDENES_CONTROL
    {
        public string AUFNR { get; set; }
        public string FOLIO_SAM { get; set; }
        public string ERNAM { get; set; }
        public string ERDAT { get; set; }
        public string AENAM { get; set; }
        public string AEDAT { get; set; }
        public string WERKS { get; set; }

        public ORDENES_CONTROL()
        {
            AUFNR = string.Empty;
            FOLIO_SAM = string.Empty;
            ERNAM = string.Empty;
            ERDAT = string.Empty;
            AENAM = string.Empty;
            AEDAT = string.Empty;
            WERKS = string.Empty;
        }
    }
}