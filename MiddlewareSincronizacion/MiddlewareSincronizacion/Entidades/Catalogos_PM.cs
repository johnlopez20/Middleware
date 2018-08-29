using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class Catalogos_PM
    {

        //INFORME
        public string I_FEART { get; set; }
        public string I_KURZTEXT_ES { get; set; }
        public string I_KURZTEXT_EN { get; set; }
        //TEXTOS
        public string T_CODEGRUPPE { get; set; }
        public string T_CODE { get; set; }
        public string T_KURZTEXT_ES { get; set; }
        public string T_KURZTEXT_EN { get; set; }
        //CLASES
        public string C_FEHLKLASSE { get; set; }
        public string C_KURZTEXT_ES { get; set; }
        public string C_KURZTEXT_EN { get; set; }
        //INSPECCION
        public string IN_CODEGRUPPE { get; set; }
        public string IN_CODE { get; set; }
        public string IN_KURZTEXT_ES { get; set; }
        public string IN_KURZTEXT_EN { get; set; }
        public string AUSWAHLMGE { get; set; }
        public string BEWERTUNG { get; set; }
        public Catalogos_PM()
        {
            I_FEART = string.Empty;
            I_KURZTEXT_ES = string.Empty;
            I_KURZTEXT_EN = string.Empty;
            T_CODEGRUPPE = string.Empty;
            T_CODE = string.Empty;
            T_KURZTEXT_ES = string.Empty;
            T_KURZTEXT_EN = string.Empty;
            C_FEHLKLASSE = string.Empty;
            C_KURZTEXT_ES = string.Empty;
            C_KURZTEXT_EN = string.Empty;
            IN_CODEGRUPPE = string.Empty;
            IN_CODE = string.Empty;
            IN_KURZTEXT_ES = string.Empty;
            IN_KURZTEXT_EN = string.Empty;
            AUSWAHLMGE = string.Empty;
            BEWERTUNG = string.Empty;
        }
    }
}
