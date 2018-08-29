using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddlewareSincronizacion.Entidades;
using System.Data.Entity.Core.EntityClient;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Usuarios
    {
        #region Instacia
        private static DALC_Usuarios instance = null;
        private static readonly object padlock = new object();

        public static DALC_Usuarios ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Usuarios();
                }
                return instance;
            }
        }
        #endregion
        public IEnumerable<SELECT_usuario_MDL_Result> BuscarUsuario(EntityConnectionStringBuilder connection, Usuarios us)
        {
            var context = new samEntities(connection.ToString());
            return context.SELECT_usuario_MDL(us.PERNR);
        }
        public void InsertarUsuario(EntityConnectionStringBuilder connection, Usuarios us)
        {
            var context = new samEntities(connection.ToString());
            int habilitado = 0;
            if (us.ZSAM_AC.Equals("X"))
            {
                habilitado = 1;
            }
            else
            {
                habilitado = 0;
            }
            context.INSERT_usuario_MDL(us.PERNR,
                                        "63f10d08837efc789bdf65edb02f440bdb6a35c3",
                                        us.VORNA,
                                        us.NACHN,
                                        us.NACH2,
                                        us.STRAS,
                                        us.ORT01,
                                        us.ORT02,
                                        us.TELNR,
                                        us.NUM01,
                                        us.EMAIL,
                                        habilitado,
                                        us.PERMISOS,
                                        us.WERKS,
                                        us.ENDDA,
                                        us.BEGDA,
                                        us.PSTLZ,
                                        us.LAND1,
                                        us.USER_SAP,
                                        us.LGORT,
                                        us.FIELD86,
                                        us.FIELD87);
        }
        public void ActualizarUsuario(EntityConnectionStringBuilder connection, Usuarios us)
        {
            var context = new samEntities(connection.ToString());
            int habilitado = 0;
            if (us.ZSAM_AC.Equals("X"))
            {
                habilitado = 1;
            }
            else
            {
                habilitado = 0;
            }
            context.UPDATE_usuario_MDL(us.PERNR,
                                        us.VORNA,
                                        us.NACHN,
                                        us.NACH2,
                                        us.STRAS,
                                        us.ORT01,
                                        us.ORT02,
                                        us.TELNR,
                                        us.NUM01,
                                        us.EMAIL,
                                        habilitado,
                                        us.PERMISOS,
                                        us.WERKS,
                                        us.LGORT,
                                        us.FIELD86,
                                        us.FIELD87);
        }
        public void VaciarGrupoCC(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_grupo_centro_costos_MDL();
        }
        public void VaciarGrupoMateriales(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_grupo_materiales_MDL();
        }
        public void VaciarGrupoMateriales(EntityConnectionStringBuilder connection, UsuarioGrupoMateriales usgm)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_UGM_MDL(usgm.PERNR,
                                   usgm.ZSAM_GU);
            //context.DELETE_usuario_grupo_materiales_MDL(usgm.PERNR);
        }
        public void IngresarGrupoMateriales(EntityConnectionStringBuilder connection, UsuarioGrupoMateriales usgm)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_usuario_grupo_materiales_MDL(usgm.PERNR,
                                                        usgm.ZSAM_GU);
        }
        public void VaciarUsuarioGrupoCentroCoste(EntityConnectionStringBuilder connection, UsuarioGrupoCentroCoste ugcc)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_UGCC_MDL(ugcc.PERNR,
                                    ugcc.ZSAM_GK);
            //context.DELETE_usuario_grupo_centro_costo_MDL(ugcc.PERNR);
        }
        public void IngresarUsuarioGrupoCentroCoste(EntityConnectionStringBuilder connection, UsuarioGrupoCentroCoste ugcc)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_usuario_grupo_centro_costo_MDL(ugcc.PERNR,
                                                          ugcc.ZSAM_GK);
        }
        public void IngresaCatalogoGrupoCentroCostos(EntityConnectionStringBuilder connection, CatalogoGrupoCentroCostos cgcc)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_grupo_centro_costos_MDL(cgcc.ZSAM_GK,
                                                   cgcc.DESCRIPCION);
        }
        public void IngresaCatalogoGrupoMateriales(EntityConnectionStringBuilder connection, CatalogoGrupoMateriales cgm)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_grupo_materiales_MDL(cgm.ZSAM_GU,
                                                cgm.DESCRIPCION);
        }
        public void VaciarListaMaterialesGrupoMaterial(EntityConnectionStringBuilder connection, ListaMaterialGrupoMaterial lmgm)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_MGM_MDL(lmgm.MTART,
                                   lmgm.MATNR,
                                   lmgm.ZSAM_GU);
            //context.DELETE_materiales_grupo_materiales_MDL(lmgm.MATNR);
        }
        public void IngresaListaMaterialGrupoMateerial(EntityConnectionStringBuilder connection, ListaMaterialGrupoMaterial lmgm)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_materiales_grupo_materiales_MDL(lmgm.MTART,
                                                           lmgm.ZSAM_GU,
                                                           lmgm.MATNR);
        }
        public void VaciarListaCentroCostoGrupoCosto(EntityConnectionStringBuilder connection, ListaCentroCostoGrupoCosto lccgc)
        {
            var context = new samEntities(connection.ToString());
            context.DELETE_CCGCC_MDL(lccgc.ZSAM_GK,
                                     lccgc.KOSTL,
                                     lccgc.BUKRS,
                                     lccgc.KSTAR);
            //context.DELETE_centro_costo_grupo_centro_costo_MDL(lccgc.ZSAM_GK);
        }
        public void IngresaListaCentroCostoGrupoCosto(EntityConnectionStringBuilder connection, ListaCentroCostoGrupoCosto lccgc)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_centro_costo_grupo_centro_costo_MDL(lccgc.ZSAM_GK,
                                                               lccgc.KOSTL,
                                                               lccgc.BUKRS,
                                                               lccgc.KSTAR);
        }
    }
}
