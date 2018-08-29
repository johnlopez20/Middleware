using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class FlujoSD
    {
        public string VBELN { get; set; }
        public string POSNR { get; set; }
        public string ERDAT { get; set; }
        public string WAERK { get; set; }
        public string VKORG { get; set; }
        public string VTWEG { get; set; }
        public string SPART { get; set; }
        public string VKGRP { get; set; }
        public string VKBUR { get; set; }
        public string AUART { get; set; }
        public string KUNNR { get; set; }
        public string KOSTL { get; set; }
        public string VGTYP { get; set; }
        public string XBLNR { get; set; }
        public string ZUONR { get; set; }
        public string MATNR { get; set; }
        public string MATWA { get; set; }
        public string PMATN { get; set; }
        public string CHARG { get; set; }
        public string MATKL { get; set; }
        public string ARKTX { get; set; }
        public string PRODH { get; set; }
        public string ZMENG { get; set; }
        public string ZIEME { get; set; }
        public string NETWR { get; set; }
        public string WERKS { get; set; }
        public string LGORT { get; set; }
        public string ROUTE { get; set; }
        public string AUFNR { get; set; }
        public string VBELV_LI { get; set; }
        public string POSNR_LI { get; set; }
        public string RFMNG { get; set; }
        public string MEINS { get; set; }
        public string VBELV_8 { get; set; }
        public string POSNR_8 { get; set; }
        public string RFMNG_8 { get; set; }
        public string MEINS_8 { get; set; }
        public string VBELV_R { get; set; }
        public string POSNR_R { get; set; }
        public string RFMNG_R { get; set; }
        public string MEINS_R { get; set; }
        public string VBELV_M { get; set; }
        public string POSNR_M { get; set; }
        public string RFMNG_M { get; set; }
        public string MEINS_M { get; set; }
        public string KUWEV_KUNNR { get; set; }
        public string LIKP_BTGEW { get; set; }
        public string LIKP_LDDAT { get; set; }
        public string LIKP_LIFEX { get; set; }
        public string LIKP_NTGEW { get; set; }
        public string LIKP_TDDAT { get; set; }
        public string LIKP_VSTEL { get; set; }
        public string LIPS_CHARG { get; set; }
        public string LIPS_GEWEI { get; set; }
        public string LIPSD_G_LFIMG { get; set; }
        public string TVROT_BEZEI { get; set; }
        public string VBAK_ERNAM { get; set; }
        public string VBKD_BSTKD { get; set; }
        public string VFKK_FKART { get; set; }
        public string VFKK_FKNUM { get; set; }
        public string VFKK_STABR { get; set; }
        public string VFKP_NETWR { get; set; }
        public string VFKP_EBELN { get; set; }
        public string VTTK_ADD01 { get; set; }
        public string VTTK_ADD02 { get; set; }
        public string VTTK_ADD03 { get; set; }
        public string VTTK_ADD04 { get; set; }
        public string VTTK_BFART { get; set; }
        public string VTTK_EXTI1 { get; set; }
        public string VTTK_EXTI2 { get; set; }
        public string VTTK_ROUTE { get; set; }
        public string VTTK_SHTYP { get; set; }
        public string VTTK_SIGNI { get; set; }
        public string VTTK_STTRG { get; set; }
        public string VTTK_TDLNR { get; set; }
        public string VTTK_TPBEZ { get; set; }
        public string VTTK_VSART { get; set; }
        public string ACTUAL_START_DATE { get; set; }
        public string ACTUAL_FINISH_DATE { get; set; }
        public string SYSTEM_STATUS { get; set; }
        public string KALAB { get; set; }
        public string FLAG { get; set; }
        public string NAME1 { get; set; }
        public string AUDAT { get; set; }
        public FlujoSD()
        {
            VBELN = string.Empty;
            POSNR = string.Empty;
            ERDAT = string.Empty;
            WAERK = string.Empty;
            VKORG = string.Empty;
            VTWEG = string.Empty;
            SPART = string.Empty;
            VKGRP = string.Empty;
            VKBUR = string.Empty;
            AUART = string.Empty;
            KUNNR = string.Empty;
            KOSTL = string.Empty;
            VGTYP = string.Empty;
            XBLNR = string.Empty;
            ZUONR = string.Empty;
            MATNR = string.Empty;
            MATWA = string.Empty;
            PMATN = string.Empty;
            CHARG = string.Empty;
            MATKL = string.Empty;
            ARKTX = string.Empty;
            PRODH = string.Empty;
            ZMENG = string.Empty;
            ZIEME = string.Empty;
            NETWR = string.Empty;
            WERKS = string.Empty;
            LGORT = string.Empty;
            ROUTE = string.Empty;
            AUFNR = string.Empty;
            VBELV_LI = string.Empty;
            POSNR_LI = string.Empty;
            RFMNG = string.Empty;
            MEINS = string.Empty;
            VBELV_8 = string.Empty;
            POSNR_8 = string.Empty;
            RFMNG_8 = string.Empty;
            MEINS_8 = string.Empty;
            VBELV_R = string.Empty;
            POSNR_R = string.Empty;
            RFMNG_R = string.Empty;
            MEINS_R = string.Empty;
            VBELV_M = string.Empty;
            POSNR_M = string.Empty;
            RFMNG_M = string.Empty;
            MEINS_M = string.Empty;
            KUWEV_KUNNR = string.Empty;
            LIKP_BTGEW = string.Empty;
            LIKP_LDDAT = string.Empty;
            LIKP_LIFEX = string.Empty;
            LIKP_NTGEW = string.Empty;
            LIKP_TDDAT = string.Empty;
            LIKP_VSTEL = string.Empty;
            LIPS_CHARG = string.Empty;
            LIPS_GEWEI = string.Empty;
            LIPSD_G_LFIMG = string.Empty;
            TVROT_BEZEI = string.Empty;
            VBAK_ERNAM = string.Empty;
            VBKD_BSTKD = string.Empty;
            VFKK_FKART = string.Empty;
            VFKK_FKNUM = string.Empty;
            VFKK_STABR = string.Empty;
            VFKP_NETWR = string.Empty;
            VFKP_EBELN = string.Empty;
            VTTK_ADD01 = string.Empty;
            VTTK_ADD02 = string.Empty;
            VTTK_ADD03 = string.Empty;
            VTTK_ADD04 = string.Empty;
            VTTK_BFART = string.Empty;
            VTTK_EXTI1 = string.Empty;
            VTTK_EXTI2 = string.Empty;
            VTTK_ROUTE = string.Empty;
            VTTK_SHTYP = string.Empty;
            VTTK_SIGNI = string.Empty;
            VTTK_STTRG = string.Empty;
            VTTK_TDLNR = string.Empty;
            VTTK_TPBEZ = string.Empty;
            VTTK_VSART = string.Empty;
            ACTUAL_START_DATE = string.Empty;
            ACTUAL_FINISH_DATE = string.Empty;
            SYSTEM_STATUS = string.Empty;
            KALAB = string.Empty;
            FLAG = string.Empty;
            NAME1 = string.Empty;
            AUDAT = string.Empty;
        }
    }
}
