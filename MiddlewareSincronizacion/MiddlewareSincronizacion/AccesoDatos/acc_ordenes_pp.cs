using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class acc_ordenes_pp
    {
        #region Instancia
        private static acc_ordenes_pp instance = null;
        private static readonly object padlock = new object();

        public static acc_ordenes_pp ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new acc_ordenes_pp();
                }
                return instance;
            }
        }
        #endregion
        public void truncarMotivosPP(EntityConnectionStringBuilder connection)
        {
            var contex = new samEntities(connection.ToString());
            contex.motivos_trunca_mdl();
        }
        public void ingresaMotivos(EntityConnectionStringBuilder connection, string centro, string indice, string motivo)
        {
            var contex = new samEntities(connection.ToString());
            contex.motivo_insert_mdl(centro, indice, motivo);
        }
        public void InsertarProductionPlan(EntityConnectionStringBuilder connection, Plan_Orden_PP plan)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.Insrta_Ordenes_mdl(plan.AUFNR,
                                          plan.FOLIO_SAM,
                                          plan.WERKS,
                                          plan.AUART,
                                          plan.ESTATUS,
                                          plan.KTEXT,
                                          plan.VAPLZ,
                                          plan.WAWRK,
                                          plan.QMNUM,
                                          plan.USER4,
                                          plan.ILART,
                                          plan.ANLZU,
                                          plan.GSTRP,
                                          plan.GLTRP,
                                          plan.PRIOK,
                                          plan.REVNR,
                                          plan.AUFPL,
                                          plan.TPLNR,
                                          plan.PLTXT,
                                          plan.MATNR,
                                          plan.MAKTX,
                                          plan.EQKTX,
                                          plan.BAUTL,
                                          plan.LVORM,
                                          plan.ERDAT,
                                          plan.AEDAT,
                                          plan.RMZHL,
                                          plan.GAMNG,
                                          plan.UEBTK,
                                          plan.KUNNR,
                                          plan.NAME1,
                                          plan.TEXTO,
                                          plan.NOTIFICADO,
                                          plan.RESTANTE,
                                          plan.AUSP1,
                                          plan.SALES_ORDER,
                                          plan.SALES_ORDER_ITEM,
                                          plan.TCLIENTE,
                                          plan.ROUTE,
                                          plan.STOCK);
            }
            catch (Exception) { }
        }
        public void ActualizarOrden(EntityConnectionStringBuilder connection, Plan_Orden_PP plan)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_Ordenes_MDL(plan.AUFNR,
                                          plan.FOLIO_SAM,
                                          plan.WERKS,
                                          plan.AUART,
                                          plan.ESTATUS,
                                          plan.KTEXT,
                                          plan.VAPLZ,
                                          plan.WAWRK,
                                          plan.QMNUM,
                                          plan.USER4,
                                          plan.ILART,
                                          plan.ANLZU,
                                          plan.GSTRP,
                                          plan.GLTRP,
                                          plan.PRIOK,
                                          plan.REVNR,
                                          plan.AUFPL,
                                          plan.TPLNR,
                                          plan.PLTXT,
                                          plan.MATNR,
                                          plan.MAKTX,
                                          plan.EQKTX,
                                          plan.BAUTL,
                                          plan.LVORM,
                                          plan.ERDAT,
                                          plan.AEDAT,
                                          plan.RMZHL,
                                          plan.GAMNG,
                                          plan.UEBTK,
                                          plan.KUNNR,
                                          plan.NAME1,
                                          plan.TEXTO,
                                          plan.NOTIFICADO,
                                          plan.RESTANTE,
                                          plan.AUSP1,
                                          plan.SALES_ORDER,
                                          plan.SALES_ORDER_ITEM,
                                          plan.TCLIENTE,
                                          plan.ROUTE,
                                          plan.STOCK);
        }
        public IEnumerable<getDatosEtiquetaPP2_Result> etiquetaPP_2(EntityConnectionStringBuilder connection, string folio)
        {
            var context = new samEntities(connection.ToString());
            return context.getDatosEtiquetaPP2(folio);
        }
        public IEnumerable<string> tipoImpresora(EntityConnectionStringBuilder connection, string pt)
        {
            var context = new samEntities(connection.ToString());
            return context.tipoImpresora_mdl(pt);
        }
        public IEnumerable<string> impresora(EntityConnectionStringBuilder connection, string pt)
        {
            var context = new samEntities(connection.ToString());
            return context.getPrinterPP(pt);
        }
        public void BorraregistroStatus(EntityConnectionStringBuilder connection, string folio)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.deleteStatusPT_mdl(folio);
            }
            catch (Exception) { }
        }
        public void updateStatusFolio(EntityConnectionStringBuilder connection, string folio, string msg)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.updateStatusErrorPT_mdl(folio, msg);
            }
            catch (Exception) { }
        }
        public void borrarRegistroFolio(EntityConnectionStringBuilder connection, string folio)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.deleteTablasPT_mdl(folio);
            }
            catch (Exception) { }
        }
        public void updateRegistrosFolio(EntityConnectionStringBuilder connection, string folio, string msg)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.updateTablasErrorPT_mdl(folio, msg);
            }
            catch (Exception) { }
        }
        public void ActualizaResultado(EntityConnectionStringBuilder connection, string folio_sam, string mensaje)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_Noti_PP_MDL(folio_sam,
                                       mensaje);
        }
        public void InsertarOperaciones_de_ordenesPM(EntityConnectionStringBuilder connection, Operaciones_de_ordenesPM op)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.Inserta_Operaciones_mdl(op.AUFNR,
                                               op.FOLIO_SAM,
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
                                               op.MATNR,
                                               op.QMNUM,
                                               op.NPLDA,
                                               op.AUDISP,
                                               op.FSAVZ,
                                               op.FSAVD,
                                               op.FSEDD,
                                               op.FSEDZ,
                                               op.MEINS,
                                               op.MAKTX,
                                               op.ESPECIFICACION,
                                               op.OBSERVACIONES,
                                               op.VGW01,
                                               op.VGE01,
                                               op.SYSTEM_STATUS);

            }
            catch (Exception) { }
        }
        public void ActulizarOperaciones(EntityConnectionStringBuilder connection, Operaciones_de_ordenesPM op)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_Operaciones_MDL(op.AUFNR,
                                               op.FOLIO_SAM,
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
                                               op.MATNR,
                                               op.QMNUM,
                                               op.NPLDA,
                                               op.AUDISP,
                                               op.FSAVZ,
                                               op.FSAVD,
                                               op.FSEDD,
                                               op.FSEDZ,
                                               op.MEINS,
                                               op.MAKTX,
                                               op.ESPECIFICACION,
                                               op.OBSERVACIONES,
                                               op.VGW01,
                                               op.VGE01,
                                               op.SYSTEM_STATUS);
        }
        public void InsertarComponentes_de_ordenesPM(EntityConnectionStringBuilder connection, Componentes_de_ordenesPM co)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.INSERT_Componentes_mdl(co.AUFNR,
                                       co.FOLIO_SAM,
                                       co.POSNR,
                                       co.MATNR,
                                       co.MATXT,
                                       co.MENGE,
                                       co.EINHEIT,
                                       co.POSTP,
                                       co.SOBKZ_D,
                                       co.LGORT,
                                       co.WERKS,
                                       co.VORNR,
                                       co.CHARG,
                                       co.WEMPF,
                                       co.ABLAD,
                                       co.XLOEK,
                                       co.SCHGT,
                                       co.RGEKZ,
                                       co.AUDISP,
                                       co.BWART,
                                       co.SPECIAL_STOCK,
                                       co.ZREST);
            }
            catch (Exception) { }
        }
        public void ActualizarComponentes(EntityConnectionStringBuilder connection, Componentes_de_ordenesPM co)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_Componentes_MDL(co.AUFNR,
                                       co.FOLIO_SAM,
                                       co.POSNR,
                                       co.MATNR,
                                       co.MATXT,
                                       co.MENGE,
                                       co.EINHEIT,
                                       co.POSTP,
                                       co.SOBKZ_D,
                                       co.LGORT,
                                       co.WERKS,
                                       co.VORNR,
                                       co.CHARG,
                                       co.WEMPF,
                                       co.ABLAD,
                                       co.XLOEK,
                                       co.SCHGT,
                                       co.RGEKZ,
                                       co.AUDISP,
                                       co.BWART,
                                       co.SPECIAL_STOCK);
        }
        public void InsertarOrdenes_control(EntityConnectionStringBuilder connection, ORDENES_CONTROL oc)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.Ordenes_control2_mdl(oc.AUFNR,
                                           oc.FOLIO_SAM,
                                           oc.ERNAM,
                                           oc.ERDAT,
                                           oc.AENAM,
                                           oc.AEDAT,
                                           oc.WERKS);
            }
            catch (Exception) { }
        }
        public void ActualizarControl(EntityConnectionStringBuilder connection, ORDENES_CONTROL oc)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_Ordenes_control2_MDL(oc.AUFNR,
                                                oc.FOLIO_SAM,
                                                oc.ERNAM,
                                                oc.ERDAT,
                                                oc.AENAM,
                                                oc.AEDAT,
                                                oc.WERKS);
        }
        public void Insertartexto_actividades(EntityConnectionStringBuilder connection, textos_actividades tx)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.IngresaTextoActividades_mdl(tx.ARBPL
                                                    , tx.TEXT1
                                                    , tx.NOTX1
                                                    , tx.TEXT2
                                                    , tx.NOTX2
                                                    , tx.TEXT3
                                                    , tx.NOTX3
                                                    , tx.TEXT4
                                                    , tx.NOTX4
                                                    , tx.TEXT5
                                                    , tx.NOTX5
                                                    , tx.TEXT6
                                                    , tx.NOTX6
                                                    , tx.WERKS);
            }
            catch (Exception) { }
        }
        public void ActualizarActividades(EntityConnectionStringBuilder connection, textos_actividades tx)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_TextoActividades_MDL(tx.ARBPL,
                                                tx.TEXT1,
                                                tx.NOTX1,
                                                tx.TEXT2,
                                                tx.NOTX2,
                                                tx.TEXT3,
                                                tx.NOTX3,
                                                tx.TEXT4,
                                                tx.NOTX4,
                                                tx.TEXT5,
                                                tx.NOTX5,
                                                tx.TEXT6,
                                                tx.NOTX6,
                                                tx.WERKS);
        }
        public void EliminarRegistroNoti(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_Noti_PP_MDL(folio_sam);
        }
        public void EliminarOrdenDelta(EntityConnectionStringBuilder connection, string orden)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_ORDENES_PP_DELTA_MDL(orden);
        }
    }
}
