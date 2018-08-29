using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_ReservasMateriales
    {
        #region instancia
        private static DALC_ReservasMateriales instance = null;
        private static readonly object padlock = new object();

        public static DALC_ReservasMateriales ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_ReservasMateriales();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarReservasMateriales(EntityConnectionStringBuilder connection, string centro)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_reservas_materiales_MDL(centro);
        }
        public void IngresaReservasMateriales(EntityConnectionStringBuilder connection, ReservasMateriales rm)
        {
            var context = new samEntities(connection.ToString());
            context.reservas_materiales_MDL(rm.RSNUM,
                                            rm.RSPOS,
                                            rm.XLOEK,
                                            rm.XWAOK,
                                            rm.KZEAR,
                                            rm.MATNR,
                                            rm.WERKS,
                                            rm.LGORT,
                                            rm.CHARG,
                                            rm.SOBKZ,
                                            rm.BDTER,
                                            rm.BDMNG,
                                            rm.MEINS,
                                            rm.SHKZG,
                                            rm.FMENG,
                                            rm.ENMNG,
                                            rm.ENWRT,
                                            rm.WAERS,
                                            rm.ERFMG,
                                            rm.ERFME,
                                            rm.KOSTL,
                                            rm.AUFNR,
                                            rm.BWART,
                                            rm.SAKNR,
                                            rm.UMWRK,
                                            rm.UMLGO,
                                            rm.SGTXT,
                                            rm.LMENG,
                                            rm.STLTY,
                                            rm.STLNR,
                                            rm.POTX1,
                                            rm.POTX2,
                                            rm.UMREZ,
                                            rm.UMREN,
                                            rm.AUFPL,
                                            rm.PLNFL,
                                            rm.VORNR,
                                            rm.PEINH,
                                            rm.AFPOS,
                                            rm.MATKL,
                                            rm.LIFNR,
                                            rm.FOLIO_SAM,
                                            rm.LVORM,
                                            rm.UNAME);
        }
    }
}
