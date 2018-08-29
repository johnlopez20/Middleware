using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ReporteOrden
    {
        #region Instancia
        private static DALC_ReporteOrden instance = null;
        private static readonly object padlock = new object();

        public static DALC_ReporteOrden ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ReporteOrden();
                }
                return instance;
            }
        }

        #endregion
        public void VaciarReporteOrden(EntityConnectionStringBuilder connection, ReporteOrden um)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_reportes_ordenes_MDL(um.FOLIO_SAM,
                                                um.PPLANT);
        }
        public void IngresarReporteOrden(EntityConnectionStringBuilder connection, ReporteOrden av)
        {
            var context = new samEntities(connection.ToString());
            context.reportes_ordenes_MDL(av.FOLIO_SAM,
                                         av.AUFNR,
                                         av.FUNC_LOC,
                                         av.UZEIT,
                                         av.DATUM,
                                         av.TABIX,
                                         av.PPLANT,
                                         av.ORDTYPE,
                                         av.MNWKCTR,
                                         av.SHORTTXT,
                                         av.EQUIP,
                                         av.RECIBIDO,
                                         av.PROCESADO,
                                         av.ERROR);
        }
        public void ActualizaReporteOrden(EntityConnectionStringBuilder connection, ReporteOrden um)
        {
            var context = new samEntities(connection.ToString());
            if(um.AUFNR.Equals(""))
            {
                context.UPDATE_reporte_ordenes_reportes_MDL(um.FOLIO_SAM,
                                                            um.PROCESADO,
                                                            um.ERROR);
            }
            else
            {
                context.DELETE_reporte_ordenes_reporte_MDL(um.FOLIO_SAM);
            }
        }
    }
}
