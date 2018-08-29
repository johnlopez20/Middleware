using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_BomEquipo
    {
        #region instancia
        private static DALC_BomEquipo instance = null;
        private static readonly object padlock = new object();

        public static DALC_BomEquipo obtenerInstania()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_BomEquipo();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarBom(EntityConnectionStringBuilder connection, BomEquipo be)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_bom_equipos_MDL(be.EQUNR,
                                             be.STLNR,
                                             be.WERKS);
        }
        public void IngresaBom(EntityConnectionStringBuilder connection, BomEquipo be)
        {
            var context = new samEntities(connection.ToString());
            context.bom_equipos_MDL(be.MANDT,
                                    be.EQUNR,
                                    be.WERKS,
                                    be.STLAN,
                                    be.STLTY,
                                    be.STLNR,
                                    be.STLAL,
                                    be.STKOZ,
                                    be.STLKN,
                                    be.STPOZ,
                                    be.STASZ,
                                    be.DATUV,
                                    be.LKENZ,
                                    be.LOEKZ,
                                    be.ANDAT,
                                    be.ANNAM,
                                    be.IDNRK,
                                    be.PSWRK,
                                    be.POSTP,
                                    be.SPOSN,
                                    be.SORTP,
                                    be.KMPME,
                                    be.KMPMG,
                                    be.FMENG);
        }
    }
}
