using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Reservas
    {
        #region Instancia
        private static DALC_Reservas instance = null;
        private static readonly object padlock = new object();

        public static DALC_Reservas ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Reservas();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELEC_datos_reserva_id_MDL_Result> ObtenerDatosIdReserva(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_datos_reserva_id_MDL(id);
        }
        public IEnumerable<SELECT_reservas_valida_hora_MDL_Result> ObtenerValidacionHoraReserva(EntityConnectionStringBuilder connection, int id, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_reservas_valida_hora_MDL(id,
                                                         hora);
        }
        public IEnumerable<SELEC_fol_reserva_menos_MDL_Result> ObtenerFolioMenosReserva(EntityConnectionStringBuilder connection, int id)
        {
            var context = new samEntities(connection.ToString());
            return context.SELEC_fol_reserva_menos_MDL(id);
        }
        public IEnumerable<SELECT_lista_folios_reservas_MDL_Result> ObtenerTodoFolioReserva(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_lista_folios_reservas_MDL(folio_sam);
        }
        public IEnumerable<SELECT_reserva_cabecera_crea_list_MDL_Result> ObtenerFoliosLista(EntityConnectionStringBuilder connection, string fecha, string hora)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_reserva_cabecera_crea_list_MDL(fecha,
                                                                 hora);
        }
        public IEnumerable<SELECT_reserva_cabecera_crea_Fol_MDL_Result> ObtenerReservaCab(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_reserva_cabecera_crea_Fol_MDL(folio_sam);
        }
        public IEnumerable<SELECT_reserva_posiciones_crea_Fol_MDL_Result> ObtenerReservaPos(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_reserva_posiciones_crea_Fol_MDL(folio_sam);
        }
        public IEnumerable<SELECT_reserva_cabecera_crea_MDL_Result> ObtenerReservasCab(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_reserva_cabecera_crea_MDL();
        }
        public IEnumerable<SELECT_reserva_posiciones_crea_MDL_Result> ObtenerReservasPos(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_reserva_posiciones_crea_MDL();
        }
        public void ActualizaReservaCab(EntityConnectionStringBuilder connection, ReservaCab recab)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_reserva_cabecera_crea_MDL(recab.FOLIO_SAM,
                                                     recab.RECIBIDO);
        }
        public void ActualizaReservaPos(EntityConnectionStringBuilder connection, ReservaPos repos)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_reserva_posiciones_crea_MDL(repos.FOLIO_SAM,
                                                       repos.RECIBIDO);
        }
        public void ActualizarResultado(EntityConnectionStringBuilder conncetion, string folio_sam, string reserva, string mensaje)
        {
            var context = new samEntities(conncetion.ToString());
            context.UPDATE_Reservas_MM_MDL(folio_sam,
                                           reserva,
                                           mensaje);
        }
        public void ElimanarRegistroCreacion(EntityConnectionStringBuilder connection, string folio_sam)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_Reservas_FOL_MM_MDL(folio_sam);
        }
    }
}
