using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Almacenes_Ivend
    {
        #region Instacia
        private static DALC_Almacenes_Ivend instance = null;
        private static readonly object padlock = new object();

        public static DALC_Almacenes_Ivend ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Almacenes_Ivend();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarAlmacenesIvend(EntityConnectionStringBuilder connection, Almacenes_Ivend almi)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_almacenes_ivend_MDL(almi.WERKS,
                                                 almi.LGORT,
                                                 almi.IVEND,
                                                 almi.ILGORT,
                                                 almi.LOCATION);
        }
        public void IngresaAlmacenesIvend(EntityConnectionStringBuilder connection, Almacenes_Ivend almi)
        {
            var context = new samEntities(connection.ToString());
            context.almacenes_ivend_MDL(almi.WERKS,
                                        almi.LGORT,
                                        almi.IVEND,
                                        almi.ILGORT,
                                        almi.LOCATION);
        }
    }
}
