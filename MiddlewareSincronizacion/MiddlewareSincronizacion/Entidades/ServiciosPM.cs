using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareSincronizacion.Entidades
{
    public class ServiciosPM
    {
        public string FOLIO_SAM { get; set; }
        public string AUFNR { get; set; }
        public string PACKNO { get; set; }
        public string INTROW { get; set; }
        public string EXTROW { get; set; }
        public string DEL { get; set; }
        public string SRVPOS { get; set; }
        public string RANG { get; set; }
        public string EXTGROUP { get; set; }
        public string PAQUETE { get; set; }
        public string SUB_PACKNO { get; set; }
        public string LBNUM { get; set; }
        public string AUSGB { get; set; }
        public string STLVPOS { get; set; }
        public string EXTSRVNO { get; set; }
        public string MENGE { get; set; }
        public string MEINS { get; set; }
        public string UEBTO { get; set; }
        public string UEBTK { get; set; }
        public string WITH_LIM { get; set; }
        public string SPINF { get; set; }
        public string PEINH { get; set; }
        public string BRTWR { get; set; }
        public string NETWR { get; set; }
        public string FROMPOS { get; set; }
        public string TOPOS { get; set; }
        public string KTEXT1 { get; set; }
        public string VRTKZ { get; set; }
        public string TWRKZ { get; set; }
        public string PERNR { get; set; }
        public string MOLGA { get; set; }
        public string LGART { get; set; }
        public string LGTXT { get; set; }
        public string STELL { get; set; }
        public string IFTNR { get; set; }
        public string BUDAT { get; set; }
        public string INSDT { get; set; }
        public string PLN_PACKNO { get; set; }
        public string PLN_INTROW { get; set; }
        public string KNT_PACKNO { get; set; }
        public string KNT_INTROW { get; set; }
        public string TMP_PACKNO { get; set; }
        public string TMP_INTROW { get; set; }
        public string STLV_LIM { get; set; }
        public string LIMIT_ROW { get; set; }
        public string ACT_MENGE { get; set; }
        public string ACT_WERT { get; set; }
        public string KNT_WERT { get; set; }
        public string KNT_MENGE { get; set; }
        public string ZIELWERT { get; set; }
        public string UNG_WERT { get; set; }
        public string UNG_MENGE { get; set; }
        public string ALT_INTROW { get; set; }
        public string BASIC { get; set; }
        public string ALTERNAT { get; set; }
        public string BIDDER { get; set; }
        public string SUPPLE { get; set; }
        public string FREEQTY { get; set; }
        public string INFORM { get; set; }
        public string PAUSCH { get; set; }
        public string EVENTUAL { get; set; }
        public string MWSKZ { get; set; }
        public string TXJCD { get; set; }
        public string PRS_CHG { get; set; }
        public string MATKL { get; set; }
        public string TBTWR { get; set; }
        public string NAVNW { get; set; }
        public string BASWR { get; set; }
        public string KKNUMV { get; set; }
        public string IWEIN { get; set; }
        public string INT_WORK { get; set; }
        public string EXTERNALID { get; set; }
        public string KSTAR { get; set; }
        public string ACT_WORK { get; set; }
        public string MAPNO { get; set; }
        public string SRVMAPKEY { get; set; }
        public string TAXTARIFFCODE { get; set; }
        public string SDATE { get; set; }
        public string BEGTIME { get; set; }
        public string ENDTIME { get; set; }
        public string PERSEXT { get; set; }
        public string CATSCOUNTE { get; set; }
        public string STOKZ { get; set; }
        public string BELNR { get; set; }
        public string FORMELNR { get; set; }
        public string FRMVAL1 { get; set; }
        public string FRMVAL2 { get; set; }
        public string FRMVAL3 { get; set; }
        public string FRMVAL4 { get; set; }
        public string FRMVAL5 { get; set; }
        public string USERF1_NUM { get; set; }
        public string USERF2_NUM { get; set; }
        public string USERF1_TXT { get; set; }
        public string USERF2_TXT { get; set; }
        public string KNOBJ { get; set; }
        public string CHGTEXT { get; set; }
        public string KALNR { get; set; }
        public string KLVAR { get; set; }
        public string EXTDES { get; set; }
        public string BOSINTER { get; set; }
        public string BOSGRP { get; set; }
        public string BOS_RISK { get; set; }
        public string BOS_ECP { get; set; }
        public string CHGLTEXT { get; set; }
        public string BOSGRUPPENR { get; set; }
        public string BOSLFDNR { get; set; }
        public string BOSDRUKZ { get; set; }
        public string BOSSUPPLENO { get; set; }
        public string BOSSUPPLESTATUS { get; set; }
        public string SAPBOQ_OBJTYPE { get; set; }
        public string SAPBOQ_SPOSNR { get; set; }
        public string SAPBOQ_MINTROW { get; set; }
        public string SAPBOQ_QT_REL { get; set; }
        public string SAPBOQ_CK_QTY { get; set; }
        public string SAPBOQ_M_FRATE { get; set; }
        public string EXTREFKEY { get; set; }
        public string INV_MENGE { get; set; }
        public string PER_SDATE { get; set; }
        public string PER_EDATE { get; set; }
        public string GL_ACCOUNT { get; set; }
        public string VORNR { get; set; }
        public string WAERS { get; set; }

        public ServiciosPM()
        {
            FOLIO_SAM = string.Empty;
            AUFNR = string.Empty;
            PACKNO = string.Empty;
            INTROW = string.Empty;
            EXTROW = string.Empty;
            DEL = string.Empty;
            SRVPOS = string.Empty;
            RANG = string.Empty;
            EXTGROUP = string.Empty;
            PAQUETE = string.Empty;
            SUB_PACKNO = string.Empty;
            LBNUM = string.Empty;
            AUSGB = string.Empty;
            STLVPOS = string.Empty;
            EXTSRVNO = string.Empty;
            MENGE = string.Empty;
            MEINS = string.Empty;
            UEBTO = string.Empty;
            UEBTK = string.Empty;
            WITH_LIM = string.Empty;
            SPINF = string.Empty;
            PEINH = string.Empty;
            BRTWR = string.Empty;
            NETWR = string.Empty;
            FROMPOS = string.Empty;
            TOPOS = string.Empty;
            KTEXT1 = string.Empty;
            VRTKZ = string.Empty;
            TWRKZ = string.Empty;
            PERNR = string.Empty;
            MOLGA = string.Empty;
            LGART = string.Empty;
            LGTXT = string.Empty;
            STELL = string.Empty;
            IFTNR = string.Empty;
            BUDAT = string.Empty;
            INSDT = string.Empty;
            PLN_PACKNO = string.Empty;
            PLN_INTROW = string.Empty;
            KNT_PACKNO = string.Empty;
            KNT_INTROW = string.Empty;
            TMP_PACKNO = string.Empty;
            TMP_INTROW = string.Empty;
            STLV_LIM = string.Empty;
            LIMIT_ROW = string.Empty;
            ACT_MENGE = string.Empty;
            ACT_WERT = string.Empty;
            KNT_WERT = string.Empty;
            KNT_MENGE = string.Empty;
            ZIELWERT = string.Empty;
            UNG_WERT = string.Empty;
            UNG_MENGE = string.Empty;
            ALT_INTROW = string.Empty;
            BASIC = string.Empty;
            ALTERNAT = string.Empty;
            BIDDER = string.Empty;
            SUPPLE = string.Empty;
            FREEQTY = string.Empty;
            INFORM = string.Empty;
            PAUSCH = string.Empty;
            EVENTUAL = string.Empty;
            MWSKZ = string.Empty;
            TXJCD = string.Empty;
            PRS_CHG = string.Empty;
            MATKL = string.Empty;
            TBTWR = string.Empty;
            NAVNW = string.Empty;
            BASWR = string.Empty;
            KKNUMV = string.Empty;
            IWEIN = string.Empty;
            INT_WORK = string.Empty;
            EXTERNALID = string.Empty;
            KSTAR = string.Empty;
            ACT_WORK = string.Empty;
            MAPNO = string.Empty;
            SRVMAPKEY = string.Empty;
            TAXTARIFFCODE = string.Empty;
            SDATE = string.Empty;
            BEGTIME = string.Empty;
            ENDTIME = string.Empty;
            PERSEXT = string.Empty;
            CATSCOUNTE = string.Empty;
            STOKZ = string.Empty;
            BELNR = string.Empty;
            FORMELNR = string.Empty;
            FRMVAL1 = string.Empty;
            FRMVAL2 = string.Empty;
            FRMVAL3 = string.Empty;
            FRMVAL4 = string.Empty;
            FRMVAL5 = string.Empty;
            USERF1_NUM = string.Empty;
            USERF2_NUM = string.Empty;
            USERF1_TXT = string.Empty;
            USERF2_TXT = string.Empty;
            KNOBJ = string.Empty;
            CHGTEXT = string.Empty;
            KALNR = string.Empty;
            KLVAR = string.Empty;
            EXTDES = string.Empty;
            BOSINTER = string.Empty;
            BOSGRP = string.Empty;
            BOS_RISK = string.Empty;
            BOS_ECP = string.Empty;
            CHGLTEXT = string.Empty;
            BOSGRUPPENR = string.Empty;
            BOSLFDNR = string.Empty;
            BOSDRUKZ = string.Empty;
            BOSSUPPLENO = string.Empty;
            BOSSUPPLESTATUS = string.Empty;
            SAPBOQ_OBJTYPE = string.Empty;
            SAPBOQ_SPOSNR = string.Empty;
            SAPBOQ_MINTROW = string.Empty;
            SAPBOQ_QT_REL = string.Empty;
            SAPBOQ_CK_QTY = string.Empty;
            SAPBOQ_M_FRATE = string.Empty;
            EXTREFKEY = string.Empty;
            INV_MENGE = string.Empty;
            PER_SDATE = string.Empty;
            PER_EDATE = string.Empty;
            GL_ACCOUNT = string.Empty;
            VORNR = string.Empty;
            WAERS = string.Empty;
        }
    }
}
