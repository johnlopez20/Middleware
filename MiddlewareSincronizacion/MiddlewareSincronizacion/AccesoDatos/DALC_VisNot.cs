using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_VisNot
    {
        #region Instancia
        private static DALC_VisNot instance = null;
        private static readonly object padlock = new object();

        public static DALC_VisNot ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_VisNot();
                }
                return instance;
            }
        }

        #endregion
        public void IngresaNOTIFICACIONES(EntityConnectionStringBuilder connection, NOTIFICACIONES not)
        {
            var context = new samEntities(connection.ToString());
            context.notificaciones_cabecera_vis_MDL(not.AUFNR,
                                                    not.WERKS,
                                                    not.CABECERA,
                                                    not.VORNR,
                                                    not.UVORN,
                                                    not.KAPAR,
                                                    not.RMZHL,
                                                    not.AUERU,
                                                    not.STOKZ,
                                                    not.BUDAT,
                                                    not.ARBPL,
                                                    not.ISMNW_2,
                                                    not.ISMNU,
                                                    not.LEARR,
                                                    not.LTXA1,
                                                    not.SATZA,
                                                    not.ISDD,
                                                    not.IEDD,
                                                    not.OFMNW,
                                                    not.ARBEI,
                                                    not.FSAVD,
                                                    not.SSAVD,
                                                    not.FSEDD,
                                                    not.SSEDD,
                                                    not.ARBID,
                                                    not.LVORM);
        }
        public void VaciarNOTIFICACIONES(EntityConnectionStringBuilder connection, NOTIFICACIONES not)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_notificaciones_cabecera_vis_MDL(not.AUFNR,
                                                           not.VORNR,
                                                           not.WERKS);
        }
    }
}
