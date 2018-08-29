using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_CaracteristicasQM
    {
        #region Instancia
        private static DALC_CaracteristicasQM instance = null;
        private static readonly object padlock = new object();

        public static DALC_CaracteristicasQM obtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_CaracteristicasQM();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarTablasCaracteristicasQM(EntityConnectionStringBuilder connection, CaracterisitcasQM cr)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_caracteristicas_planes_inspeccion_material_MDL(cr.MATNR,
                                                                          cr.WERKS);
        }
        public void IngresarCaracteristicasQM(EntityConnectionStringBuilder connection, CaracterisitcasQM cr)
        {
            var context = new samEntities(connection.ToString());
            context.caracteristicas_planes_inspeccion_material_MDL(cr.MATNR,
                                                                   cr.WERKS,
                                                                   cr.PLNTY,
                                                                   cr.PLNNR,
                                                                   cr.PLNAL,
                                                                   cr.VORNR,
                                                                   cr.QPPKTABS,
                                                                   cr.KURZTEXT,
                                                                   cr.LTEXTSPR,
                                                                   cr.STICHPRVER,
                                                                   cr.PROBEMGEH,
                                                                   cr.PRUEFEINH,
                                                                   cr.KATAB1,
                                                                   cr.KATALGART1,
                                                                   cr.AUSWMENGE1,
                                                                   cr.AUSWMGWRK1,
                                                                   cr.STEUERKZ,
                                                                   cr.RENGLON,
                                                                   cr.MERKNR,
                                                                   cr.VERWMERKM,
                                                                   cr.CHAR_TYPE,
                                                                   cr.PRUEFUMF,
                                                                   cr.KTX01,
                                                                   cr.QPMK_ZAEHL,
                                                                   cr.MASSEINHSW);
        }
        public void VaciarDatosInspeccion(EntityConnectionStringBuilder connection, DatosInspeccion din)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_datos_inspeccion_MDL(din.MATNR,
                                                din.WERKS);
        }
        public void InsertarDatosInspeccion(EntityConnectionStringBuilder connection, DatosInspeccion din)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_datos_inspeccion_MDL(din.MATNR,
                                                din.WERKS,
                                                din.ART,
                                                din.KURZTEXT_ES,
                                                din.KURZTEXT_EN,
                                                din.APA,
                                                din.AKTIV,
                                                din.CHG);
        }
    }
}
