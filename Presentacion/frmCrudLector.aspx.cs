using ReglaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class frmCrudLector : System.Web.UI.Page
    {
        public static int sID = -1;
        public static string sOpc = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string vSql;
            var lector = new Lector();
            ListItem valorSeleccionado;
            List<string> datos;

            if (!Page.IsPostBack)
            {

                if (Request.QueryString["id"] != null)
                {
                    sID = int.Parse(Request.QueryString["id"]);
                    datos = lector.Consultar(sID);
                    txtNIde.Text = datos[0];
                    txtNombre.Text = datos[1];
                    txtApellido.Text = datos[2];
                    txtEmail.Text = datos[3];
                    txtTel.Text = datos[4];
                }

                if (Request.QueryString["op"] != null)
                {
                    sOpc = Request.QueryString["op"].ToString();
                    switch (sOpc)
                    {
                        case "C":
                            lblTitulo.Text = "Crear lector";
                            btnGuardar.Visible = true;
                            break;
                        case "U":
                            lblTitulo.Text = "Actualizar lector";
                            btnActualizar.Visible = true;
                            break;
                    }
                }
                else
                {
                    Response.Redirect("frmLectores.aspx");
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
                        strTitulo = "Crear lector";
                        break;
                    case "U":
                        strTitulo = "Actualizar lector";
                        break;
                }
            }
            return strTitulo;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var lector = new Lector();
            int numReg;
            lector.N_ide = txtNIde.Text;
            lector.Nombre = txtNombre.Text;
            lector.Apellido = txtApellido.Text;
            lector.Email = txtEmail.Text;
            lector.Tel = txtTel.Text;
            lector.Estado = "A";

            numReg = lector.Insertar();

            if (numReg > 0)
            {
                Limpiar();
                btnGuardar.Enabled = false;
                Response.AppendHeader("Refresh", "3; URL=frmLectores.aspx");
                string script = "executeAlertify(true, 'Se ha creado al lector exitosamente');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
            else
            {
                string script = "executeAlertify(false, 'No se pudo crear el lector, consulte con el administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
        }

        protected void Limpiar()
        {
            txtNIde.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTel.Text = string.Empty;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmLectores.aspx");
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            var lector = new Lector();
            int numReg;
            bool existe;
            string script;
            lector.Id = sID;
            lector.N_ide = txtNIde.Text;
            lector.Nombre = txtNombre.Text;
            lector.Apellido = txtApellido.Text;
            lector.Email = txtEmail.Text;
            lector.Tel = txtTel.Text;

            existe = lector.ConsultarIntegridad(lector.Id, lector.N_ide);

            if (existe)
            {
                script = "executeAlertify(false, 'Ya existe un lector con este nit, por favor verifíque');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);

            }
            else
            {
                numReg = lector.Actualizar();

                if (numReg > 0)
                {
                    Limpiar();
                    btnActualizar.Enabled = false;
                    Response.AppendHeader("Refresh", "3; URL=frmLectores.aspx");
                    script = "executeAlertify(true, 'Se ha actualizado al lector exitosamente');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
                }
                else
                {
                    script = "executeAlertify(false, 'No se pudo actualizar al lector, consulte con el administrador');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
                }
              
            }

            
        }
    }
}