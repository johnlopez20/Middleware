using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ClaseOrdenesPMSAM
    {
        #region Instacia
        private static DALC_ClaseOrdenesPMSAM instance = null;
        private static readonly object padlock = new object();

        public static DALC_ClaseOrdenesPMSAM ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ClaseOrdenesPMSAM();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELECT_lista_clase_orden_MDL_Result> ObtenerClaseOrden(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_clase_orden_MDL();
        }
    }
}
