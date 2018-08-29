using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Ordenes
    {
        #region instancia
        private static DALC_Ordenes instance = null;
        private static readonly object padlock = new object();

        public static DALC_Ordenes obtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Ordenes();
                }
                return instance;
            }
        }
        #endregion
        public void IngresaORDENES_PM(EntityConnectionStringBuilder connection, ORDENES_PM or)
        {
            var context = new samEntities(connection.ToString());
            context.ordenes_pm_notificaciones_MDL(or.AUART,
                                                  or.KOKRS,
                                                  or.AUFNR,
                                                  or.ESTATUS,
                                                  or.KTEXT,
                                                  or.AUTYP,
                                                  or.IWERK,
                                                  or.LVORM);
        }
        public void VaciarORDENES_PM(EntityConnectionStringBuilder connection, ORDENES_PM or)
        {
            var context = new samEntities(connection.ToString());
            context.ordenes_pm_notificaciones_delete_MDL(or.AUFNR,
                                                         or.IWERK);
        }
        public void VaciarPM_OPERACIONES(EntityConnectionStringBuilder connection, PM_OPERACIONES pm)
        {
            var context = new samEntities(connection.ToString());
            context.pm_operaciones_notificaciones_delete_MDL(pm.AUFNR,
                                                             pm.WERKS);
        }
        public void IngresaPM_OPERACIONES(EntityConnectionStringBuilder connection, PM_OPERACIONES pm)
        {
            var context = new samEntities(connection.ToString());
            context.INS_pm_operaciones_notificaciones_MDL(pm.AUFNR,
                                                      pm.AUFPL,
                                                      pm.APLZL,
                                                      pm.PLNAL,
                                                      pm.PLNTY,
                                                      pm.VINTV,
                                                      pm.PLNNR,
                                                      pm.ZAEHL,
                                                      pm.VORNR,
                                                      pm.STEUS,
                                                      pm.ARBID,
                                                      pm.WERKS,
                                                      pm.LTXA1,
                                                      pm.BMSCH,
                                                      pm.DAUNO,
                                                      pm.DAUNE,
                                                      pm.ARBEI,
                                                      pm.ARBEH,
                                                      pm.MGVRG,
                                                      pm.ISM01,
                                                      pm.ISM02,
                                                      pm.ISM03,
                                                      pm.ISM04,
                                                      pm.ISM05,
                                                      pm.ISM06,
                                                      pm.ILE01,
                                                      pm.ILE02,
                                                      pm.ILE03,
                                                      pm.ILE04,
                                                      pm.ILE05,
                                                      pm.ILE06,
                                                      pm.MEINH,
                                                      pm.AUERU,
                                                      pm.BANFN,
                                                      pm.BNFPO,
                                                      pm.EKORG,
                                                      pm.EKGRP,
                                                      pm.MATKL,
                                                      pm.LIFNR,
                                                      pm.PREIS,
                                                      pm.PEINH,
                                                      pm.WAERS,
                                                      pm.SAKTO,
                                                      pm.AFNAM,
                                                      pm.RUECK,
                                                      pm.ISTRU,
                                                      pm.ISTRU2);
        }
        public void VaciarEquipo_O(EntityConnectionStringBuilder connection, Equipo_O eq)
        {
            var context = new samEntities(connection.ToString());
            context.equipos_notificaciones_delete_MDL(eq.AUFNR,
                                                      eq.WERKS);
        }
        public void IngresaEquipo_O(EntityConnectionStringBuilder connection, Equipo_O eq)
        {
            var context = new samEntities(connection.ToString());
            context.equipos_notificaciones_MDL(eq.EQUNR,
                                               eq.MATNR,
                                               eq.SERNR,
                                               eq.WERKS,
                                               eq.LGORT,
                                               eq.AUFNR,
                                               eq.SUPEQUI,
                                               eq.CHARG,
                                               eq.MONTADO);
        }
        public void VaciarPM01(EntityConnectionStringBuilder connection, PM01 pm)
        {
            var context = new samEntities(connection.ToString());
            context.pm01_notificaciones_delete_MDL(pm.AUFNR,
                                                   pm.WERKS);
        }
        public void IngresaPM01(EntityConnectionStringBuilder connection, PM01 pm)
        {
            var context = new samEntities(connection.ToString());
            context.pm01_notificaciones_MDL(pm.RSNUM,
                                            pm.RSPOS,
                                            pm.RSART,
                                            pm.BDART,
                                            pm.RSSTA,
                                            pm.XLOEK,
                                            pm.XWAOK,
                                            pm.KZEAR,
                                            pm.XFEHL,
                                            pm.MATNR,
                                            pm.WERKS,
                                            pm.LGORT,
                                            pm.PRVBE,
                                            pm.CHARG,
                                            pm.PLPLA,
                                            pm.SOBKZ,
                                            pm.BDTER,
                                            pm.BDMNG,
                                            pm.MEINS,
                                            pm.SHKZG,
                                            pm.FMENG,
                                            pm.ENMNG,
                                            pm.ENWRT,
                                            pm.WAERS,
                                            pm.ERFMG,
                                            pm.ERFME,
                                            pm.PLNUM,
                                            pm.BANFN,
                                            pm.BNFPO,
                                            pm.AUFNR,
                                            pm.BAUGR,
                                            pm.SERNR,
                                            pm.KDAUF,
                                            pm.KDPOS,
                                            pm.KDEIN,
                                            pm.PROJN,
                                            pm.BWART,
                                            pm.SAKNR,
                                            pm.GSBER,
                                            pm.UMWRK,
                                            pm.UMLGO,
                                            pm.NAFKZ,
                                            pm.NOMAT,
                                            pm.NOMNG,
                                            pm.POSTP,
                                            pm.POSNR,
                                            pm.ROMS1,
                                            pm.ROMS2,
                                            pm.ROMS3,
                                            pm.ROMEI,
                                            pm.ROMEN,
                                            pm.SGTXT,
                                            pm.LMENG,
                                            pm.ROHPS,
                                            pm.RFORM,
                                            pm.ROANZ,
                                            pm.FLMNG,
                                            pm.STLTY,
                                            pm.STLNR,
                                            pm.STLKN,
                                            pm.STPOZ,
                                            pm.LTXSP,
                                            pm.POTX1,
                                            pm.POTX2,
                                            pm.SANKA,
                                            pm.ALPOS,
                                            pm.EWAHR,
                                            pm.AUSCH,
                                            pm.AVOAU,
                                            pm.NETAU,
                                            pm.NLFZT,
                                            pm.AENNR,
                                            pm.UMREZ,
                                            pm.UMREN,
                                            pm.SORTF,
                                            pm.SBTER,
                                            pm.VERTI,
                                            pm.SCHGT,
                                            pm.UPSKZ,
                                            pm.DBSKZ,
                                            pm.TXTPS,
                                            pm.DUMPS,
                                            pm.BEIKZ,
                                            pm.ERSKZ,
                                            pm.AUFST,
                                            pm.AUFWG,
                                            pm.BAUST,
                                            pm.BAUWG,
                                            pm.AUFPS,
                                            pm.EBELN,
                                            pm.EBELP,
                                            pm.EBELE,
                                            pm.KNTTP,
                                            pm.KZVBR,
                                            pm.PSPEL,
                                            pm.AUFPL,
                                            pm.PLNFL,
                                            pm.VORNR,
                                            pm.APLZL,
                                            pm.OBJNR,
                                            pm.FLGAT,
                                            pm.GPREIS,
                                            pm.FPREIS,
                                            pm.PEINH,
                                            pm.RGEKZ,
                                            pm.EKGRP,
                                            pm.ROKME,
                                            pm.ZUMEI,
                                            pm.ZUMS1,
                                            pm.ZUMS2,
                                            pm.ZUMS3,
                                            pm.ZUDIV,
                                            pm.VMENG,
                                            pm.PRREG,
                                            pm.LIFZT,
                                            pm.CUOBJ,
                                            pm.KFPOS,
                                            pm.REVLV,
                                            pm.BERKZ,
                                            pm.LGNUM,
                                            pm.LGTYP,
                                            pm.LGPLA,
                                            pm.TBMNG,
                                            pm.NPTXTKY,
                                            pm.KBNKZ,
                                            pm.KZKUP,
                                            pm.AFPOS,
                                            pm.NO_DISP,
                                            pm.BDZTP,
                                            pm.ESMNG,
                                            pm.ALPGR,
                                            pm.ALPRF,
                                            pm.ALPST,
                                            pm.KZAUS,
                                            pm.NFEAG,
                                            pm.NFPKZ,
                                            pm.NFGRP,
                                            pm.NFUML,
                                            pm.ADRNR,
                                            pm.CHOBJ,
                                            pm.SPLKZ,
                                            pm.SPLRV,
                                            pm.KNUMH,
                                            pm.WEMPF,
                                            pm.ABLAD,
                                            pm.HKMAT,
                                            pm.HRKFT,
                                            pm.VORAB,
                                            pm.MATKL,
                                            pm.FRUNV,
                                            pm.CLAKZ,
                                            pm.INPOS,
                                            pm.WEBAZ,
                                            pm.LIFNR,
                                            pm.FLGEX,
                                            pm.FUNCT,
                                            pm.GPREIS_2,
                                            pm.FPREIS_2,
                                            pm.PEINH_2,
                                            pm.INFNR,
                                            pm.KZECH,
                                            pm.KZMPF,
                                            pm.STLAL,
                                            pm.PBDNR,
                                            pm.STVKN,
                                            pm.KTOMA,
                                            pm.VRPLA,
                                            pm.KZBWS,
                                            pm.NLFZV,
                                            pm.NLFMV,
                                            pm.TECHS,
                                            pm.OBJTYPE,
                                            pm.CH_PROC,
                                            pm.FXPRU,
                                            pm.UMSOK,
                                            pm.VORAB_SM,
                                            pm.FIPOS,
                                            pm.FIPEX,
                                            pm.FISTL,
                                            pm.GEBER,
                                            pm.GRANT_NBR,
                                            pm.FKBER,
                                            pm.PRIO_URG,
                                            pm.PRIO_REQ,
                                            pm.KBLNR,
                                            pm.KBLPOS,
                                            pm.BUDGET_PD,
                                            pm.SC_OBJECT_ID,
                                            pm.SC_ITM_NO,
                                            pm.SGT_SCAT,
                                            pm.SGT_RCAT,
                                            pm.FMFGUS_KEY,
                                            pm.ADVCODE,
                                            pm.STRUC_CODE,
                                            pm.STRUC_CLASS,
                                            pm.STRUC_CLASSTYP,
                                            pm.FSH_RALLOC_QTY,
                                            pm.FSH_ASSIGN,
                                            pm.MILL_UCDET,
                                            pm.WTY_IND,
                                            pm.R_PART_INDICATOR,
                                            pm.WTYSC_CLMITEM);
        }
        public void VaciarPM03_1(EntityConnectionStringBuilder connection, PM03_1 pm)
        {
            var context = new samEntities(connection.ToString());
            context.pm03_1_notificaciones_delete_MDL(pm.AUFNR);
        }
        public void IngresaPM03_1(EntityConnectionStringBuilder connection, PM03_1 pm)
        {
            var context = new samEntities(connection.ToString());
            context.pm03_1_notificaciones_MDL(pm.AUFNR,
                                              pm.EXTROW,
                                              pm.SRVPOS,
                                              pm.KTEXT1,
                                              pm.MENGE,
                                              pm.MEINS,
                                              pm.NETWR,
                                              pm.PEINH,
                                              pm.MATKL,
                                              pm.KSTAR);
        }
        public void VaciarPM03_2(EntityConnectionStringBuilder connection, PM03_2 pm)
        {
            var context = new samEntities(connection.ToString());
            context.pm03_2_notificaciones_delete_MDL(pm.AUFNR,
                                                     pm.WERKS);
        }
        public void IngresaPM03_2(EntityConnectionStringBuilder connection, PM03_2 pm)
        {
            var context = new samEntities(connection.ToString());
            context.pm03_2_notificaciones_MDL(pm.AUFNR,
                                              pm.BANFN,
                                              pm.BNFPO,
                                              pm.EBELN,
                                              pm.EBELP,
                                              pm.LOEKZ,
                                              pm.TXZ01,
                                              pm.WERKS,
                                              pm.LGORT,
                                              pm.KONNR,
                                              pm.IDNLF,
                                              pm.MENGE,
                                              pm.MEINS,
                                              pm.NETWR);
        }
        public void VaciarPM03_3(EntityConnectionStringBuilder connection, PM03_3 pm)
        {
            var context = new samEntities(connection.ToString());
            context.pm03_3_notificaciones_delete_MDL(pm.AUFNR);
        }
        public void IngresaPM03_3(EntityConnectionStringBuilder connection, PM03_3 pm)
        {
            var context = new samEntities(connection.ToString());
            context.pm03_3_notificaciones_MDL(pm.AUFNR,
                                              pm.EBELN,
                                              pm.EBELP,
                                              pm.GJAHR,
                                              pm.BELNR,
                                              pm.BUZEI,
                                              pm.BEWTP,
                                              pm.BWART,
                                              pm.BUDAT,
                                              pm.MENGE,
                                              pm.WRBTR,
                                              pm.WAERS,
                                              pm.ERNAM);
        }
        public void VaciarQM01_1(EntityConnectionStringBuilder connection, QM01_1 qm)
        {
            var context = new samEntities(connection.ToString());
            context.qm01_1_notificaciones_delete_MDL(qm.AUFNR);
        }
        public void IngresaQM01_1(EntityConnectionStringBuilder connection, QM01_1 qm)
        {
            var context = new samEntities(connection.ToString());
            context.qm01_1_notificaciones_MDL(qm.AUFNR,
                                                     qm.MERKNR,
                                                     qm.KURZTEXT,
                                                     qm.MASSEINHSW,
                                                     qm.ISTSTPUMF,
                                                     qm.ISTSTPANZ);
        }
        public void VaciarQM01_2(EntityConnectionStringBuilder connection, QM01_2 qm)
        {
            var context = new samEntities(connection.ToString());
            context.qm01_2_notificaciones_delete_MDL(qm.AUFNR);
        }
        public void IngresaQM01_2(EntityConnectionStringBuilder connection, QM01_2 qm)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_qm01_2_notificaciones_MDL(qm.AUFNR,
                                                     qm.PRUEFLOS,
                                                     qm.MERKNR,
                                                     qm.KURZTEXT,
                                                     qm.KTX01,
                                                     qm.KATAB1,
                                                     qm.ANZFEHLEH,
                                                     qm.ORIGINAL_INPUT,
                                                     qm.MBEWERTG,
                                                     qm.CODE1,
                                                     qm.MASSEINHSW,
                                                     qm.PRUEFBEMKT,
                                                     qm.KATALGART1,
                                                     qm.CHAR_TYPE,
                                                     qm.AUSWMENGE1,
                                                     qm.SOLLSTPUMF,
                                                     qm.TEORICO,
                                                     qm.TINFERIOR,
                                                     qm.TSUPERIOR,
                                                     qm.SOLLWNI,
                                                     qm.TOLOBNI,
                                                     qm.TOLUNNI);
        }
        public void VaciarQM01_3(EntityConnectionStringBuilder connection, QM01_3 qm)
        {
            var context = new samEntities(connection.ToString());
            context.qm01_3_notificaciones_delete_MDL(qm.AUFNR);
        }
        public void IngresaQM01_3(EntityConnectionStringBuilder connection, QM01_3 qm)
        {
            var context = new samEntities(connection.ToString());
            context.INS_qm01_3_notificaciones_MDL(qm.AUFNR,
                                                     qm.PRUEFLOS,
                                                     qm.MERKNR,
                                                     qm.DETAILERG,
                                                     qm.KURZTEXT,
                                                     qm.KATAB1,
                                                     qm.ISTSTPUMF,
                                                     qm.ORIGINAL_INPUT,
                                                     qm.CODE1,
                                                     qm.ICBEWERT,
                                                     qm.KTX01,
                                                     qm.PRUEFBEMKT,
                                                     qm.ERSTELLER,
                                                     qm.SOLLSTPUMF,
                                                     qm.MBEWERTG,
                                                     qm.MASSEINHSW,
                                                     qm.CHAR_TYPE,
                                                     qm.FOLIO_SAM,
                                                     qm.TEORICO,
                                                     qm.TINFERIOR,
                                                     qm.TSUPERIOR,
                                                     qm.SOLLWNI,
                                                     qm.TOLOBNI,
                                                     qm.TOLUNNI);
        }
        public void VaciarCAB_QM01(EntityConnectionStringBuilder connection, CAB_QM01 qm)
        {
            var context = new samEntities(connection.ToString());
            context.qm_cabecera_delete_MDL(qm.AUFNR,
                                           qm.WERK);
        }
        public void IngresaCAB_QM01(EntityConnectionStringBuilder connection, CAB_QM01 qm)
        {
            var context = new samEntities(connection.ToString());
            context.qm_cabecera_MDL(qm.AUFNR,
                                    qm.KTEXTLOS,
                                    qm.PRUEFLOS,
                                    qm.WERK,
                                    qm.ERSTELLER,
                                    qm.ENSTEHDAT,
                                    qm.ENTSTEZEIT,
                                    qm.AENDERER,
                                    qm.AENDERDAT,
                                    qm.AENDERZEIT);
        }
        public void VaciarENSAMBLE(EntityConnectionStringBuilder connection, ENSAMBLE qm)
        {
            var context = new samEntities(connection.ToString());
            context.ensamble_notificaciones_delete_MDL(qm.AUFNR);
        }
        public void IngresaENSAMBLE(EntityConnectionStringBuilder connection, ENSAMBLE qm)
        {
            var context = new samEntities(connection.ToString());
            context.ensamble_notificaciones_MDL(qm.AUFNR,
                                                qm.NUM_EQUI,
                                                qm.FECHA_MONT,
                                                qm.ULTIM_MEDI,
                                                qm.STATUS_MONT,
                                                qm.VAL_OVER,
                                                qm.FECHA_DESM);
        }
        public void IngresarCaracteristicasLote(EntityConnectionStringBuilder connection, CaracteristicasLote lc)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_caracteristicas_lote_pm_MDL(lc.MATNR,
                                                       lc.WERKS,
                                                       lc.CHARG,
                                                       lc.CLABS,
                                                       lc.NUM_EQUI,
                                                       lc.FECHA_MONT,
                                                       lc.ULTIM_MEDI,
                                                       lc.STATUS_MONT,
                                                       lc.VAL_OVER,
                                                       lc.FECHA_DESM);
        }
        public void VaciarCaracteristicasLote(EntityConnectionStringBuilder connection, CaracteristicasLote lo)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_caracteristicas_lote_pm_MDL(lo.MATNR,
                                                       lo.WERKS,
                                                       lo.CHARG);
        }
        public void EliminarDecisionEmpleoCarac(EntityConnectionStringBuilder connection, DecisionEmpleoCaracteristicas cd)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_caracteristicas_decision_empleo_MDL(cd.PRUEFLOS);
        }
        public void IngresarDecisionEmpleoCarac(EntityConnectionStringBuilder connection, DecisionEmpleoCaracteristicas cd)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_caracteristicas_decision_empleo_MDL(cd.PRUEFLOS,
                                                               cd.EQUNR,
                                                               cd.VCODE,
                                                               cd.VCODEGRP,
                                                               cd.VBEWERTUNG,
                                                               cd.KURZTEXT);
        }
        public void EliminarDecisionEmpleoLoteIns(EntityConnectionStringBuilder connection, DecisionEmpleoLoteInps dl)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_lote_inspeccion_decision_empleo_MDL(dl.PRUEFLOS);
        }
        public void InsertarDecisionEmpleoLoteIns(EntityConnectionStringBuilder connection, DecisionEmpleoLoteInps dl)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_lote_inspeccion_decision_empleo_MDL(dl.PRUEFLOS,
                                                               dl.VCODE,
                                                               dl.VCODEGRP,
                                                               dl.VBEWERTUNG,
                                                               dl.KURZTEXT);
        }
        public void VaciarTextosCaracteristicas(EntityConnectionStringBuilder connection, TextosCaracteristicasOrdenes tc)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_textos_caracteristicas_qm_MDL(tc.AUFNR);
        }
        public void IngresarTextosCaracteristicas(EntityConnectionStringBuilder connection, TextosCaracteristicasOrdenes tc)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_textos_caracteristicas_qm_MDL(tc.AUFNR,
                                                         tc.PRUEFLOS,
                                                         tc.MERKNR,
                                                         tc.TDLINE);
        }
    }
}
