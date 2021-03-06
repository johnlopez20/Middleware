﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class CabNotificacionesCrea
    {
        public string FOLIO_SAM { get; set; }
        public string FOLIO_ORD { get; set; }
        public string AUFNR { get; set; }
        public string WERKS { get; set; }
        public string VORNR { get; set; }
        public string RMZHL { get; set; }
        public string UZEIT { get; set; }
        public string DATUM { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string ERROR { get; set; }
        public string FECHA_RECIBIDO { get; set; }
        public string HORA_RECIBIDO { get; set; }

        public CabNotificacionesCrea()
        {
            FOLIO_SAM = string.Empty;
            FOLIO_ORD = string.Empty;
            AUFNR = string.Empty;
            WERKS = string.Empty;
            VORNR = string.Empty;
            RMZHL = string.Empty;
            UZEIT = string.Empty;
            DATUM = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            ERROR = string.Empty;
            FECHA_RECIBIDO = string.Empty;
            HORA_RECIBIDO = string.Empty;
        }
    }
}
