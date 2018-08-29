using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_sincronizacion
    {
        #region Instancia
        private static DALC_sincronizacion instance = null;
        private static readonly object padlock = new object();

        public static DALC_sincronizacion ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_sincronizacion();
                }
                return instance;
            }
        }
        #endregion
        string hora = DateTime.Now.ToString("hh:mm:ss");

        public void vaciaSicronizacion(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_sincronizacion_MDL();
        }

        public void IngresaSincronizacion(EntityConnectionStringBuilder connection, Sincronizacion ss)
        {
            var context = new samEntities(connection.ToString());
            context.sincronizacion_MDL(ss.VKORG,
                                       ss.HY_INTERFAZ,
                                       ss.NAME,
                                       ss.SECUENCIA,
                                       ss.ACTIVA,
                                       ss.BBP_TABL,
                                       ss.PERIODO,
                                       ss.SELEC,
                                       ss.SINCRONIZACION,
                                       ss.CARGA_INICIAL);
        }
        public void EventoMeddleware(EntityConnectionStringBuilder connection, string evento)
        {
            var context = new samEntities(connection.ToString());
            DateTime fecha = DateTime.Today;
            hora = DateTime.Now.ToString("hh:mm:ss");
            context.INSERT_eventos_meddleware_MDL(evento,
                                                  fecha.ToString(),
                                                  hora);
        }
        public void InsertarErrorMDL(EntityConnectionStringBuilder connection, string rfc, string error)
        {
            var context = new samEntities(connection.ToString());
            DateTime fecha = DateTime.Today;
            hora = DateTime.Now.ToString("hh:mm:ss");
            context.reporte_rfc_status_MDL(rfc,
                                           fecha,
                                           hora,
                                           error);
        }
        public void IndicadorConsumoRFC(EntityConnectionStringBuilder connection, string rfc, string indicador, string centro)
        {
            var context = new samEntities(connection.ToString());
            context.UPDATE_sincronizacion_MDL(rfc,
                                              centro,
                                              indicador);
        }
        public void VaciarTablaTiempos(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_tiempos_MDL();
        }
        public void IngresarTiempos(EntityConnectionStringBuilder connection, Tiempo tim)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_tiempos_MDL(tim.WERKS,
                                       tim.BBP_TABL,
                                       tim.PERIODO);
        }
        public IEnumerable<SELECT_sincronizacion_inventarios_MDL_Result> ObtenerRfc(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_sincronizacion_inventarios_MDL();
        }
        /*****************************************PP*********************************************/
        public void vaciaImpresoras(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.impresoras_trunca_mdl();
        }
        public void IngresaImpresora(EntityConnectionStringBuilder connection, string pt, string ip, string tp)
        {
            var context = new samEntities(connection.ToString());
            context.impresora_insert_mdl(pt, ip, tp);
        }
    }
}