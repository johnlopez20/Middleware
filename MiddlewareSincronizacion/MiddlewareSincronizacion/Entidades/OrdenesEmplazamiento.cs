using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class OrdenesEmplazamiento
    {
        public string AUFNR { get; set; }
        public string FOLIO_SAM { get; set; }
        public string SWERK { get; set; }
        public string STORT { get; set; }
        public string MSGRP { get; set; }
        public string BEBER { get; set; }
        public string ARBPL { get; set; }
        public string ABCKZ { get; set; }
        public string EQFNR { get; set; }
        public string BUKRS { get; set; }
        public string ANLNR { get; set; }
        public string ANLUN { get; set; }
        public string KOSTL { get; set; }
        public string PROID { get; set; }
        public OrdenesEmplazamiento()
        {
            AUFNR = string.Empty;
            FOLIO_SAM = string.Empty;
            SWERK = string.Empty;
            STORT = string.Empty;
            MSGRP = string.Empty;
            BEBER = string.Empty;
            ARBPL = string.Empty;
            ABCKZ = string.Empty;
            EQFNR = string.Empty;
            BUKRS = string.Empty;
            ANLNR = string.Empty;
            ANLUN = string.Empty;
            KOSTL = string.Empty;
            PROID = string.Empty;
        }
    }
}
