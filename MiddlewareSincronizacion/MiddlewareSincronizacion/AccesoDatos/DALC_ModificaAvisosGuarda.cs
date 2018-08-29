using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ModificaAvisosGuarda
    {
        #region Instacia
        private static DALC_ModificaAvisosGuarda instance = null;
        private static readonly object padlock = new object();

        public static DALC_ModificaAvisosGuarda ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ModificaAvisosGuarda();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELECT_txtavical_datos_id_MDL_Result> ObtenerDatosIdAvisoModif(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_txtavical_datos_id_MDL(id);
        }
        public IEnumerable<SELECT_txtavical_valida_hora_MDL_Result> ObtenerHoraAvisoMod(EntityConnectionStringBuilder connection, int id, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_txtavical_valida_hora_MDL(id,
                                                            hora);
        }
        public IEnumerable<SELEC_fol_txtavical_menos_MDL_Result> ObtenerFolioMenosAviMod(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_fol_txtavical_menos_MDL(id);
        }
        public IEnumerable<SELECT_lista_folios_txtavical_MDL_Result> ObtenerListaFolios(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_txtavical_MDL(folio_sam);
        }
    }
}
