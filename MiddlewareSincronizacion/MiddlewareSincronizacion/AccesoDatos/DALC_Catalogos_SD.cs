using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using MiddlewareSincronizacion.Entidades;

namespace MiddlewareSincronizacion.AccesoDatos
{
    public class DALC_Catalogos_SD
    {
        #region Instacia
        private static DALC_Catalogos_SD instance = null;
        private static readonly object padlock = new object();

        public static DALC_Catalogos_SD ObtenerInstancia()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DALC_Catalogos_SD();
                }
                return instance;
            }
        }
        #endregion
        public void VaciarTablasCatalogo(EntityConnectionStringBuilder connection)
        {
            var context = new samEntities(connection.ToString());
            context.TRUNCATE_catalogos_SD_MDL_v2();
        }
        public void InsertarOrgVentas(EntityConnectionStringBuilder connection, Organizacion_Ventas o)
        {
            try
            {
                var context = new samEntities(connection.ToString());
                context.INSERT_org_ventas_MDL(o.VKORG,
                                                       o.VTEXT);
            }
            catch (Exception) { }
        }
        public void InsertarCanalD(EntityConnectionStringBuilder connection, Canal_Distribucion c)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_canal_distribucion_MDL1(c.VKORG,
                                                  c.VTWEG,
                                                  c.VTEXT_ES,
                                                  c.VTEXT_EN);
            //context.INSERT_canal_distribucion_MDL(c.VKORG,
            //                                      c.VTWEG,
            //                                      c.VTEXT_ES,
            //                                      c.VTEXT_EN);
        }
        public void InsertarSociedades(EntityConnectionStringBuilder connection, Sociedades s)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_sociedades_MDL1(s.BUKRS,
                                          s.BUTXT_ES,
                                          s.BUTXT_EN,
                                          s.ORT01,
                                          s.WAERS);
            //context.INSERT_sociedades_MDL(s.BUKRS,
            //                              s.BUTXT_ES,
            //                              s.BUTXT_EN,
            //                              s.ORT01,
            //                              s.WAERS);
        }
        public void InsertarCentroB(EntityConnectionStringBuilder connection, CentroBeneficio b)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_centro_beneficio_MDL(b.BUKRS,
                                                b.KOKRS,
                                                b.PRCTR,
                                                b.KTEXT);
        }
        public void InsertarCentroC(EntityConnectionStringBuilder connection, CentroCoste cc)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_centrocosto_MDL(cc.KOKRS,
                                            cc.KOSTL,
                                            cc.KTEXT_ES,
                                            cc.KTEXT_EN);
            //context.INSERT_centro_costo_MDL(cc.KOKRS,
            //                                cc.KOSTL,
            //                                cc.KTEXT_ES,
            //                                cc.KTEXT_EN);
        }
        public void InsertarCuentaM(EntityConnectionStringBuilder connection, CuentaMayor m)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_cuenta_mayor_MDL1(m.SAKNR,
                                            m.KTOPL,
                                            m.TXT50_ES,
                                            m.TXT50_EN);
            //context.INSERT_cuenta_mayor_MDL(m.SAKNR,
            //                                m.KTOPL,
            //                                m.TXT50_ES,
            //                                m.TXT50_EN);
        }
        public void InsertarSector(EntityConnectionStringBuilder connection, Sector s)
        {
            try
            {
                var context = new samEntities(connection.ToString());
                context.INSERT_sector_MDL1(s.SPART,
                                          s.VTEXT_ES,
                                          s.VTEXT_EN);
                //context.INSERT_sector_MDL(s.SPART,
                //                          s.VTEXT_ES,
                //                          s.VTEXT_EN);
            }
            catch (Exception) { }
        }
        public void InsertarClaseP(EntityConnectionStringBuilder connection, ClasePedido c)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_clase_pedido_MDL("",
                                            c.AUART,
                                            c.BEZEI);
        }
        public void InsertarOficinaV(EntityConnectionStringBuilder connection, OficinaVentas o)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_oficina_ventas_MDL(o.VKBUR,
                                              o.BEZEI);
        }
        public void InsertarGrupoV(EntityConnectionStringBuilder connection, GrupoVendedores g)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_grupo_vendedores_MDL(g.VKGRP,
                                                g.BEZEI);
        }
        public void InsertarListaP(EntityConnectionStringBuilder connection, ListaPrecios l)
        {
            var context = new samEntities(connection.ToString());
            context.INSERT_lista_precios_MDL(l.PLTYP,
                                             l.PTEXT);
        }
    }
}
