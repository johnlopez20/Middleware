using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class PosAvisosSAPModificaciones
    {
        public string FOLIO_SAM { get; set; }
        public string QMNUM { get; set; }
        public string RENGLON { get; set; }
        public string FECHA { get; set; }
        public string HORA { get; set; }
        public string REFOBJECTKEY { get; set; }
        public string ACT_KEY { get; set; }
        public string ACT_SORT_NO { get; set; }
        public string ACTTEXT { get; set; }
        public string ACT_CODEGRP { get; set; }
        public string ACT_CODE { get; set; }
        public string START_DATE { get; set; }
        public string START_TIME { get; set; }
        public string END_DATE { get; set; }
        public string END_TIME { get; set; }
        public string ITEM_SORT_NO { get; set; }
        public string RECIBIDO { get; set; }
        public string PROCESADO { get; set; }
        public string MENSAJE { get; set; }
        public string HORA_RECIBIDO { get; set; }
        public string FECHA_RECIBIDO { get; set; }
        public string UNAME { get; set; }
        public PosAvisosSAPModificaciones()
        {
            FOLIO_SAM = string.Empty;
            QMNUM = string.Empty;
            RENGLON = string.Empty;
            FECHA = string.Empty;
            HORA = string.Empty;
            REFOBJECTKEY = string.Empty;
            ACT_KEY = string.Empty;
            ACT_SORT_NO = string.Empty;
            ACTTEXT = string.Empty;
            ACT_CODEGRP = string.Empty;
            ACT_CODE = string.Empty;
            START_DATE = string.Empty;
            START_TIME = string.Empty;
            END_DATE = string.Empty;
            END_TIME = string.Empty;
            ITEM_SORT_NO = string.Empty;
            RECIBIDO = string.Empty;
            PROCESADO = string.Empty;
            MENSAJE = string.Empty;
            HORA_RECIBIDO = string.Empty;
            FECHA_RECIBIDO = string.Empty;
            UNAME = string.Empty;
        }
    }
}
