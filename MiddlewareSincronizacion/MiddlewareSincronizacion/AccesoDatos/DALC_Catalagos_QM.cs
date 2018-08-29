using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Catalagos_QM
    {
        #region Instancia
        private static DALC_Catalagos_QM instance = null;
        private static readonly object padlock = new object();

        public static DALC_Catalagos_QM ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Catalagos_QM();
                }
                return instance;
            }
        }
        #endregion
        public void IngresaInforme(EntityConnectionStringBuilder connection, Catalogos_PM cat)
        {
            var context = new samEntities(connection.ToString());
            context.InsertInformesErrores_MDL(cat.I_FEART,
                                              cat.I_KURZTEXT_ES,
                                              cat.I_KURZTEXT_EN);
        }
        public void VaciarInforme(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            //context.TruncateInformesErrores_MDL();
            context.TruncateInformesErrores_MDL1();
        }

        public void IngresaTextosCodigos(EntityConnectionStringBuilder connection, Catalogos_PM ct)
        {
            var context = new samEntities(connection.ToString());
            context.InsertTextosCodigos_MDL(ct.T_CODEGRUPPE,
                                              ct.T_CODE,
                                              ct.T_KURZTEXT_ES,
                                              ct.T_KURZTEXT_EN);
        }

        public void VaciarTextosCodigos(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TruncateTextosCodigos_MDL1();
            //context.TruncateTextosCodigos_MDL();
        }


        public void IngresaClasesDefecto(EntityConnectionStringBuilder connection, Catalogos_PM cla)
        {
            var context = new samEntities(connection.ToString());
            context.InsertClasesDefecto_MDL(cla.C_FEHLKLASSE,
                                            cla.C_KURZTEXT_ES,
                                            cla.C_KURZTEXT_EN);
        }


        public void VaciarClasesDefecto(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TruncateClasesDefecto_MDL1();
            //context.TruncateClasesDefecto_MDL();
        }

        public void IngresaInspeccionCodigo(EntityConnectionStringBuilder connection, Catalogos_PM ins)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_tInspeccionCodigos_MDL(ins.AUSWAHLMGE,
                                                ins.IN_CODEGRUPPE,
                                                ins.IN_CODE,
                                                ins.IN_KURZTEXT_ES,
                                                ins.IN_KURZTEXT_EN,
                                                ins.BEWERTUNG);
        }
        public void VaciarInspeccionCodigo(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TruncateInspeccionCodigos_MDL1();
            //context.TruncateInspeccionCodigos_MDL();
        }
        public void IngresaClaseInforme(EntityConnectionStringBuilder connection, ClaseInforme cla)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_clase_informe_MDL(cla.FEART,
                                             cla.KURZTEXT_ES,
                                             cla.KURZTEXT_EN,
                                             cla.RBNR,
                                             cla.RBNRX_ES,
                                             cla.RBNRX_EN,
                                             cla.BWART,
                                             cla.BTEXT_ES,
                                             cla.BTEXT_EN);
        }
        public void VaciarClaseInforme(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_clase_informe_MDL();
        }
        public void IngresaCodigoDefecto(EntityConnectionStringBuilder connection, CodigosDefecto code)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_codigos_defecto_MDL(code.KATALOGART,
                                               code.CODEGRUPPE,
                                               code.CODE,
                                               code.KURZTEXT);
        }
        public void VaciarCodigoDefecto(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_codigos_defecto_MDL();
        }
    }
}
