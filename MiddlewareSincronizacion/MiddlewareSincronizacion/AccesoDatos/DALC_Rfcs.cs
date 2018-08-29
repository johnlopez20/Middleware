using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Rfcs
    {
        #region Instancia
        private static DALC_Rfcs instance = null;
        private static readonly object padlock = new object();

        public static DALC_Rfcs obtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Rfcs();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<sincronizacion_input_MDL_Result> ObtenerSincronizacionInput(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.sincronizacion_input_MDL();
        }
    }
}
