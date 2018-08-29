using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Obyc
    {
        #region Instancia
        private static DALC_Obyc instance = null;
        private static readonly object padlock = new object();

        public static DALC_Obyc ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Obyc();
                }
                return instance;
            }
        }
        #endregion
        public void borraObyc(EntityConnectionStringBuilder connection, OBYC obyc)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_obyc_MDL(obyc.KTOPL);
        }
        public void InsertarObyc(EntityConnectionStringBuilder connection, OBYC ob)
        {
            var context = new samEntities(connection.ToString());
            context.obyc_MDL(ob.KTOPL,
                             ob.KTOSL,
                             ob.KOMOK,
                             ob.BKLAS,
                             ob.KONTS);
        }
    }
}
