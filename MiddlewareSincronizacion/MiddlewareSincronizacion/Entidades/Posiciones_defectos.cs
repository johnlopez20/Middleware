using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Posiciones_defectos
    {
        public string FOLIO_SAM { get; set; }
        public string POSICION { get; set; }
        public string PRUEFLOS { get; set; }
        public string DATUM { get; set; }
        public string UZEIT { get; set; }
        public string FEGRP { get; set; }
        public string FECOD { get; set; }
        public string TXTCDGR { get; set; }
        public string ANZFEHLER { get; set; }
        public string FEQKLAS { get; set; }
        public string OTGRP { get; set; }
        public string OTEIL { get; set; }
        public string TXTCDOT { get; set; }
        public string FETXT { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string MENSAJE { get; set; }
        public string FECHA_RECIBIDO { get; set; }
        public string HORA_RECIBIDO { get; set; }
        public Posiciones_defectos()
        {
            FOLIO_SAM = string.Empty;
            POSICION = string.Empty;
            PRUEFLOS = string.Empty;
            DATUM = string.Empty;
            UZEIT = string.Empty;
            FEGRP = string.Empty;
            FECOD = string.Empty;
            TXTCDGR = string.Empty;
            ANZFEHLER = string.Empty;
            FEQKLAS = string.Empty;
            OTGRP = string.Empty;
            OTEIL = string.Empty;
            TXTCDOT = string.Empty;
            FETXT = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            MENSAJE = string.Empty;
            FECHA_RECIBIDO = string.Empty;
            HORA_RECIBIDO = string.Empty;
        }
    }
}
