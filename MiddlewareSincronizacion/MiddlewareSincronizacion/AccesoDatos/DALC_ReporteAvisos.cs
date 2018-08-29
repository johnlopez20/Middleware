using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ReporteAvisos
    {
        #region Instancia
        private static DALC_ReporteAvisos instance = null;
        private static readonly object padlock = new object();

        public static DALC_ReporteAvisos ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ReporteAvisos();
                }
                return instance;
            }
        }

        #endregion
        public void VaciarReporteAviso(EntityConnectionStringBuilder connection, ReporteAviso um)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_reporte_avisos_MDL(um.FOLIO_SAM,
                                              um.IWERK);
        }
        public void IngresarReporteAviso(EntityConnectionStringBuilder connection, ReporteAviso av)
        {
            var context = new samEntities(connection.ToString());
            context.reporte_avisos_MDL(av.FOLIO_SAM,
                                       av.UZEIT,
                                       av.DATUM,
                                       av.IWERK,
                                       av.QMART,
                                       av.QMTXT,
                                       av.EQUNR,
                                       av.QMGRP,
                                       av.QMCOD,
                                       av.FOLIO_SAP,
                                       av.RECIBIDO,
                                       av.PROCESADO,
                                       av.ERROR);
        }
        public void ActualizaReporteAviso(EntityConnectionStringBuilder connection, ReporteAviso um)
        {
            var context = new samEntities(connection.ToString());
            if(um.FOLIO_SAP.Equals(""))
            {
                context.UPDATE_reporte_avisos_reportes_MDL(um.FOLIO_SAM,
                                                           um.PROCESADO,
                                                           um.ERROR);
            }
            else
            {
                context.DELETE_reporte_avisos_reporte_MDL(um.FOLIO_SAM);
            }
        }
    }
}
