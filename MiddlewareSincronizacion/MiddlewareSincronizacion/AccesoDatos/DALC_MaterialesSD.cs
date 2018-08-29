using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_MaterialesSD
    {
        #region Instancia
        private static DALC_MaterialesSD instance = null;
        private static readonly object padlock = new object();

        public static DALC_MaterialesSD ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_MaterialesSD();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarMateriales(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_materiales_ventas_MDL();
        }
        public void InsertarMaterialesVentas(EntityConnectionStringBuilder connection, MaterialesSD ma)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_materiales_venta_MDL(ma.MATNR,
                                                ma.WERKS,
                                                ma.MEINS,
                                                ma.BISMT,
                                                ma.MATKL,
                                                ma.MTART,
                                                ma.EKGRP,
                                                ma.XCHPF,
                                                ma.MAKTX_ES,
                                                ma.MAKTX_EN,
                                                ma.DISMM,
                                                ma.MINBE,
                                                ma.LFRHY,
                                                ma.FXHOR,
                                                ma.DISPO,
                                                ma.DISLS,
                                                ma.BSTMI,
                                                ma.BSTMA,
                                                ma.MABST,
                                                ma.BESKZ,
                                                ma.SOBSL,
                                                ma.WEBAZ,
                                                ma.PLIFZ,
                                                ma.EISBE,
                                                ma.EISLO,
                                                ma.QMATV,
                                                ma.QMPUR,
                                                ma.SPART,
                                                ma.MTPOS_MARA,
                                                ma.MTVFP,
                                                ma.PRCTR,
                                                ma.LADGR,
                                                ma.VRKME,
                                                ma.KONDM,
                                                ma.VERSG,
                                                ma.KTGRM,
                                                ma.MTPOS,
                                                ma.PRODH,
                                                ma.VKORG,
                                                ma.VKGRP,
                                                ma.VTWEG,
                                                ma.VPRSV,
                                                ma.VERPR,
                                                ma.STPRS,
                                                ma.PEINH,
                                                ma.BWTAR,
                                                ma.BKLAS,
                                                ma.TRAGR,
                                                ma.LOEKZ,
                                                ma.LOEKZC,
                                                ma.MFRPN,
                                                ma.WRKST);
        }
        public void InsertarTextosCom(EntityConnectionStringBuilder connection, TextoComercialMaterialesSD txt)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_textos_comercial_material_MDL(txt.MATNR,
                                                         txt.VKORG,
                                                         txt.SPART,
                                                         txt.INDICE,
                                                         txt.TDFORMAT,
                                                         txt.TDLINE);
        }
    }
}
