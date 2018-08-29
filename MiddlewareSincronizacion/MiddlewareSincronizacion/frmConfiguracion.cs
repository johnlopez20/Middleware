using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiddlewareSincronizacion.Entidades;
using System.Xml;
using SAP.Middleware.Connector;
using System.Net.NetworkInformation;

namespace MiddlewareSincronizacion
{
    public partial class frmConfiguracion : Form
    {
        public bool conexionValida = false;
        public ConfigSAP configSAP;
        private bool probado = false;
        ConexionSAP conexion = new ConexionSAP();
        public frmConfiguracion()
        {
            conexionSAP();
            InitializeComponent();
            this.txtAplicationServer.Text = conexion.ApplicationServer;
            this.txtSystemSAP.Text = conexion.System;
            this.txtSystemNuberSAP.Text = conexion.SystemNumber;
            this.txtClient.Text = conexion.Cliente;
            this.txtUser.Text = conexion.User;
            this.txtPassword.Text = conexion.Password;
            this.txtLanguage.Text = conexion.Language;
            this.txtSAProuter.Text = conexion.SAPRouter;
        }
        public ConexionSAP conexionSAP()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Conexiones.xml");
                XmlNode nodoActual = doc.GetElementsByTagName("Conexiones")[0];
                // string CadenaConexion =tratarCadena(obtenerCadenaConexión());
                conexion.System = nodoActual["System"].InnerText;
                conexion.SystemNumber = nodoActual["SystemNumber"].InnerText;
                conexion.ApplicationServer = nodoActual["AppServer"].InnerText;
                conexion.Cliente = nodoActual["Client"].InnerText;
                conexion.User = nodoActual["User"].InnerText;
                conexion.Password = nodoActual["Password"].InnerText;
                conexion.Language = nodoActual["Language"].InnerText;
                conexion.SAPRouter = nodoActual["SAPRouter"].InnerText;
            }
            catch (Exception)
            {
            }
            return conexion;
        }
        public void GuardarDatosSAP(ConexionSAP con)
        {
            XmlDocument xml = new XmlDocument();
            //string nodon = xml.GetElementsByTagName("Conexiones")[0].Attributes["n"].InnerText;            
            try
            {
                xml.Load("Conexiones.xml");
                foreach (XmlNode nodoC in xml.GetElementsByTagName("Conexiones")[0].ChildNodes)
                {

                    if (nodoC.Name == "System")
                    {
                        nodoC.InnerText = con.System;
                    }
                    if (nodoC.Name == "SystemNumber")
                    {
                        nodoC.InnerText = con.SystemNumber;
                    }
                    if (nodoC.Name == "AppServer")
                    {
                        nodoC.InnerText = con.ApplicationServer;
                    }
                    if (nodoC.Name == "Client")
                    {
                        nodoC.InnerText = con.Cliente;
                    }
                    if (nodoC.Name == "User")
                    {
                        nodoC.InnerText = con.User;
                    }
                    if (nodoC.Name == "Password")
                    {
                        nodoC.InnerText = con.Password;
                    }
                    if (nodoC.Name == "Language")
                    {
                        nodoC.InnerText = con.Language;
                    }
                    if (nodoC.Name == "SAPRouter")
                    {
                        nodoC.InnerText = con.SAPRouter;
                    }
                }
            }
            catch (Exception) { }
            xml.Save("Conexiones.xml");
        }
        private void btnGuardarConfig_Click(object sender, EventArgs e)
        {
            if (probado)
            {
                ConexionSAP con = new ConexionSAP();
                con.System = txtSystemSAP.Text;
                con.SystemNumber = txtSystemNuberSAP.Text;
                con.ApplicationServer = txtAplicationServer.Text;
                con.Cliente = txtClient.Text;
                con.User = txtUser.Text;
                con.Password = txtPassword.Text;
                con.Language = txtLanguage.Text;
                con.SAPRouter = txtSAProuter.Text;
                GuardarDatosSAP(con);
                DialogResult = DialogResult.OK;
            }
            else
            {
                probarConexion();
                if (conexionValida)
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    DialogResult = DialogResult.Abort;
                }
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            probarConexion();
        }

        public bool TestCon(ConfigSAP configSAP)
        {
            try
            {
                RfcConfigParameters parametros = new RfcConfigParameters();
                parametros.Add(RfcConfigParameters.SAPRouter, configSAP.SAProuter as string);
                parametros.Add(RfcConfigParameters.Client, configSAP.client as string);
                parametros.Add(RfcConfigParameters.Language, configSAP.language as string);
                parametros.Add(RfcConfigParameters.User, configSAP.user as string);
                parametros.Add(RfcConfigParameters.Password, configSAP.password as string);
                parametros.Add(RfcConfigParameters.AppServerHost, configSAP.applicationServer as string);
                parametros.Add(RfcConfigParameters.SystemNumber, configSAP.systemNumberSAP as string);
                parametros.Add(RfcConfigParameters.SystemID, configSAP.systemSAP as string);
                parametros.Add(RfcConfigParameters.Name, configSAP.systemSAP as string);
                try
                {
                    RfcDestination rfcDest = RfcDestinationManager.GetDestination(parametros);
                }
                catch (Exception) { }

               RfcDestination rfcDesti = null;
                rfcDesti = RfcDestinationManager.GetDestination(configSAP.systemSAP as string);
                RfcRepository repo = rfcDesti.Repository;
                IRfcFunction FUNCION = repo.CreateFunction("ZMATERIALES");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void probarConexion()
        {
            ConfigSAP configSAP = ObtenerDatosConfig();
            if (TestCon(configSAP))
            {
                this.conexionValida = true;
                this.configSAP = configSAP;
                MessageBox.Show("Conexión valida", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnGuardarConfig.Enabled = true;
            }
            else
            {
                conexionValida = false;
                btnGuardarConfig.Enabled = false;
                MessageBox.Show("No se pudo establecer la conexión. Verifique datos", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            probado = true;
        }
        public bool probarCon()
        {
            ConfigSAP configSAP = ObtenerDatosConfig();
            if (TestCon(configSAP))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Conexion Erronea!, verifique los datos...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public bool probarCon2()
        {
            //Ping Pings = new Ping();
            //int timeout = 10;

            //if (Pings.Send("192.168.100.44", timeout).Status == IPStatus.Success)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            //try
            //{
            //    System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.google.com");
            //    return true;
            //}
            //catch (Exception) { return false; }
            ConfigSAP configSAP = ObtenerDatosConfig();
            if (TestCon(configSAP))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ConfigSAP ObtenerDatosConfig()
        {
            ConfigSAP configSAP = new ConfigSAP();
            XmlDocument doc = new XmlDocument();
            doc.Load("Conexiones.xml");
            XmlNode nodoActual = doc.GetElementsByTagName("Conexiones")[0];
            configSAP.systemSAP = nodoActual["System"].InnerText;
            configSAP.systemNumberSAP = nodoActual["SystemNumber"].InnerText;
            configSAP.applicationServer = nodoActual["AppServer"].InnerText;
            configSAP.client = nodoActual["Client"].InnerText;
            configSAP.user = nodoActual["User"].InnerText;
            configSAP.password = nodoActual["Password"].InnerText;
            configSAP.language = nodoActual["Language"].InnerText;
            configSAP.SAProuter = nodoActual["SAPRouter"].InnerText;
            return configSAP;
        }

        private void frmConfiguracion_Shown(object sender, EventArgs e)
        {
            probado = false;
        }

        private void txtSystemSAP_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSystemNuberSAP_TextChanged(object sender, EventArgs e)
        {

        }

        private void lklClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}
