using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Materiales
    {
        #region Instancia
        private static DALC_Materiales instance = null;
        private static readonly object padlock = new object();

        public static DALC_Materiales ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Materiales();
                }
                return instance;
            }
        }

        #endregion

        public void IngresaMaterial(EntityConnectionStringBuilder connection, Materiales mat)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_Materiales_MDL(mat.MATNR,
                                         mat.WERKS,
                                         mat.MEINS,
                                         mat.BISMT,
                                         mat.MATKL,
                                         mat.MTART,
                                         mat.TRAGR,
                                         mat.EKGRP,
                                         mat.XCHPF,
                                         mat.MAKTX_ES,
                                         mat.MAKTX_EN,
                                         mat.DISMM,
                                         mat.MINBE,
                                         mat.LFRHY,
                                         mat.FXHOR,
                                         mat.DISPO,
                                         mat.DISLS,
                                         mat.BSTMI,
                                         mat.BSTMA,
                                         mat.MABST,
                                         mat.BESKZ,
                                         mat.SOBSL,
                                         mat.WEBAZ,
                                         mat.PLIFZ,
                                         mat.EISBE,
                                         mat.EISLO,
                                         mat.QMATV,
                                         mat.QMPUR,
                                         mat.SPART,
                                         mat.MTPOS_MARA,
                                         mat.MTVFP,
                                         mat.PRCTR,
                                         mat.LADGR,
                                         mat.VRKME,
                                         mat.KONDM,
                                         mat.VERSG,
                                         mat.KTGRM,
                                         mat.MTPOS,
                                         mat.PRODH,
                                         mat.VKORG,
                                         mat.VKGRP,
                                         mat.VTWEG,
                                         mat.VPRSV,
                                         mat.VERPR,
                                         mat.STPRS,
                                         mat.PEINH,
                                         mat.BWTAR,
                                         mat.BKLAS,
                                         mat.LOEKZ,
                                         mat.LOEKZC,
                                         mat.MFRPN,
                                         mat.WRKST);
        }


        //public void BorrarMateriales(EntityConnectionStringBuilder connection, Materiales mat)
        //{
        //    var context = new samEntities(connection.ToString());
        //    context.DeleteMateriales_MDL(mat.MATNR,
        //                                 mat.WERKS);
        //}
        public void BorrarMateriales(EntityConnectionStringBuilder connection, string centro)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_materiales_MDL(centro);
        }
        public void IngresaServicio(EntityConnectionStringBuilder connection, Servicios se)
        {
            var context = new samEntities(connection.ToString());
            context.InsertServicios_MDL(se.ASNUM,
                                        se.ASKTX_ES,
                                        se.ASKTX_EN,
                                        se.MEINS,
                                        se.BKLAS,
                                        se.BKBEZ_ES,
                                        se.BKBEZ_EN,
                                        se.MATKL,
                                        se.SPART,
                                        se.ASTYP,
                                        se.ASTYP_TXT_ES,
                                        se.ASTYP_TXT_EN,
                                        se.LVORM);
        }


        public void BorrarServicios(EntityConnectionStringBuilder connection, Servicios serv)
        {
            var context = new samEntities(connection.ToString());
            context.DeleteServicios_MDL(serv.ASNUM);
        }
        public void EliminarConversiones(EntityConnectionStringBuilder connection, MaterialesConversion mc)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_materiales_conversion_ventas_MDL(mc.MATNR);
        }
        public void IngresarMaterialesConversion(EntityConnectionStringBuilder connection, MaterialesConversion mc)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_materiales_conversion_ventas_MDL(mc.MATNR,
                                                            mc.UMREZ,
                                                            mc.MEINS,
                                                            mc.UMREN,
                                                            mc.BSTME,
                                                            mc.LVORM,
                                                            mc.VRKME);
        }
    }
}
