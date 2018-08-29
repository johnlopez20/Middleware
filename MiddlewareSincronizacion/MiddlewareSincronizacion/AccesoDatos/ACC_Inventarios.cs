using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class ACC_Inventarios
    {
        #region Instancia
        private static ACC_Inventarios instance = null;
        private static readonly object padlock = new object();

        public static ACC_Inventarios ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ACC_Inventarios();
                }
                return instance;
            }
        }
        #endregion
        public void InsertarInvRuta(EntityConnectionStringBuilder connection, Inventarios inv)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.InsertaInv_mdl(inv.MATNR,
                                      inv.MAKTX_ES,
                                      inv.MAKTX_EN,
                                      inv.WERKS,
                                      inv.MEINS,
                                      inv.LGORT,
                                      inv.LGOBE,
                                      inv.CLABS,
                                      inv.CINSM,
                                      inv.CSPEM,
                                      inv.CUMLM,
                                      inv.CHARG,
                                      inv.MTART,
                                      inv.MATKL,
                                      inv.SERNR,
                                      inv.XCHPF,
                                      inv.ult_stock_libre,
                                      inv.cnt_cent_dest,
                                      inv.ult_cnt_dest,
                                      inv.ult_trans);
            }
            catch (Exception) { }
        }
        public void InsertarInvRuta_Reservas(EntityConnectionStringBuilder connection, Inventarios inv)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.INSERT_INV_RESERVAS_MDL(inv.MATNR,
                                      inv.MAKTX_ES,
                                      inv.MAKTX_EN,
                                      inv.WERKS,
                                      inv.MEINS,
                                      inv.LGORT,
                                      inv.LGOBE,
                                      inv.CLABS,
                                      inv.CINSM,
                                      inv.CSPEM,
                                      inv.CUMLM,
                                      inv.CHARG,
                                      inv.MTART,
                                      inv.MATKL,
                                      inv.SERNR,
                                      inv.XCHPF,
                                      inv.ult_stock_libre,
                                      inv.cnt_cent_dest,
                                      inv.ult_cnt_dest,
                                      inv.ult_trans);
            }
            catch (Exception) { }
        }
        public void InsertarInvEE(EntityConnectionStringBuilder connection, Inventarios inv)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.InsertaInvEE_mdl(inv.MATNR,
                                        inv.MAKTX_ES,
                                        inv.MAKTX_EN,
                                        inv.WERKS,
                                        inv.MEINS,
                                        inv.LGORT,
                                        inv.LGOBE,
                                        inv.CLABS,
                                        inv.CINSM,
                                        inv.CSPEM,
                                        inv.CUMLM,
                                        inv.CHARG,
                                        inv.MTART,
                                        inv.MATKL,
                                        inv.SERNR,
                                        inv.XCHPF,
                                        inv.SOBKZ,
                                        inv.VBELN,
                                        inv.POSNR);
            }
            catch (Exception) { }
        }
        public void InsertarInvEE_Rservas(EntityConnectionStringBuilder connection, Inventarios inv)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.INSERT_INVEE_RESERVAS_MDL(inv.MATNR,
                                        inv.MAKTX_ES,
                                        inv.MAKTX_EN,
                                        inv.WERKS,
                                        inv.MEINS,
                                        inv.LGORT,
                                        inv.LGOBE,
                                        inv.CLABS,
                                        inv.CINSM,
                                        inv.CSPEM,
                                        inv.CUMLM,
                                        inv.CHARG,
                                        inv.MTART,
                                        inv.MATKL,
                                        inv.SERNR,
                                        inv.XCHPF,
                                        inv.SOBKZ,
                                        inv.VBELN,
                                        inv.POSNR);
            }
            catch (Exception) { }
        }
        public void BorraMatAlamcenXcentro(EntityConnectionStringBuilder connection, string centro)
        {
            var context = new samEntities(connection.ToString());
            context.deleteMatAlmCentro_mdl(centro);
        }
        public void InsertarMaterialAlmacen(EntityConnectionStringBuilder connection, Materiales_Almacen ma)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.InsertMatAlm_mdl(ma.MATNR,
                                        ma.WERKS,
                                        ma.LGORT,
                                        ma.MAKTX_ES,
                                        ma.LVORM,
                                        ma.HABLT);
            }
            catch (Exception) { }
        }
        public void BorraStockTransferencia(EntityConnectionStringBuilder connection, string centro)
        {
            var context = new samEntities(connection.ToString());
            context.deleteStockTransferenciaCentro_V2(centro);
        }
        public void InsertarStockTransferencia(EntityConnectionStringBuilder connection, Stock_Transeferencia st)
        {
            try
            {
                var contex = new samEntities(connection.ToString());
                contex.INSERT_STOCKTRASNFER_MDL(st.WERKS,
                                               st.MATNR,
                                               st.UMLMC,
                                               st.XCHPF,
                                               st.MEINS,
                                               st.MAKTX,
                                               st.MAKTX_E,
                                               "",
                                               st.LVORM,
                                               st.UMLGO,
                                               st.CHARG,
                                               st.SOBKZ,
                                               st.KDAUF,
                                               st.KDPOS,
                                               st.LGORT);
            }
            catch (Exception) { }
        }
        public IEnumerable<VALIDA_MOVI_ERROR_MDL_Result> ValidarErrores(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.VALIDA_MOVI_ERROR_MDL();
        }
        public void TruncateInventarioTotal(EntityConnectionStringBuilder conncetion, string centro)
        {
            var context = new samEntities(conncetion.ToString());
            context.TRUNCATE_Inventario_MDL(centro);
        }
        public void TRuncateIventarioReservas(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_Inventario_reservas_MDL();
        }
    }
}
