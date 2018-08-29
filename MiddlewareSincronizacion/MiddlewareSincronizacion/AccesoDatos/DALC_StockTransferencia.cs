using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_StockTransferencia
    {
        #region Instancia
        private static DALC_StockTransferencia instance = null;
        private static readonly object padlock = new object();

        public static DALC_StockTransferencia obtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_StockTransferencia();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELECT_Validar_existe_material_MDL_Result> ValidarMaterial(EntityConnectionStringBuilder connection, string centro, string material)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_Validar_existe_material_MDL(centro,
                                                              material);
        }
        public void ActulizarMaterialStockTransferencia(EntityConnectionStringBuilder connection, string centro, string material, string stock)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_Stock_Transferencia_MDL(centro,
                                                   material,
                                                   stock,
                                                   stock);
        }
        public void ActualizarMaterialStockTransfer(EntityConnectionStringBuilder connection, StockTransferencia sk)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_STOCK_TRANS_MDL(sk.WERKS,
                                           sk.MATNR,
                                           sk.UMLMC,
                                           sk.XCHPF,
                                           sk.MEINS,
                                           sk.MAKTX,
                                           sk.MAKTX_E,
                                           "",
                                           sk.LVORM,
                                           sk.LGORT,
                                           sk.CHARG,
                                           sk.SOBKZ,
                                           sk.KDAUF,
                                           sk.KDPOS,
                                           sk.UMLGO);
        }
        public void VaciarTablaStockTraslado(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.Delete_StockTransferecia_MDL();
        }
        public void IngresarStockTransferencia(EntityConnectionStringBuilder connection, StockTransferencia sk)
        {
            var context = new samEntities(connection.ToString());
            context.InsertStockTransferecia_MDL(sk.WERKS,
                                                sk.MATNR,
                                                sk.UMLMC,
                                                sk.XCHPF,
                                                sk.MEINS,
                                                sk.MAKTX,
                                                sk.MAKTX_E,
                                                sk.UMLMC,
                                                sk.LVORM);
        }
        public void IngresarStockTransfer(EntityConnectionStringBuilder connection, StockTransferencia sk)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_Stock_Transfer_MDL(sk.WERKS,
                                              sk.MATNR,
                                              sk.UMLMC,
                                              sk.XCHPF,
                                              sk.MEINS,
                                              sk.MAKTX,
                                              sk.MAKTX_E,
                                              "",
                                              sk.LVORM,
                                              sk.LGORT,
                                              sk.CHARG,
                                              sk.SOBKZ,
                                              sk.KDAUF,
                                              sk.KDPOS,
                                              sk.UMLGO);
        }
        public IEnumerable<SELECT_MATERIAL_STOCK_TRANSFER_MDL_Result> ValidarMaterialST(EntityConnectionStringBuilder connection, StockTransferencia almid)
        {
            var context = new samEntities(connection.ToString());
            try
            {
                return context.SELECT_MATERIAL_STOCK_TRANSFER_MDL(almid.WERKS,
                                                      almid.MATNR,
                                                      almid.LGORT,
                                                      almid.CHARG,
                                                      almid.KDAUF,
                                                      almid.KDPOS,
                                                      almid.UMLGO,
                                                      almid.SOBKZ);
            }
            catch (Exception) 
            {
                return context.SELECT_MATERIAL_STOCK_TRANSFER_MDL(almid.WERKS,
                                                       almid.MATNR,
                                                       almid.LGORT,
                                                       almid.CHARG,
                                                       almid.KDAUF,
                                                       almid.KDPOS,
                                                       almid.UMLGO,
                                                       almid.SOBKZ);
            }
        }
        public void ActualizarStockTransfer313(EntityConnectionStringBuilder connection, StockTransferencia st, string stock)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_Stock_Transferencia_313_MDL(st.WERKS,
                                                       st.MATNR,
                                                       st.LGORT,
                                                       st.CHARG,
                                                       st.SOBKZ,
                                                       st.KDAUF,
                                                       st.KDPOS,
                                                       st.UMLGO,
                                                       stock,
                                                       stock,
                                                       st.LVORM,
                                                       st.XCHPF);
        }
    }
}
