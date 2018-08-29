using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_DecisionEmpleo
    {
        #region Instacia
        private static DALC_DecisionEmpleo instance = null;
        private static readonly object padlock = new object();

        public static DALC_DecisionEmpleo ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_DecisionEmpleo();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELECT_decision_empleo_lote_crea_MDL_Result> ObtenerDecisionesEmpleo(EntityConnectionStringBuilder connection)
        {
            string fol = "";
            var context = new samEntities(connection.ToString());
            return context.SELECT_decision_empleo_lote_crea_MDL(fol);
        }
        public void ActualizarDecisionEmpleo(EntityConnectionStringBuilder connection, DecisionEmpleo de)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_decision_empleo_lote_crea_MDL(de.FOLIO_SAM,
                                                         de.RECIBIDO);
        }
        public IEnumerable<SELECT_textos_decision_empleo_MDL_Result> ObtenerTextosDecisionEmpleo(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_textos_decision_empleo_MDL();
        }
    }
}
