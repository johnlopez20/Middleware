using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_RepoSolpe
    {
        #region Instacia
        private static DALC_RepoSolpe instance = null;
        private static readonly object padlock = new object();

        public static DALC_RepoSolpe ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_RepoSolpe();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarRepoSolpe(EntityConnectionStringBuilder connection, RepoSolpe reps)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_repo_solped_MDL(reps.FOLIO_SAM,
                                           //reps.BNFPO,
                                           reps.WERKS);
        }
        public void IngresarSolpe(EntityConnectionStringBuilder connection, RepoSolpe sol)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_repo_solped_MDL(sol.FOLIO_SAM,
                                           sol.BNFPO,
                                           sol.BANFN,
                                           sol.BADAT,
                                           sol.PSTYP,
                                           sol.KNTTP,
                                           sol.MATNR,
                                           sol.TXZ01,
                                           sol.MENGE,
                                           sol.MEINS,
                                           sol.WERKS,
                                           sol.LGORT,
                                           sol.AFNAM,
                                           sol.FRGDT,
                                           sol.FRGKZ,
                                           sol.FRGCT,
                                           sol.PROCESADO,
                                           sol.ERROR,
                                           sol.EBELN,
                                           sol.UDATE,
                                           sol.FRGKE);
        }
    }
}
