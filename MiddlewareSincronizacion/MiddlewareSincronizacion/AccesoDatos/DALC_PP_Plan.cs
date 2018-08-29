using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_PP_Plan
    {
        #region Instancia
        private static DALC_PP_Plan instance = null;
        private static readonly object padlock = new object();

        public static DALC_PP_Plan obtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_PP_Plan();
                }
                return instance;
            }
        }

        #endregion
        public void VaciarPlan(EntityConnectionStringBuilder connection, PP_Plan pp)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_plan_orden_MDL(pp.AUFNR,
                                          pp.WAWRK);
        }
        public void IngresaPlan(EntityConnectionStringBuilder connection, PP_Plan pp)
        {
            var context = new samEntities(connection.ToString());
            context.plan_orden_MDL(pp.AUFNR,
                                   pp.FOLIOSAM,
                                   pp.AUART,
                                   pp.ESTATUS,
                                   pp.KTEXT,
                                   pp.VAPLZ,
                                   pp.WAWRK,
                                   pp.QMNUM,
                                   pp.USER4,
                                   pp.ILART,
                                   pp.ANLZU,
                                   pp.GSTRP,
                                   pp.GLTRP,
                                   pp.PRIOK,
                                   pp.REVNR,
                                   pp.AUFPL,
                                   pp.TPLNR,
                                   pp.PLTXT,
                                   pp.EQUNR,
                                   pp.EQKTX,
                                   pp.BAUTL,
                                   pp.LVORM);
        }
        public void VaciarPlanOP(EntityConnectionStringBuilder connection, PP_PlanOP op)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_plan_op_MDL(op.AUFNR,
                                       op.WERKS);
        }
        public void IngresaPlanOP(EntityConnectionStringBuilder connection, PP_PlanOP op)
        {
            var context = new samEntities(connection.ToString());
            context.plan_op_MDL(op.AUFNR,
                                op.FOLIOSAM,
                                op.VORNR,
                                op.UVORN,
                                op.ARBPL,
                                op.WERKS,
                                op.STEUS,
                                op.KTSCH,
                                op.ANLZU,
                                op.LTXA1,
                                op.ISMNW,
                                op.ARBEI,
                                op.ARBEH,
                                op.ANZZL,
                                op.DAUNO,
                                op.DAUNE,
                                op.INDET,
                                op.LARNT,
                                op.WEMPF,
                                op.ABLAD,
                                op.AUFKT,
                                op.FLG_KMP,
                                op.FLG_FHM,
                                op.TPLNR,
                                op.EQUNR,
                                op.QMNUM,
                                op.NPLDA,
                                op.AUDISP,
                                op.FSAVZ,
                                op.FSAVD,
                                op.FSEDD,
                                op.FSEDZ);
        }
        public void IngresaComponente(EntityConnectionStringBuilder connection, Componentes cc)
        {
            var context = new samEntities(connection.ToString());
            context.componentes_MDL(cc.FOLIOSAM,
                                    cc.AUFNR,
                                    cc.POSNR,
                                    cc.MATNR,
                                    cc.MATXT,
                                    cc.MENGE,
                                    cc.EINHEIT,
                                    cc.POSTP,
                                    cc.SOBKZ_D,
                                    cc.LGORT,
                                    cc.WERKS,
                                    cc.VORNR,
                                    cc.CHARG,
                                    cc.WEMPF,
                                    cc.ABLAD,
                                    cc.XLOEK,
                                    cc.SCHGT,
                                    cc.RGEKZ,
                                    cc.AUDISP);
        }
        public void VaciarComponente(EntityConnectionStringBuilder connection, Componentes cc)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_componentes_MDL(cc.AUFNR,
                                           cc.WERKS);
        }
        public void VaciarServiciosPM(EntityConnectionStringBuilder connection, ServiciosPM sp)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_servicios_pm_MDL(sp.AUFNR);
        }
        public void IngresaServiciosPM(EntityConnectionStringBuilder connection, ServiciosPM spm)
        {
            var context = new samEntities(connection.ToString());
            context.servicios_pm_MDL(spm.FOLIO_SAM,
                                     spm.AUFNR,
                                     spm.PACKNO,
                                     spm.INTROW,
                                     spm.EXTROW,
                                     spm.DEL,
                                     spm.SRVPOS,
                                     spm.RANG,
                                     spm.EXTGROUP,
                                     spm.PAQUETE,
                                     spm.SUB_PACKNO,
                                     spm.LBNUM,
                                     spm.AUSGB,
                                     spm.STLVPOS,
                                     spm.EXTSRVNO,
                                     spm.MENGE,
                                     spm.MEINS,
                                     spm.UEBTO,
                                     spm.UEBTK,
                                     spm.WITH_LIM,
                                     spm.SPINF,
                                     spm.PEINH,
                                     spm.BRTWR,
                                     spm.NETWR,
                                     spm.FROMPOS,
                                     spm.TOPOS,
                                     spm.KTEXT1,
                                     spm.VRTKZ,
                                     spm.TWRKZ,
                                     spm.PERNR,
                                     spm.MOLGA,
                                     spm.LGART,
                                     spm.LGTXT,
                                     spm.STELL,
                                     spm.IFTNR,
                                     spm.BUDAT,
                                     spm.INSDT,
                                     spm.PLN_PACKNO,
                                     spm.PLN_INTROW,
                                     spm.KNT_PACKNO,
                                     spm.KNT_INTROW,
                                     spm.TMP_PACKNO,
                                     spm.TMP_INTROW,
                                     spm.STLV_LIM,
                                     spm.LIMIT_ROW,
                                     spm.ACT_MENGE,
                                     spm.ACT_WERT,
                                     spm.KNT_WERT,
                                     spm.KNT_MENGE,
                                     spm.ZIELWERT,
                                     spm.UNG_WERT,
                                     spm.UNG_MENGE,
                                     spm.ALT_INTROW,
                                     spm.BASIC,
                                     spm.ALTERNAT,
                                     spm.BIDDER,
                                     spm.SUPPLE,
                                     spm.FREEQTY,
                                     spm.INFORM,
                                     spm.PAUSCH,
                                     spm.EVENTUAL,
                                     spm.MWSKZ,
                                     spm.TXJCD,
                                     spm.PRS_CHG,
                                     spm.MATKL,
                                     spm.TBTWR,
                                     spm.NAVNW,
                                     spm.BASWR,
                                     spm.KKNUMV,
                                     spm.IWEIN,
                                     spm.INT_WORK,
                                     spm.EXTERNALID,
                                     spm.KSTAR,
                                     spm.ACT_WORK,
                                     spm.MAPNO,
                                     spm.SRVMAPKEY,
                                     spm.TAXTARIFFCODE,
                                     spm.SDATE,
                                     spm.BEGTIME,
                                     spm.ENDTIME,
                                     spm.PERSEXT,
                                     spm.CATSCOUNTE,
                                     spm.STOKZ,
                                     spm.BELNR,
                                     spm.FORMELNR,
                                     spm.FRMVAL1,
                                     spm.FRMVAL2,
                                     spm.FRMVAL3,
                                     spm.FRMVAL4,
                                     spm.FRMVAL5,
                                     spm.USERF1_NUM,
                                     spm.USERF2_NUM,
                                     spm.USERF1_TXT,
                                     spm.USERF2_TXT,
                                     spm.KNOBJ,
                                     spm.CHGTEXT,
                                     spm.KALNR,
                                     spm.KLVAR,
                                     spm.EXTDES,
                                     spm.BOSINTER,
                                     spm.BOSGRP,
                                     spm.BOS_RISK,
                                     spm.BOS_ECP,
                                     spm.CHGLTEXT,
                                     spm.BOSGRUPPENR,
                                     spm.BOSLFDNR,
                                     spm.BOSDRUKZ,
                                     spm.BOSSUPPLENO,
                                     spm.BOSSUPPLESTATUS,
                                     spm.SAPBOQ_OBJTYPE,
                                     spm.SAPBOQ_SPOSNR,
                                     spm.SAPBOQ_MINTROW,
                                     spm.SAPBOQ_QT_REL,
                                     spm.SAPBOQ_CK_QTY,
                                     spm.SAPBOQ_M_FRATE,
                                     spm.EXTREFKEY,
                                     spm.INV_MENGE,
                                     spm.PER_SDATE,
                                     spm.PER_EDATE,
                                     spm.GL_ACCOUNT,
                                     spm.VORNR,
                                     spm.WAERS);
        }
        public void IngresaTextoPosicionOrdenVis(EntityConnectionStringBuilder connection, TextoPosicionesOrdenesVis txt)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_texto_posicion_ordenes_vis_MDL(txt.AUFNR,
                                                          txt.FOLIO_SAM,
                                                          txt.VORNR,
                                                          txt.INDICE,
                                                          txt.TDLINE);
        }
        public void VaciarTextoPosicionOrdenVis(EntityConnectionStringBuilder connection, TextoPosicionesOrdenesVis txt)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_texto_posicion_ordenes_vis_MDL(txt.AUFNR);
        }
        public void VaciarOrdenesControl(EntityConnectionStringBuilder connection, OrdenesControl oc)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_ordenes_control_MDL(oc.AUFNR);
        }
        public void IngresarOrdenesControl(EntityConnectionStringBuilder connection, OrdenesControl oc)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_ordenes_control_MDL(oc.AUFNR,
                                               oc.FOLIO_SAM,
                                               oc.ERNAM,
                                               oc.ERDAT,
                                               oc.AENAM,
                                               oc.AEDAT);
        }
        public void VaciarOrdenesEmplazamiento(EntityConnectionStringBuilder connection, OrdenesEmplazamiento oe)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_ordenes_emplazamiento_MDL(oe.AUFNR,
                                                     oe.SWERK);
        }
        public void IngresarOrdenesEmplazamiento(EntityConnectionStringBuilder connection, OrdenesEmplazamiento oe)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_ordenes_emplazamiento_MDL(oe.AUFNR,
                                                     oe.FOLIO_SAM,
                                                     oe.SWERK,
                                                     oe.STORT,
                                                     oe.MSGRP,
                                                     oe.BEBER,
                                                     oe.ARBPL,
                                                     oe.ABCKZ,
                                                     oe.EQFNR,
                                                     oe.BUKRS,
                                                     oe.ANLNR,
                                                     oe.ANLUN,
                                                     oe.KOSTL,
                                                     oe.PROID);
        }
        public void VaciarOrdenesPlanificacion(EntityConnectionStringBuilder connection, OrdenesPlanificacion opa)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_ordenes_planificacion_MDL(opa.AUFNR);
        }
        public void IngresarOrdenesPlanificacion(EntityConnectionStringBuilder connection, OrdenesPlanificacion opa)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_ordenes_planificacion_MDL(opa.AUFNR,
                                                     opa.FOLIO_SAM,
                                                     opa.WARPL,
                                                     opa.WAPOS,
                                                     opa.PLNTY,
                                                     opa.PLNNR,
                                                     opa.PLNAL);
        }
    }
}
