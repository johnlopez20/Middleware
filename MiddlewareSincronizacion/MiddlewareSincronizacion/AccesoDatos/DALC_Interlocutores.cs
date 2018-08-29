using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddlewareSincronizacion.Entidades;
using System.Data.Entity.Core.EntityClient;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Interlocutores
    {
        #region Instancia
        private static DALC_Interlocutores instance = null;
        private static readonly object padlock = new object();

        public static DALC_Interlocutores ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Interlocutores();
                }
                return instance;
            }
        }
        #endregion
        public void InsertarInterlocutores(EntityConnectionStringBuilder connection, Interlocutores i)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_interlocutores_MDL(i.KUNNR,
                                              i.VKORG,
                                              i.VTWEG,
                                              i.SPART,
                                              i.PARVW,
                                              i.PARZA,
                                              i.KUNN2,
                                              i.LIFNR,
                                              i.PERNR,
                                              i.PARNR,
                                              i.KNREF,
                                              i.DEFPA);
        }
        public void InsertarSociedades(EntityConnectionStringBuilder connection, Sociedades s)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_sociedades_MDL(s.BUKRS,
                                          s.BUTXT_ES,
                                          s.BUTXT_EN,
                                          s.ORT01,
                                          s.WAERS);
        }
        public void InsertarOrgVentas(EntityConnectionStringBuilder connection, Org_ventas o)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_org_ventas_MDL(o.VKORG,
                                          o.VTEXT);
        }
        public void VaciarTablasInterlocutores(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.Truncate_Interlocutores_MDL();
        }
    }
}
