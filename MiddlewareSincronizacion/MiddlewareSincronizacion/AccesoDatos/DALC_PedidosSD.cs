using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddlewareSincronizacion.Entidades;
using System.Data.Entity.Core.EntityClient;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_PedidosSD
    {
        #region Instancia
        private static DALC_PedidosSD instance = null;
        private static readonly object padlock = new object();

        public static DALC_PedidosSD ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_PedidosSD();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELEC_datos_pedido_sd_id_MDL_Result> ObtenerDatosIdPedido(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_datos_pedido_sd_id_MDL(id);
        }
        public IEnumerable<SELECT_pedido_sd_valida_hora_MDL_Result> ObtenerValidacionHoraPedido(EntityConnectionStringBuilder connection, int id, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_pedido_sd_valida_hora_MDL(id,
                                                            hora);
        }
        public IEnumerable<SELECT_lista_folios_pedido_sd_MDL_Result> ObtenerTodoFolioPedido(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_pedido_sd_MDL(folio_sam);
        }
        public IEnumerable<SELECT_cabecera_pedidos_sd_crea_Fol_MDL1_Result> ObtenerCabeceraPedidos(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_pedidos_sd_crea_Fol_MDL1(folio_sam);
        }
        public IEnumerable<SELECT_posiciones_pedido_sd_crea_Fol_MDL_Result> ObtenerPosicionesPedidos(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_posiciones_pedido_sd_crea_Fol_MDL(folio_sam);
        }
        public IEnumerable<SELECT_clientes_pedido_sd_crea_Fol_MDL_Result> ObtenerClientesPedidos(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_clientes_pedido_sd_crea_Fol_MDL(folio_sam);
        }
        public IEnumerable<SELECT_cantidades_pedido_sd_crea_Fol_MDL_Result> ObtenerCantidadesPedidos(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cantidades_pedido_sd_crea_Fol_MDL(folio_sam);
        }
        public IEnumerable<SELECT_textos_pedidos_cabecera_sd_crea_Fol_MDL_Result> ObtenerTextosCabPedidos(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_textos_pedidos_cabecera_sd_crea_Fol_MDL(folio_sam);
        }
        public IEnumerable<SELECT_textos_pedidos_posiciones_sd_crea_Fol_MDL_Result> ObtenerTextosPosPedidos(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_textos_pedidos_posiciones_sd_crea_Fol_MDL(folio_sam);
        }
        public void InsertarCabeceraPedidos(EntityConnectionStringBuilder connection, Cabecera_Pedidos_SD cab)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_cabecera_pedidos_venta_vis_MDL(cab.AUART,
                                                          cab.VBELN,
                                                          cab.XBLNR,
                                                          cab.KUNNR,
                                                          cab.TXTPA,
                                                          cab.KUNWE,
                                                          cab.TXTPD,
                                                          cab.BSTNK,
                                                          cab.BSTDK,
                                                          cab.VKORG,
                                                          cab.VTWEG,
                                                          cab.SPART,
                                                          cab.TXT_VTRBER,
                                                          cab.VKBUR,
                                                          cab.BEZEI,
                                                          cab.VKGRP,
                                                          cab.BEZEIGV,
                                                          cab.AUDAT,
                                                          cab.NETWR,
                                                          cab.WAERK,
                                                          cab.ERNAM,
                                                          cab.AUGRU,
                                                          cab.PRSDT,
                                                          cab.VDATU,
                                                          cab.KNUMV);
        }
        public void InsertarPosicionesPedidos(EntityConnectionStringBuilder connection, Posiciones_Pedidos_SD pos)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_posiciones_pedido_venta_vis_MDL(pos.VBELN,
                                                           pos.XBLNR,
                                                           pos.POSNR,
                                                           pos.PRGRS,
                                                           pos.EDATUM,
                                                           pos.MATNR,
                                                           pos.KWMENG,
                                                           pos.VRKME,
                                                           pos.EPMEH,
                                                           pos.ARKTX,
                                                           pos.KDMAT,
                                                           pos.PSTYV,
                                                           pos.PROFL,
                                                           pos.UEPOS,
                                                           pos.KPRGBZ,
                                                           pos.ETDAT,
                                                           pos.GBSTA_BEZ,
                                                           pos.ABGRU,
                                                           pos.BEZEIM,
                                                           pos.LFSTA_BEZ,
                                                           pos.LFGSA_BEZ,
                                                           pos.MGAME,
                                                           pos.NETWR,
                                                           pos.WAERK,
                                                           pos.MWSBP);
        }
        public void InsertarExpedicionPedidos(EntityConnectionStringBuilder connection, Expedicion_Pedidos_SD exp)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_expedicion_pedidos_vis_MDL(exp.VBELN,
                                                      exp.XBLNR,
                                                      exp.POSNR,
                                                      exp.WERKS,
                                                      exp.NAME1,
                                                      exp.LGORT,
                                                      exp.LGOBE,
                                                      exp.VSTEL,
                                                      exp.VTEXT,
                                                      exp.ROUTE,
                                                      exp.BEZEIPX,
                                                      exp.LPRIO,
                                                      exp.BEZEIPE,
                                                      exp.NTGEW,
                                                      exp.GEWEI,
                                                      exp.BRGEW);
        }
        public void InsertarCondicionesPedidos(EntityConnectionStringBuilder connection, Condiciones_Pedidos_SD con)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_condiciones_pedidos_vis_MDL(con.KNUMV,
                                                       "",
                                                       con.KPOSN,
                                                       con.STUNR,
                                                       con.KAPPL,
                                                       con.KINAK,
                                                       con.KSCHL,
                                                       con.VTEXT,
                                                       con.KBETR,
                                                       con.WAERS,
                                                       con.KPEIN,
                                                       con.KMEIN,
                                                       con.KWERT,
                                                       con.KUMZA,
                                                       con.MEINS,
                                                       con.KUMNE,
                                                       con.KMEI1,
                                                       con.KWERT_K,
                                                       con.KWAEH,
                                                       con.KSTAT);
        }
        public void InsertarRepartosPedidos(EntityConnectionStringBuilder connection, Repartos_Pedidos_SD rep)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_repartos_pedidos_vis_MDL(rep.VBELN,
                                                    rep.XBLNR,
                                                    rep.POSNR,
                                                    rep.DELCO,
                                                    rep.KWMENG,
                                                    rep.VRKME,
                                                    rep.LSMENG,
                                                    rep.PRGRS,
                                                    rep.EDATU,
                                                    rep.WMENG,
                                                    rep.CMENG,
                                                    rep.BMENG,
                                                    rep.VRKME_B,
                                                    rep.LIFSP,
                                                    rep.ETTYP,
                                                    rep.BANFN,
                                                    rep.BNFPO);
        }
        public void InsertarTextosCabPedidos(EntityConnectionStringBuilder connection, Textos_Cabecera_Pedidos_SD_Vis txtc)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_textos_cabecera_pedidos_venta_vis_MDL(txtc.VBELN,
                                                                 txtc.FOLIO_SAM,
                                                                 txtc.INDICE,
                                                                 txtc.TDLINE,
                                                                 txtc.UNAME);
        }
        public void InsertarTextosPosPedidos(EntityConnectionStringBuilder connection, Textos_Posiciones_Pedidos_SD_Vis txtp)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_textos_posiciones_pedidos_venta_vis_MDL(txtp.VBELN,
                                                                   txtp.FOLIO_SAM,
                                                                   txtp.POSNR,
                                                                   txtp.INDICE,
                                                                   txtp.TDLINE,
                                                                   txtp.UNAME);
        }
        public void InsertarInforecords(EntityConnectionStringBuilder connection, Inforecords info)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_inforecord_MDL(info.VKORG,
                                          info.VTWEG,
                                          info.KUNNR,
                                          info.MATNR,
                                          info.POSTX);
        }
        public void InsertarInterlocutores(EntityConnectionStringBuilder connection, Interlocutores_pedidos_vis inter)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_interlocutores_pedido_vis_MDL(inter.VBELN,
                                                         inter.XBLNR,
                                                         inter.POSNR,
                                                         inter.PARVW,
                                                         inter.KUNNR,
                                                         inter.XCPDK);
        }
        public void ActualizaPedidoCrea(EntityConnectionStringBuilder connection, CabeceraPedidosSD s)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_pedido_sd_crea_MDL(s.FOLIO_SAM,
                                              s.VBELN,
                                              s.RECIBIDO,
                                              s.PROCESADO,
                                              s.ERROR);
        }
        public void VaciarTablasPedidosSD(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.Truncate_PedidoSD_MDL();
        }
    }
}
