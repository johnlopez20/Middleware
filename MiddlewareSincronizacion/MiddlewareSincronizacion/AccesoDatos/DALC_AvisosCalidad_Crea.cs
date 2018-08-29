using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_AvisosCalidad_Crea
    {
        #region Instacia
        private static DALC_AvisosCalidad_Crea instance = null;
        private static readonly object padlock = new object();

        public static DALC_AvisosCalidad_Crea ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_AvisosCalidad_Crea();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELECT_cabecera_avisos_textos_calidad_crea_Folio_MDL_Result> ObtenerCabeceraTextosAvisosCalidad(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_avisos_textos_calidad_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_posiciones_avisos_textos_calidad_crea_Folio_MDL_Result> ObtenerPosicionesAvisosCalidad(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_posiciones_avisos_textos_calidad_crea_Folio_MDL(folio_sam);
        }
        public void ActualizarCabTextoAvi(EntityConnectionStringBuilder connection, TextoAvisosCalidadCrea txt)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_cabecera_avisos_textos_calidad_crea_MDL(txt.FOLIO_SAM,
                                                                   txt.RECIBIDO);
        }
        public void ActualicarPosTextoAvi(EntityConnectionStringBuilder connection, TextoPosAvisosCalidad tpo)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_posiciones_avisos_textos_calidad_crea_MDL(tpo.FOLIO_SAM,
                                                                     tpo.RECIBIDO);
        }
    }
}
