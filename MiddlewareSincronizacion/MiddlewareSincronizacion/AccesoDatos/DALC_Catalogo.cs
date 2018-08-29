using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Catalogo
    {
        #region Instacia
        private static DALC_Catalogo instance = null;
        private static readonly object padlock = new object();

        public static DALC_Catalogo ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Catalogo();
                }
                return instance;
            }
        }
        #endregion
        private String Verifica(string cr)
        {
            if (cr.IndexOf("'") != -1) { return cr.Replace("'", "''"); }
            return cr;
        }
        public void VaciarOrganizacionCompras(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_organizacion_compras_MDL();
        }
        public void IngresaOrganizacionCompras(EntityConnectionStringBuilder connection, OrganizacionCompras oc)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_organizacion_compras_MDL(oc.WERKS,
                                                    oc.EKORG,
                                                    oc.EKOTX_ES,
                                                    oc.EKOTX_EN);
        }
        public void VaciarAlmacenes(EntityConnectionStringBuilder connection, string centro)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_almacenes_v2_MDL(centro);
        }
        public void IngresaAlmacenes(EntityConnectionStringBuilder connection, Almacenes al)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_almacenes_MDL(al.LGORT,
                                         al.LGOBE_ES,
                                         al.LGOBE_EN,
                                         al.WERKS);
        }
        public void VaciarClaveMoneda(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_clave_moneda_MDL();
        }
        public void IngresaClaveMoneda(EntityConnectionStringBuilder connection, ClaveMoneda cm)
        {
            try
            {
                var context = new samEntities(connection.ToString());
                context.INSERT_clave_moneda_MDL(cm.WAERS,
                                                cm.LTEXT_ES,
                                                cm.LTEXT_EN);
            }
            catch (Exception) { }
        }
        public void VaciarInfoTipo(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_info_tipo_MDL();
        }
        public void IngresaTipoInfo(EntityConnectionStringBuilder connection, InfoTipo it)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_info_tipo_MDL(it.VALUED,
                                         it.DDTEXT_ES,
                                         it.DDTEXT_EN);
        }
        public void VaciarGrupoArticulos(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_grupo_articulos_MDL();
        }
        public void IngresaGrupoArticulos(EntityConnectionStringBuilder connection, GrupoArticulos ga)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_grupo_articulos_MDL(ga.MATKL,
                                               Verifica(ga.WGBEZ_ES),
                                               Verifica(ga.WGBEZ_EN),
                                               Verifica(ga.WGBEZ60_ES),
                                               Verifica(ga.WGBEZ60_EN));
        }
        public void VaciarTipoMaterial(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_tipo_material_MDL();
        }
        public void IngresaTipoMaterial(EntityConnectionStringBuilder connection, TipoMaterial tm)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_tipo_material_MDL(tm.MTART,
                                             tm.MTBEZ_ES,
                                             tm.MTBEZ_EN);
        }
        public void VaciarTipoImp(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_tipo_imputacion_MDL();
        }
        public void IngresaTipoImp(EntityConnectionStringBuilder connection, TipoImp ti)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_tipo_imputacion_MDL(ti.KNTTP,
                                               ti.KNTTX_ES,
                                               ti.KNTTX_EN);
        }
        public void VaciarTipoPos(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_tipo_posicion_MDL();
        }
        public void IngresaTipoPos(EntityConnectionStringBuilder connection, TipoPos tp)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_tipo_posicion_MDL(tp.PSTYP,
                                             tp.EPSTP_ES,
                                             tp.PTEXT_ES,
                                             tp.EPSTP_EN,
                                             tp.PTEXT_EN);
        }
        public void VaciarDistribucion(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_distribucion_MDL();
        }
        public void IngresaDistribucion(EntityConnectionStringBuilder connection, Distribucion ds)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_distribucion_MDL(ds.VALUED,
                                            ds.DDTEXT_ES,
                                            ds.DDTEXT_EN);
        }
        public void VaciarTratamiento(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_estatus_tratamiento_MDL();
        }
        public void IngresaTratamiento(EntityConnectionStringBuilder connection, EstatusTratamiento et)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_estatus_tratamiento_MDL(et.VALUED,
                                                   et.DDTEXT_ES,
                                                   et.DDTEXT_EN);
        }
        public void VaciarClaseDoc(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_clase_documento_MDL();
        }
        public void IngresaClaseDoc(EntityConnectionStringBuilder connection, ClaseDoc cl)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_clase_documento_MDL(cl.BSART,
                                               cl.BATXT_ES,
                                               cl.BATXT_EN);
        }
        public void VaciarSector(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_sector_MDL();
        }
        public void IngresaSector(EntityConnectionStringBuilder connection, Sector se)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_sector_MDL(se.SPART,
                                      se.VTEXT_ES,
                                      se.VTEXT_EN);
        }
        public void VaciarCanalDistribucion(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_canal_distribucion_MDL();
        }
        public void IngresaCanalDistribucion(EntityConnectionStringBuilder connection, Canal_Distribucion cd)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_canal_distribucion_MDL(cd.VKORG,
                                                  cd.VTWEG,
                                                  cd.VTEXT_ES,
                                                  cd.VTEXT_EN);
        }
        public void VaciarOrgVentas(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_organizacion_ventas_MDL();
        }
        public void IngresaOrgVenta(EntityConnectionStringBuilder connection, Organizacion_Ventas ov)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_organizacion_ventas_MDL(ov.VKORG,
                                                   ov.VTEXT);
        }
        public void VaciarCentroCosto(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_centrocosto_MDL();
        }
        public void IngresaCentroCosto(EntityConnectionStringBuilder connection, CentroCoste cc)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_centrocosto_MDL(cc.KOKRS,
                                           cc.KOSTL,
                                           cc.KTEXT_ES,
                                           cc.KTEXT_EN);
        }
        public void VaciarCentroBeneficio(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_centro_beneficio_MDL();
        }
        public void IngresaCentroBeneficio(EntityConnectionStringBuilder connection, CentroBeneficio cb)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_centro_beneficio_MDL(cb.BUKRS,
                                                cb.KOKRS,
                                                cb.PRCTR,
                                                cb.KTEXT);
        }
        public void VaciarSociedades(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_sociedades_MDL();
        }
        public void IngresaSociedades(EntityConnectionStringBuilder connection, Sociedades so)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_sociedades_MDL(so.BUKRS,
                                          so.BUTXT_ES,
                                          so.BUTXT_EN,
                                          so.ORT01,
                                          so.WAERS);
        }
        public void VaciarGrupoCompras(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_grupo_compras_MDL();
        }
        public void IngresaGrupoCompras(EntityConnectionStringBuilder connection, GrupoCompras gc)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_grupo_compras_MDL(gc.EKGRP,
                                             gc.EKNAM);
        }
        public void VaciarCuentaMayor(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_cuenta_mayor_MDL();
        }
        public void IngresaCuentaMayor(EntityConnectionStringBuilder connection, CuentaMayor cm)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_cuenta_mayor_MDL(cm.SAKNR,
                                            cm.KTOPL,
                                            cm.TXT50_ES,
                                            cm.TXT50_EN);
        }
    }
}
