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
using System.Data.SqlClient;
using System.Xml;
using MiddlewareSincronizacion.AccesoDatos;
using System.Data.Entity.Core.EntityClient;

namespace MiddlewareSincronizacion
{
    public partial class frmLocalHost : Form
    {
        private bool probado = false;
        ConexionLocalHost conexion = new ConexionLocalHost();

        public frmLocalHost()
        {
            conexionLocal();
            InitializeComponent();
            this.txtServer.Text = conexion.Server;
            this.txtDB.Text = conexion.DataBase;
            this.txtUser.Text = conexion.User;
            this.txtPass.Text = conexion.Password;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (btnGuardar.Enabled)
            {
                MessageBox.Show("Guarde antes de cerrar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.Close();
            }
        }
        public EntityConnectionStringBuilder Connection()
        {
            EntityConnectionStringBuilder connection = new EntityConnectionStringBuilder();
            connection.Provider = "System.Data.SqlClient";
            connection.ProviderConnectionString = "data source = " + txtServer.Text + " ;initial catalog = " + txtDB.Text + " ;persist security info=True;user id = " + txtUser.Text + ";password = " + txtPass.Text + ";MultipleActiveResultSets=True;App=EntityFramework";
            connection.Metadata = "res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl";
            return connection;

        }
        private void btnProbar_Click(object sender, EventArgs e)
        {
            TestConnection();
        }
        private bool TestConnection()
        {
            EntityConnectionStringBuilder connection = new EntityConnectionStringBuilder();
            connection.Provider = "System.Data.SqlClient";
            connection.ProviderConnectionString = "data source = " + txtServer.Text + " ;initial catalog = " + txtDB.Text + " ;persist security info=True;user id = " + txtUser.Text + ";password = " + txtPass.Text + ";MultipleActiveResultSets=True;App=EntityFramework";
            connection.Metadata = "res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl";
            samEntities condb = new samEntities(connection.ToString());
            try
            {
                //var querys = condb.centro_MDL();
                var querys = condb.centros.Select(p => p);
                //var querys = condb.reporte_rfc_status.Select(p => p);
                dataGridView1.DataSource = querys.ToList();
                MessageBox.Show("Conexión Exitosa", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.probado = true;
                btnProbar.Enabled = false;
                btnGuardar.Enabled = true;
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Conexion Erronea BD!, verifique los datos...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); this.probado = false;
                return false;
            }
            finally
            {

            }
        }
        public bool TestCon()
        {

            EntityConnectionStringBuilder connection = new EntityConnectionStringBuilder();
            connection.Provider = "System.Data.SqlClient";
            connection.ProviderConnectionString = "data source = " + txtServer.Text + " ;initial catalog = " + txtDB.Text + " ;persist security info=True;user id = " + txtUser.Text + ";password = " + txtPass.Text + ";MultipleActiveResultSets=True;App=EntityFramework";
            connection.Metadata = "res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl";
            samEntities condb = new samEntities(connection.ToString());
            try
            {
                var querys = condb.centros.Select(p => p);
                dataGridView1.DataSource = querys.ToList();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Conexion Erronea BD!, verifique los datos...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); this.probado = false;
                return false;
            }
            finally
            {

            }
        }
        public String DatosConexion()
        {
            //-------- Windows Authentication -----------
            //return "Server=.\\" + txtServer.Text.ToString()
            //      + ";Database=" + txtDB.Text.ToString()
            //      + ";Integrated Security=true";

            //-------- SQL Server Authentication----------
            //data source = ServidorSQL; initial catalog = BaseDatos; user id = Usuario; password = Contraseña
            return "Data Source=" + txtServer.Text.ToString()
                 + ";Initial Catalog=" + txtDB.Text.ToString()
                 + ";user id = " + txtUser.Text.ToString() + "; password = " + txtPass.Text.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (probado)
            {
                ConexionLocalHost con = new ConexionLocalHost();
                con.Server = txtServer.Text;
                con.DataBase = txtDB.Text;
                con.User = txtUser.Text;
                con.Password = txtPass.Text;
                GuardarDatosXAMPP(con);
                btnGuardar.Enabled = false;
                btnProbar.Enabled = true;
                DialogResult = DialogResult.OK;
            }
        }
        private void GuardarDatosXAMPP(ConexionLocalHost con)
        {
            XmlDocument xml = new XmlDocument();

            try
            {
                xml.Load("Conexiones.xml");
                foreach (XmlNode nodoC in xml.GetElementsByTagName("Conexiones")[0].ChildNodes)
                {
                    if (nodoC.Name == "Server") { nodoC.InnerText = con.Server; }

                    if (nodoC.Name == "Database") { nodoC.InnerText = con.DataBase; }

                    if (nodoC.Name == "UsuarioBD") { nodoC.InnerText = con.User; }

                    if (nodoC.Name == "PasswordBD") { nodoC.InnerText = con.Password; }
                }
            }
            catch (Exception) { MessageBox.Show("Error al tratar de guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            xml.Save("ConexionLocalHost.xml");
        }

        private ConexionLocalHost conexionLocal()
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load("Conexiones.xml");
                XmlNode nodo = xml.GetElementsByTagName("Conexiones")[0];

                conexion.Server = nodo["Server"].InnerText;
                conexion.DataBase = nodo["Database"].InnerText;
                conexion.User = nodo["UsuarioBD"].InnerText;
                conexion.Password = nodo["PasswordBD"].InnerText;
            }
            catch (Exception) { }
            return conexion;
        }
    }
}
