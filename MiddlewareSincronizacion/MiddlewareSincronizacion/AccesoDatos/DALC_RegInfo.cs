using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_RegInfo
    {
        #region Instancia
        private static DALC_RegInfo instance = null;
        private static readonly object padlock = new object();

        public static DALC_RegInfo obtenerInstacia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_RegInfo();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarRegistro(EntityConnectionStringBuilder connection, RegInfo ri)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_registroinfo_MDL(ri.INFNR,
                                            ri.WERKS);
        }
        public void IngresaRegistro(EntityConnectionStringBuilder connection, RegInfo re)
        {
            var context = new samEntities(connection.ToString());
            context.registroinfo_MDL(re.INFNR,
                                     re.EKORG,
                                     re.ESOKZ,
                                     re.WERKS,
                                     re.MATNR,
                                     re.UMREN,
                                     re.UMREZ,
                                     re.LMEIN,
                                     re.MEINS,
                                     re.MATKL,
                                     re.EKGRP,
                                     re.APLFZ,
                                     re.NORBM,
                                     re.UNTTO,
                                     re.UEBTO,
                                     re.BSTMA,
                                     re.MTXNO,
                                     re.KZABS,
                                     re.MWSKZ,
                                     re.NETPR,
                                     re.WAERS,
                                     re.PEINH,
                                     re.BPRME,
                                     re.NAME1,
                                     re.LIFNR,
                                     re.IDNLF,
                                     re.LVORM);
        }
    }
}
