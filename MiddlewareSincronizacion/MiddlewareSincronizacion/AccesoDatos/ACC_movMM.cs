using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class ACC_movMM
    {
        #region Instancia
        private static ACC_movMM instance = null;
        private static readonly object padlock = new object();

        public static ACC_movMM ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ACC_movMM();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELEC_datos_Mov_MM_id_MDL_Result> ObtenerDatosIdMov(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_datos_Mov_MM_id_MDL(id);
        }
        public IEnumerable<SELECT_Mov_MM_valida_hora_MDL_Result> ObtenerValidacionHoraMov(EntityConnectionStringBuilder connection, int id, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_Mov_MM_valida_hora_MDL(id,
                                                         hora);
        }
        public IEnumerable<SELECT_lista_folios_Mov_MM_MDL_Result> ObtenerTodoFolioMov(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_Mov_MM_MDL(folio_sam);
        }
        public IEnumerable<SELECT_cabMov_MDL_Result> obtenerCabMov(EntityConnectionStringBuilder connection, string folio)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabMov_MDL(folio);
        }
        public IEnumerable<SELECT_posMov_MDL_Result> obtenerPosMov(EntityConnectionStringBuilder connection, string folio)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_posMov_MDL(folio);
        }
        public void ActualizarResultado(EntityConnectionStringBuilder connection, string folio, string num_doc, string mensaje)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_Mov_MM_MDL(folio,
                                      num_doc,
                                      mensaje);
        }
        public void ActualizaInvMM(EntityConnectionStringBuilder connection, Inventario i)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_INV_MM_MDL(i.MATNR,
                                      i.MAKTX_ES,
                                      i.MAKTX_EN,
                                      i.WERKS,
                                      i.MEINS,
                                      i.LGORT,
                                      i.LGOBE,
                                      i.CLABS,
                                      i.CINSM,
                                      i.CSPEM,
                                      i.CUMLM,
                                      i.CHARG,
                                      i.MTART,
                                      i.MATKL,
                                      i.SERNR,
                                      i.XCHPF,
                                      i.CLABS,
                                      "",
                                      "",
                                      i.CUMLM);
        }
        public void ActulizarInvEE(EntityConnectionStringBuilder connection, InventarioEE i)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_INV_EE_MDL(i.MATNR,
                                      i.MAKTX_ES,
                                      i.MAKTX_EN,
                                      i.WERKS,
                                      i.MEINS,
                                      i.LGORT,
                                      i.LGOBE,
                                      i.CLABS,
                                      i.CINSM,
                                      i.CSPEM,
                                      i.CUMLM,
                                      i.CHARG,
                                      i.MTART,
                                      i.MATKL,
                                      i.SERNR,
                                      i.XCHPF,
                                      i.SOBKZ,
                                      i.VBELN,
                                      i.POSNR);
        }
        public IEnumerable<VALIDA_MATERIAL_INV_MM_MDL_Result> ValidarMaterialINMM(EntityConnectionStringBuilder connection, Inventario i)
        {
            var context = new samEntities(connection.ToString());
            return context.VALIDA_MATERIAL_INV_MM_MDL(i.WERKS,
                                                      i.MATNR,
                                                      i.LGORT,
                                                      i.CHARG);
        }
        public IEnumerable<VALIDA_MATERIAL_INV_EE_MDL_Result> ValidarMaterialINEE(EntityConnectionStringBuilder connection, InventarioEE i)
        {
            var context = new samEntities(connection.ToString());
            return context.VALIDA_MATERIAL_INV_EE_MDL(i.WERKS,
                                                      i.MATNR,
                                                      i.LGORT,
                                                      i.CHARG,
                                                      i.SOBKZ,
                                                      i.VBELN,
                                                      i.POSNR);
        }
        public void InsertarMM(EntityConnectionStringBuilder connection, Inventario i)
        {
            var context =  new samEntities(connection.ToString());
            context.INSERT_Inventario_MM_MDL(i.MATNR,
                                             i.MAKTX_ES,
                                             i.MAKTX_EN,
                                             i.WERKS,
                                             i.MEINS,
                                             i.LGORT,
                                             i.LGOBE,
                                             i.CLABS,
                                             i.CINSM,
                                             i.CSPEM,
                                             i.CUMLM,
                                             i.CHARG,
                                             i.MTART,
                                             i.MATKL,
                                             i.SERNR,
                                             i.XCHPF,
                                             i.CLABS,
                                             "",
                                             "",
                                             "");
        }
        public void InsertarEE(EntityConnectionStringBuilder connection, InventarioEE e)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_Inventario_EE_MDL(e.MATNR,
                                             e.MAKTX_ES,
                                             e.MAKTX_EN,
                                             e.WERKS,
                                             e.MEINS,
                                             e.LGORT,
                                             e.LGOBE,
                                             e.CLABS,
                                             e.CINSM,
                                             e.CSPEM,
                                             e.CUMLM,
                                             e.CHARG,
                                             e.MTART,
                                             e.MATKL,
                                             e.SERNR,
                                             e.XCHPF,
                                             e.SOBKZ,
                                             e.VBELN,
                                             e.POSNR);
        }
        public void ElimanarRegistroCreacion(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_MOV_FOL_MDL(folio_sam);
        }
    }
}
