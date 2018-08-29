using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Tipos_Solped
    {
        #region Instacia
        private static DALC_Tipos_Solped instance = null;
        private static readonly object padlock = new object();

        public static DALC_Tipos_Solped ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Tipos_Solped();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarTiposSolped(EntityConnectionStringBuilder connection, Tipos_Solped ts)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_tipo_solped_MDL(ts.WERKS);
        }
        public void IngresaTiposSolped(EntityConnectionStringBuilder connection, Tipos_Solped ts)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_tipo_solped_MDL(ts.WERKS,
                                           ts.BSART,
                                           ts.MTART,
                                           ts.BATXT_ES,
                                           ts.BATXT_EN);
        }
    }
}
