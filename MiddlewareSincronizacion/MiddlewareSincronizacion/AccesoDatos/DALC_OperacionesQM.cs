using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_OperacionesQM
    {
        #region Instancia
        private static DALC_OperacionesQM instance = null;
        private static readonly object padlock = new object();

        public static DALC_OperacionesQM ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_OperacionesQM();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarOperacionesQM(EntityConnectionStringBuilder connection, OperacionesQM op)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_operaciones_planes_inspeccion_MDL(op.MATNR,
                                                             op.WERKS);
        }
        public void IngresarOperacionesQM(EntityConnectionStringBuilder connection, OperacionesQM op)
        {
            var context = new samEntities(connection.ToString());
            context.operaciones_planes_inspeccion_MDL(op.MATNR,
                                                      op.WERKS,
                                                      op.PLNTY,
                                                      op.PLNNR,
                                                      op.PLNAL,
                                                      op.ZKRIZ,
                                                      op.ZAEHL,
                                                      op.PLNFL,
                                                      op.PLNKN,
                                                      op.VORNR,
                                                      op.STEUS,
                                                      op.LTXA1,
                                                      op.QPPKTABS,
                                                      op.MAKTX,
                                                      op.RENGLON);
        }
    }
}
