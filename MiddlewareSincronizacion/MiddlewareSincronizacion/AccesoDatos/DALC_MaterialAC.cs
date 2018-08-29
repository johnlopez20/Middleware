using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_MaterialAC
    {
        #region Instancia
        private static DALC_MaterialAC instance = null;
        private static readonly object padlock = new object();

        public static DALC_MaterialAC ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_MaterialAC();
                }
                return instance;
            }
        }
        #endregion
        public void borraMaterialAC(EntityConnectionStringBuilder connection, Material_Almacen_Centro mac)
        {
            var context = new samEntities(connection.ToString());
            context.DeleteMaterialAlmacenCentro_MDL(mac.MATNR);
        }
        public void ingresaMaterialAC(EntityConnectionStringBuilder connection, Material_Almacen_Centro ma)
        {
            var context = new samEntities(connection.ToString());
            context.InsertMaterialAlmacenCentro_MDL(ma.MATNR,
                                                    ma.ACTUALIZADO,
                                                    ma.LVORM);
        }
    }
}
