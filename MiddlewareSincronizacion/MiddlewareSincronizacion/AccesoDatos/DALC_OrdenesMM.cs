using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_OrdenesMM
    {
        #region Instancia
        private static DALC_OrdenesMM instance = null;
        private static readonly object padlock = new object();

        public static DALC_OrdenesMM ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_OrdenesMM();
                }
                return instance;
            }
        }

        #endregion
        public void VaciarORDENES_MM(EntityConnectionStringBuilder connection, Ordenes_MM omm)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_ordenes_mm_MDL(omm.AUFNR);
        }

        public void IngresaORDENES_MM(EntityConnectionStringBuilder connection, Ordenes_MM rm)
        {
            var context = new samEntities(connection.ToString());
            context.ordenes_mm_MDL(rm.AUART,
                                          rm.KOKRS,
                                          rm.AUFNR,
                                          rm.KTEXT,
                                          rm.ESTATUS,
                                          "",
                                          rm.LVORM);
        }
    }
}
