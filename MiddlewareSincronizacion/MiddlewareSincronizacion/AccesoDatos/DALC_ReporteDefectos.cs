using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ReporteDefectos
    {
        #region Instacia
        private static DALC_ReporteDefectos instance = null;
        private static readonly object padlock = new object();

        public static DALC_ReporteDefectos ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ReporteDefectos();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarReporteDefectos(EntityConnectionStringBuilder connection, ReporteDefectos rd)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_reporte_defectos_rep_MDL(rd.FOLIO_SAM,
                                                    rd.WERKS);
        }
        public void InsertarReporteDefecto(EntityConnectionStringBuilder connection, ReporteDefectos rd)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_reporte_defectos_MDL(rd.FOLIO_SAM,
                                                rd.PRUEFLOS,
                                                rd.WERKS,
                                                rd.DATUM,
                                                rd.MATNR,
                                                rd.UZEIT,
                                                rd.EBELN,
                                                rd.FOLIO_MOV,
                                                rd.MBLNR,
                                                rd.FEART,
                                                rd.QKURZTEXT,
                                                rd.RBNR,
                                                rd.RBNRX,
                                                rd.RECIBIDO,
                                                rd.PROCESADO,
                                                rd.MENSAJE,
                                                rd.FECHA_RECIBIDO,
                                                rd.HORA_RECIBIDO,
                                                rd.PRUEFER,
                                                rd.UNAME);
        }
        public void ActulizarDefectos(EntityConnectionStringBuilder connection, ReporteDefectos rd)
        {
            var context = new samEntities(connection.ToString());
            if(rd.PRUEFLOS.Equals(""))
            {
                context.UPDATE_reporte_defectos_MDL(rd.FOLIO_SAM,
                                                    rd.PROCESADO,
                                                    rd.MENSAJE);
            }
            else
            {
                context.DELETE_reporte_defectos_MDL(rd.FOLIO_SAM);
            }
        }
    }
}
