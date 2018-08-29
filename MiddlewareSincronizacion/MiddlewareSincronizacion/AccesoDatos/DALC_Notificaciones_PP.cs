using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Notificaciones_PP
    {
        #region Instancia
        private static DALC_Notificaciones_PP instance = null;
        private static readonly object padlock = new object();

        public static DALC_Notificaciones_PP ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Notificaciones_PP();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELEC_datos_Noti_PP_id_MDL_Result> ObtenerDatosIdNoti(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_datos_Noti_PP_id_MDL(id);
        }
        public IEnumerable<SELECT_Noti_PP_valida_hora_MDL_Result> ObtenerValidacionHoraNoti(EntityConnectionStringBuilder connection, int id, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_Noti_PP_valida_hora_MDL(id,
                                                          hora);
        }
        public IEnumerable<SELECT_lista_folios_Noti_PP_MDL_Result> ObtenerTodoFolioNoti(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_Noti_PP_MDL(folio_sam);
        }
    }
}
