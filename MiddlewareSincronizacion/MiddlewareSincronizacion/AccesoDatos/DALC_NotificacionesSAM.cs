using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_NotificacionesSAM
    {
        #region Instancia
        private static DALC_NotificacionesSAM instance = null;
        private static readonly object padlock = new object();

        public static DALC_NotificacionesSAM ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_NotificacionesSAM();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELECT_notificaciones_datos_id_MDL_Result> ObtenerDatosIdNotificaciones(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_notificaciones_datos_id_MDL(id);
        }
        public IEnumerable<SELECT_notificaciones_valida_hora_MDL_Result> ObtenerValidacionHoraNotificaciones(EntityConnectionStringBuilder connection, int id, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_notificaciones_valida_hora_MDL(id,
                                                                 hora);
        }
        public IEnumerable<SELEC_fol_notificaciones_menos_MDL_Result> ObtenerFolioMenosNotificaciones(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_fol_notificaciones_menos_MDL(id);
        }
        public IEnumerable<SELECT_lista_folios_notificaciones_MDL_Result> ObtenerTodoFolioNotificaciones(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_notificaciones_MDL(folio_sam);
        }
        public IEnumerable<SELECT_cabecera_notificaciones_crea_list_MDL_Result> ObtenerListaFolios(EntityConnectionStringBuilder connection, string fecha, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_notificaciones_crea_list_MDL(fecha,
                                                                        hora);
        }
        public IEnumerable<SELECT_cabecera_notificaciones_crea_Folios_MDL_Result> ObtenerCabFol(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_notificaciones_crea_Folios_MDL(folio_sam);
        }
        public IEnumerable<SELECT_posiciones_notificaciones_crea_Folios_MDL_Result> ObtenerPosFol(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_posiciones_notificaciones_crea_Folios_MDL(folio_sam);
        }
        public IEnumerable<SELECT_cabecera_notificaciones_crea_MDL_Result> ObtenerCabNotificacionesCrea(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_cabecera_notificaciones_crea_MDL();
        }
        public IEnumerable<SELECT_posiciones_notificaciones_crea_MDL_Result> ObtenerPosNotificacionesCrea(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_posiciones_notificaciones_crea_MDL();
        }
        public void ActualizaCabNotificacionesCrea(EntityConnectionStringBuilder connection, CabNotificacionesCrea cabnot)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_cabecera_notificaciones_crea_MDL(cabnot.FOLIO_SAM,
                                                            cabnot.RECIBIDO);
        }
        public void ActualizaPosNotificacionesCrea(EntityConnectionStringBuilder connection, PosNotificacionesCrea posnot)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_posiciones_notificaciones_crea_MDL(posnot.FOLIO_SAM,
                                                              posnot.RECIBIDO);
        }
    }
}
