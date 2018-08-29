using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_CalidadNotificaciones
    {
        #region Instacia
        private static DALC_CalidadNotificaciones instance = null;
        private static readonly object padlock = new object();

        public static DALC_CalidadNotificaciones ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_CalidadNotificaciones();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELECT_lotepm_datos_id_MDL_Result> ObtenerDatosIdLotePM(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lotepm_datos_id_MDL(id);
        }
        public IEnumerable<SELECT_lotepm_valida_hora_MDL_Result> ObtenerValidacionHoraLotePM(EntityConnectionStringBuilder connection, int id, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lotepm_valida_hora_MDL(id,
                                                         hora);
        }
        public IEnumerable<SELEC_fol_lotepm_menos_MDL_Result> ObtenerFolioMenosLotePM(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_fol_lotepm_menos_MDL(id);
        }
        public IEnumerable<SELECT_lista_folios_lotepm_MDL_Result> ObtenerTodoFolioLotePM(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_lotepm_MDL(folio_sam);
        }
        public IEnumerable<SELECT_cabecera_lotes_inspeccion_notificaciones_crea_list_MDL_Result> ObtenerListaLotesPM(EntityConnectionStringBuilder connection, string fecha, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_lotes_inspeccion_notificaciones_crea_list_MDL(fecha,
                                                                                         hora);
        }
        public IEnumerable<SELECT_cabecera_lotes_inspeccion_notificaciones_crea_Folio_MDL_Result> ObtenerCabLotInsFolio(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_lotes_inspeccion_notificaciones_crea_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_posiciones_lotes_inspeccion_notificaciones_crea_F_MDL_Result> ObtenerPosLotInsFolio(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_posiciones_lotes_inspeccion_notificaciones_crea_F_MDL(folio_sam);
        }
        public IEnumerable<SELECT_cabecera_lotes_inspeccion_notificaciones_crea_MDL_Result> ObtenerCabeceraLoteInsNot(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_lotes_inspeccion_notificaciones_crea_MDL();
        }
        public IEnumerable<SELECT_posiciones_lotes_inspeccion_notificaciones_crea_MDL_Result> ObtenerPosLotInsNot(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_posiciones_lotes_inspeccion_notificaciones_crea_MDL();
        }
        public void ActulizarCabLotInsNot(EntityConnectionStringBuilder connection, CalidadCabNot cabnot)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_cabecera_lotes_inspeccion_notificaciones_crea_MDL(cabnot.FOLIO_SAM,
                                                                             cabnot.RECIBIDO);
        }
        public void ActulizaPosLotInsNot(EntityConnectionStringBuilder connection, CalidadPosNot posnot)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_posiciones_lotes_inspeccion_notificaciones_crea_MDL(posnot.FOLIO_SAM,
                                                                               posnot.RECIBIDO);
        }
        public IEnumerable<SELECT_decision_empleo_lote_crea2_Folio_MDL_Result> ObtenerDecisionesEmploeFolio(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_decision_empleo_lote_crea2_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_textos_decision_empleo_lote_crea2_Folio_MDL_Result> ObtenerTextosResultados(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_textos_decision_empleo_lote_crea2_Folio_MDL(folio_sam);
        }
        public void ActualizarDecisionEmpleo2(EntityConnectionStringBuilder connection, DecisionEmpleo de)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_decision_empleo_lote_crea2_MDL(de.FOLIO_SAM,
                                                         de.RECIBIDO);
        }
    }
}
