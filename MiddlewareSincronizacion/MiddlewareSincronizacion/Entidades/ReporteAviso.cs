using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ReporteAviso
    {
        public string FOLIO_SAM { get; set; }
        public string UZEIT { get; set; }
        public string DATUM { get; set; }
        public string IWERK { get; set; }
        public string QMART { get; set; }
        public string QMTXT { get; set; }
        public string EQUNR { get; set; }
        public string QMGRP { get; set; }
        public string QMCOD { get; set; }
        public string FOLIO_SAP { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string ERROR { get; set; }

        public ReporteAviso()
        {
            FOLIO_SAM = string.Empty;
            UZEIT = string.Empty;
            DATUM = string.Empty;
            IWERK = string.Empty;
            QMART = string.Empty;
            QMTXT = string.Empty;
            EQUNR = string.Empty;
            QMGRP = string.Empty;
            QMCOD = string.Empty;
            FOLIO_SAP = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            ERROR = string.Empty;
        }
    }
}
