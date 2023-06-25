using ReglaNegocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class frmCrudUsuario : System.Web.UI.Page
    {
        public static int sID = -1;
        public static string sOpc = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string vSql;
            var usuario = new Usuario();
            ListItem valorSeleccionado;
            List<string> datos;

            if (!Page.IsPostBack)
            {

                if (Request.QueryString["id"] != null)
                {
                    cmbTipoUsuario.Items.Clear();
                    vSql = "SELECT [Id], [Nombre] FROM tipo_usuario";
                    Usuario.CargarCombo(cmbTipoUsuario, vSql);
                    sID = int.Parse(Request.QueryString["id"]);
                    datos = usuario.Consultar(sID);
                    txtUsuario.Text = datos[0];
                    txtNombre.Text = datos[1];
                    txtApellido.Text = datos[2];
                    txtEmail.Text = datos[3];
                    cmbTipoUsuario.SelectedValue = datos[5];
                    valorSeleccionado = cmbTipoUsuario.Items.FindByValue(cmbTipoUsuario.SelectedValue);
                    if (valorSeleccionado != null)
                    {
                        string script = "asignarDropdownList('ContentPlaceHolder1_cmbTipoUsuario', '" + datos[5] + "');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
                    }
                }

                if (Request.QueryString["op"] != null)
                {
                    sOpc = Request.QueryString["op"].ToString();
                    switch (sOpc)
                    {
                        case "C":
                            lblTitulo.Text = "Crear Usuario";
                            btnGuardar.Visible = true;
                            break;
                        case "U":
                            lblTitulo.Text = "Actualizar Usuario";
                            btnActualizar.Visible = true;
                            break;
                    }
                }
                else
                {
                    Response.Redirect("frmUsuarios.aspx");
                }
            }
                Page.DataBind();
            
            
        }

        protected string GetTitulo()
        {
            string strTitulo = string.Empty;
            if (Request.QueryString["op"] != null)
            {
                sOpc = Request.QueryString["op"].ToString();
                switch (sOpc)
                {
                    case "C":
                        strTitulo = "Crear Usuario";
                        break;
                    case "U":
                        strTitulo = "Actualizar Usuario";
                        break;
                }
            }
            return strTitulo;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmUsuarios.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var usu = new Usuario();
            int numReg;
            usu.Nombre = txtNombre.Text;
            usu.Apellido = txtApellido.Text;
            usu.Email = txtEmail.Text;
            usu.Estado = "A";
            usu.IdTipoUsuario = int.Parse(cmbTipoUsuario.SelectedValue);
            usu.Username = txtUsuario.Text;
            byte[] salt = Usuario.GenerateSalt();
            var password  = txtPassword.Text;
            string hashedPassword = Usuario.HashPassword(password, salt);
            usu.Password = hashedPassword;

            numReg = usu.Insertar();

            if (numReg > 0)
            {
                Limpiar();
                btnGuardar.Enabled = false;
                Response.AppendHeader("Refresh", "3; URL=frmUsuarios.aspx");
                string script = "executeAlertify(true, 'Se ha creado al usuario exitosamente');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
            else
            {
              string script = "executeAlertify(false, 'No se pudo crear el usuario, consulte con el administrador');";
              ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }

        }

        protected void Limpiar()
        {
            txtUsuario.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtVerification.Text = string.Empty;
            cmbTipoUsuario.SelectedIndex = 0;
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            var usu = new Usuario();
            string password, hashedPassword;
            byte[] salt;
            int numReg;
            usu.Id = sID;
            usu.Nombre = txtNombre.Text;
            usu.Apellido = txtApellido.Text;
            usu.Email = txtEmail.Text;
            usu.Estado = "A";
            usu.IdTipoUsuario = int.Parse(cmbTipoUsuario.SelectedValue);
            usu.Username = txtUsuario.Text;
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                salt = Usuario.GenerateSalt();
                password = txtPassword.Text;
                hashedPassword = Usuario.HashPassword(password, salt);
                usu.Password = hashedPassword;
            }
           
            numReg = usu.Actualizar();

            if (numReg > 0)
            {
                Limpiar();
                btnActualizar.Enabled = false;
                Response.AppendHeader("Refresh", "3; URL=frmUsuarios.aspx");
                string script = "executeAlertify(true, 'Se ha actualizado al usuario exitosamente');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
            else
            {
                string script = "executeAlertify(false, 'No se pudo actualizar el usuario, consulte con el administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
        }
    }
}