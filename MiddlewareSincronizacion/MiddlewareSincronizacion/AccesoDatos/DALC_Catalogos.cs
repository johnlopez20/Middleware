using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Catalogos
    {
        #region instancia
        private static DALC_Catalogos instance = null;
        private static readonly object padlock = new object();

        public static DALC_Catalogos obtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Catalogos();
                }
                return instance;
            }
        }
        #endregion
        public void IngresaUtilizacionL(EntityConnectionStringBuilder connection, UtilizacionL ul)
        {
            var context = new samEntities(connection.ToString());
            context.InsertUtilizacionListamaterial_MDL(ul.STLAN,
                                                       ul.PMPFE,
                                                       ul.PMPKO,
                                                       ul.PMPKA,
                                                       ul.PMPRV,
                                                       ul.PMPVS,
                                                       ul.PMPIN,
                                                       ul.PMPER,
                                                       ul.ANTXT_ES,
                                                       ul.ANTXT_EN);
        }
        public void VaciarUtilizacionL(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TruncateUtilizacionListamaterial_MDL();

        }
        public void IngresaUtilizacion(EntityConnectionStringBuilder connection, UtilizacionHR uh)
        {

            var context = new samEntities(connection.ToString());
            context.InsertUtilizacionHojaruta_MDL(uh.PLNST,
                                                  uh.TXTH_ES,
                                                  uh.TXTH_EN);
        }
        public void VaciarUtilizacion(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TruncateUtilizacionHojaruta_MDL();

        }
        public void IngresaMedicion(EntityConnectionStringBuilder connection, Medicion me)
        {
            var context = new samEntities(connection.ToString());
            context.InsertMedicion_MDL(me.MDOCM,
                                       me.POINT,
                                       me.IDATE,
                                       me.ITIME,
                                       me.INVTS,
                                       me.CNTRG,
                                       me.MDTXT_ES,
                                       me.MDTXT_EN,
                                       me.KZLTX,
                                       me.READR,
                                       me.ERDAT,
                                       me.ERUHR,
                                       me.ERNAM,
                                       me.AEDAT,
                                       me.AENAM,
                                       me.LVORM,
                                       me.GENER,
                                       me.PRUEFLOS,
                                       me.VORGLFNR,
                                       me.MERKNR,
                                       me.DETAILERG,
                                       me.ROOTD,
                                       me.TOLTY,
                                       me.TOLID,
                                       me.WOOBJ,
                                       me.DOCAF,
                                       me.READG,
                                       me.READGI,
                                       me.RECDV,
                                       me.RECDVI,
                                       me.RECDU,
                                       me.CNTRR,
                                       me.CNTRRI,
                                       me.CDIFF,
                                       me.CDIFFI,
                                       me.IDIFF,
                                       me.EXCHG,
                                       me.TOTEX,
                                       me.CODCT,
                                       me.CODGR,
                                       me.VLCOD,
                                       me.CVERS,
                                       me.PREST,
                                       me.CANCL,
                                       me.WOOB1,
                                       me.PROBENR,
                                       me.MBEWERTG,
                                       me.INTVL,
                                       me.IDAT1,
                                       me.ITIM1,
                                       me.TMSTP_BW);
        }
        public void VaciarMedicion(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TruncateMedicion_MDL();

        }
        public void IngresaMantenimiento(EntityConnectionStringBuilder connection, Mantenimiento mm)
        {
            var context = new samEntities(connection.ToString());
            context.InsertMantenimiento_MDL(mm.WARPL,
                                            mm.NUMMER,
                                            mm.OPERATOR,
                                            mm.ZYKL1,
                                            mm.ZEIEH,
                                            mm.PAKTEXT_ES,
                                            mm.PAKTEXT_EN,
                                            mm.LANGU,
                                            mm.POINT,
                                            mm.OFFSET,
                                            mm.INAKTIV,
                                            mm.NZAEH,
                                            mm.ZAEHL,
                                            mm.KTEXTYZK_ES,
                                            mm.KTEXTYZK_EN,
                                            mm.CYCLESEQIND,
                                            mm.SETREPEATIND);

        }
        public void VaciarMantenimiento(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TruncateMantenimiento_MDL();

        }
        public void IngresaGrupoCodigos(EntityConnectionStringBuilder connection, GrupoCodigos gc)
        {
            var context = new samEntities(connection.ToString());
            context.InsertGrupoCodigos_MDL(gc.KATALOGART,
                                           gc.CODEGRUPPE,
                                           gc.KURZTEXT_ES,
                                           gc.KURZTEXT_EN,
                                           gc.CODE,
                                           gc.KURZTEXT_E,
                                           gc.KURZTEXT_N);
        }
        public void VaciarGrupoCodigos(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TruncateGrupoCodigos_MDL();

        }
    }
}
