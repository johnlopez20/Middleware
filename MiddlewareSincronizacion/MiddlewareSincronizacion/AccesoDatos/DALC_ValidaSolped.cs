using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ValidaSolped
    {
        #region Instacia
        private static DALC_ValidaSolped instance = null;
        private static readonly object padlock = new object();

        public static DALC_ValidaSolped ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ValidaSolped();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarCecos(EntityConnectionStringBuilder connection, Cecos ce)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_cecos_MDL(ce.BUKRS);
        }
        public void IngresaCecos(EntityConnectionStringBuilder connection, Cecos ce)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_cecos_MDL(ce.BUKRS,
                              ce.KSTAR,
                              ce.LTEXT,
                              ce.KOSTL,
                              ce.KTEXT_ES);
        }
    }
}