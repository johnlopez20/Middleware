using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_MaterialesAlmacen
    {
        #region Instancia
        private static DALC_MaterialesAlmacen instance = null;
        private static readonly object padlock = new object();

        public static DALC_MaterialesAlmacen ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_MaterialesAlmacen();
                }
                return instance;
            }
        }
        #endregion
        public void BorrarMateralAlm303(EntityConnectionStringBuilder connection, MaterialesAlmacen adm)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_materiales_almacen_303_MDL(adm.MATNR,
                                                      adm.WERKS);
        }
        public void IngresarMaterialesAlmacen_303(EntityConnectionStringBuilder connection, MaterialesAlmacen adm)
        {
            var context = new samEntities(connection.ToString());
            context.materiales_almacen_303_MDL(adm.MATNR,
                                               adm.WERKS,
                                               adm.LGORT,
                                               adm.MAKTX,
                                               adm.LVORM);
        }
        public void IngresarMaterialesAlmacen(EntityConnectionStringBuilder connection, MaterialesAlmacen adm)
        {
            var context = new samEntities(connection.ToString());
            context.materiales_almacen_MDL(adm.MATNR,
                                            adm.WERKS,
                                            adm.LGORT,
                                            adm.MAKTX,
                                            adm.LVORM);
        }
        public void BorrarMateralAlm(EntityConnectionStringBuilder connection, MaterialesAlmacen adm)
        {
            var context = new samEntities(connection.ToString());
            context.materiales_almacen_delete_MDL(adm.MATNR, adm.WERKS);
        }
        public void BorrarMaterialesAlmacen(EntityConnectionStringBuilder connection, string centro)
        {
            string nada = "";
            var context = new samEntities(connection.ToString());
            context.materiales_almacen_delete_centro_MDL(centro,
                                                         nada);
        }
    }
}
