using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Avisos
    {
        #region instancia
        private static DALC_Avisos instance = null;
        private static readonly object padlock = new object();

        public static DALC_Avisos obtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Avisos();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarAvisos(EntityConnectionStringBuilder connection, Avisos av)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_avisos_MDL(av.QMNUM,
                                      av.IWERK);
        }
        public void IngresarAvisos(EntityConnectionStringBuilder connection, Avisos av)
        {
            var context = new samEntities(connection.ToString());
            context.avisos_MDL(av.QMNUM,
                               av.QMART,
                               av.QMTXT,
                               av.TXT04_ES,
                               av.TXT04_EN,
                               av.TXT30_ES,
                               av.TXT30_EN,
                               av.TPLNR,
                               av.PLTXT,
                               av.EQUNR,
                               av.EQKTX,
                               av.BAUTL,
                               av.INGRP,
                               av.IWERK,
                               av.INNAM,
                               av.ARBPL,
                               av.SWERK,
                               av.KTEXT,
                               av.I_PARNR,
                               av.NAME_LIST,
                               av.PARNR_VERA,
                               av.NAME_VERA,
                               av.QMNAM,
                               av.QMDAT,
                               av.MZEIT,
                               av.QMGRP,
                               av.HEADKTXT,
                               av.FOLIO_SAM,
                               av.LVORM,
                               av.AUFNR);
        }
        public void VaciarActividad(EntityConnectionStringBuilder connection, Actividad ac)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_avisos_actividades_MDL(ac.QMNUM);
        }
        public void IngresarActividad(EntityConnectionStringBuilder connection, Actividad ac)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_avisos_actividades_MDL(ac.QMNUM,
                                                  ac.FENUM,
                                                  ac.MNGRP,
                                                  ac.MNCOD,
                                                  ac.KURZTEXT_ES,
                                                  ac.KURZTEXT_EN,
                                                  ac.MATXT,
                                                  ac.MNGFA,
                                                  ac.PSTER,
                                                  ac.PSTUR,
                                                  ac.PETER,
                                                  ac.PETUR,
                                                  ac.MANUM,
                                                  ac.FOLIO_SAM,
                                                  "");
        }
        public void VaciarTextos(EntityConnectionStringBuilder connection, Textos tx)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_textos_avisos_MDL(tx.QMNUM);
        }
        public void IngresarTextos(EntityConnectionStringBuilder connection, Textos tx)
        {
            var context = new samEntities(connection.ToString());
            context.textos_avisos_MDL(tx.QMNUM,
                                      tx.TEXTO,
                                      tx.FOIO_SAM);
        }
        public void VaciarDMS(EntityConnectionStringBuilder connection, DMS dm)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_dms_avisos_MDL(dm.QMNUM);
        }
        public void IngresarDMS(EntityConnectionStringBuilder connection, DMS dm)
        {
            var context = new samEntities(connection.ToString());
            context.dms_avisos_MDL(dm.QMNUM,
                                   dm.DOKAR,
                                   dm.DOKNR,
                                   dm.LATEST_REL,
                                   dm.LATEST_VERSION,
                                   dm.ICON_STATUS, dm.DOKTL,
                                   dm.DOKVR,
                                   dm.DKTXT);
        }
    }
}
