using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Conteo_Inventario
    {
        #region Instacia
        private static DALC_Conteo_Inventario instance = null;
        private static readonly object padlock = new object();

        public static DALC_Conteo_Inventario ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Conteo_Inventario();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELEC_datos_contador_sd_id_MDL_Result> ObtenerDatosIdConteo(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_datos_contador_sd_id_MDL(id);
        }
        public IEnumerable<SELECT_conteo_sd_valida_hora_MDL_Result> ObtenerValidacionHoraConteo(EntityConnectionStringBuilder connection, int id, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_conteo_sd_valida_hora_MDL(id,
                                                            hora);
        }
        public IEnumerable<SELECT_lista_folios_conteo_sd_MDL_Result> ObtenerTodoFolioConteo(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_conteo_sd_MDL(folio_sam);
        }
        public IEnumerable<SELECT_cabecera_recuento_inventario_crea_Fol_MDL_Result> ObtenerCabeceraConteo(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_recuento_inventario_crea_Fol_MDL(folio_sam);
        }
        public IEnumerable<SELECT_posiciones_recuento_inventario_crea_Fol_MDL_Result> ObtenerPosicionesConteo(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_posiciones_recuento_inventario_crea_Fol_MDL(folio_sam);
        }
        public void ActualizaConteoCrea(EntityConnectionStringBuilder connection, Cabecera_Recuento_Inventario s)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_cabecera_recuento_inventario_crea_MDL(s.FOLIO_SAM,
                                                                 s.IBLNR_REC,
                                                                 s.RECIBIDO,
                                                                 s.PROCESADO,
                                                                 s.ERROR);
        }
    }
}
