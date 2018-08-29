using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_StatusNotificaciones
    {
        #region Instancia
        private static DALC_StatusNotificaciones instance = null;
        private static readonly object padlock = new object();

        public static DALC_StatusNotificaciones ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_StatusNotificaciones();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELECT_statusnot_datos_id_MDL_Result> ObtenerDatosIdEstatus(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_statusnot_datos_id_MDL(id);
        }
        public IEnumerable<SELECT_statusnot_valida_hora_MDL_Result> ObtenerValidacionHoraEstatus(EntityConnectionStringBuilder connection, int id, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_statusnot_valida_hora_MDL(id,
                                                            hora);
        }
        public IEnumerable<SELEC_fol_estatusnot_menos_MDL_Result> ObtenerFolioMenosEstatus(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_fol_estatusnot_menos_MDL(id);
        }
        public IEnumerable<SELECT_lista_folios_statusnot_MDL_Result> ObtenerTodoFolioEstatus(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_statusnot_MDL(folio_sam);
        }
        public IEnumerable<SELECT_status_notificaciones_li_MDL_Result> ObtenerLista(EntityConnectionStringBuilder connection, string fecha, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_status_notificaciones_li_MDL(fecha,
                                                                 hora);
        }
        //public IEnumerable<SELECT_status_notificaciones_list_MDL_Result> ObtenerListaStatus(EntityConnectionStringBuilder connection, string fecha, string hora)
        //{
        //    var context = new samEntities(connection.ToString());
        //    return context.SELECT_status_notificaciones_list_MDL(fecha,
        //                                                         hora);
        //}
        public IEnumerable<SELECT_status_notificaciones_Folio_MDL_Result> ObtenerEstatusNotFol(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_status_notificaciones_Folio_MDL(folio_sam);
        }
        public IEnumerable<SELECT_status_notificaciones_MDL_Result> ObtenerStatusNotificaciones(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_status_notificaciones_MDL();
        }
        public void ActualizaStatusNotificaciones(EntityConnectionStringBuilder connection, StatusNotificaciones stanot)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_status_notificaciones_MDL(stanot.FOLIO_SAM,
                                                     stanot.RECIBIDO);
        }
    }
}
