using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;


namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_CierreAvisosLigue
    {
        #region Instacia
        private static DALC_CierreAvisosLigue instance = null;
        private static readonly object padlock = new object();

        public static DALC_CierreAvisosLigue ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_CierreAvisosLigue();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELECT_cierre_ligue_avisos_list_MDL_Result> ObtenerListaCierreAvisos(EntityConnectionStringBuilder connection, string fecha, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cierre_ligue_avisos_list_MDL(fecha,
                                                               hora);
        }
        public IEnumerable<SELECT_cierre_ligue_avisos_MDL_Result> ObtenerCierreFolio(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cierre_ligue_avisos_MDL();
        }
        public IEnumerable<SELECT_cierre_ligue_avisos_MDL_Result> ObtenerAvisosCierreLigue(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cierre_ligue_avisos_MDL();
        }
        public void ActulizaCierreAvisosLigue(EntityConnectionStringBuilder connection, CierreAvisosLigue cie)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_cierre_ligue_avisos_MDL(cie.FOLIO_SAM,
                                                   cie.RECIBIDO);
        }
    }
}
