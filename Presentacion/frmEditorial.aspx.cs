using ReglaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class frmEditorial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Editorial.CargarGrilla(gvEditoriales);
            ClientScript.RegisterStartupScript(GetType(), "InitializeDatatable", "$(document).ready(function() { $('.tblEditorial').prepend($('<thead></thead>').append($('.tblEditorial').find('tr:first'))).DataTable({language: {url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json'}}); $('.tblEditorial thead tr th').addClass('text-center'); });", true);
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            lblId.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            lblTituloModal.InnerText = "Crear editorial";
            btnCrearEditorial.Visible = true;
            btnActualizarEditorial.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "showModal('#mdlEditoriales');", true);
        }

        protected void btnCrearEditorial_Click(object sender, EventArgs e)
        {
            var editorial = new Editorial();
            int numReg;
            string script;
            editorial.Descripcion = txtDescripcion.Text;
            editorial.Estado = "A";

            numReg = editorial.Insertar();
            if (numReg > 0)
            {
                btnCrearEditorial.Enabled = false;
                txtDescripcion.Text = string.Empty;
                script = "executeAlertify(true, 'Editorial creada exitosamente');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
                Response.AppendHeader("Refresh", "3; URL=frmEditorial.aspx");
            }
            else
            {
                script = "executeAlertify(false, 'No se pudo crear la editorial, consulte con el administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
        }


        protected void btnActualizarEditorial_Click(object sender, EventArgs e)
        {
            var editorial = new Editorial();
            int numReg;
            string script;
            editorial.Descripcion = txtDescripcion.Text;
            editorial.Id = int.Parse(lblId.Text);

            numReg = editorial.Actualizar();
            if (numReg > 0)
            {
                btnActualizarEditorial.Enabled = false;
                txtDescripcion.Text = string.Empty;
                lblId.Text = string.Empty;
                script = "executeAlertify(true, 'Editorial actualizada exitosamente');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
                Response.AppendHeader("Refresh", "3; URL=frmEditorial.aspx");
            }
            else
            {
                script = "executeAlertify(false, 'No se pudo actualizar la editorial, consulte con el administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
        }

        protected void btnEliminarEditorial_Click(object sender, EventArgs e)
        {
            var editorial = new Editorial();
            int numReg;
            string script;
            editorial.Id = int.Parse(lblIdEliminar.Text);
            numReg = editorial.Eliminar();

            if (numReg > 0)
            {
                Response.AppendHeader("Refresh", "1; URL=frmEditorial.aspx");
            }
            else
            {
                script = "executeAlertify(false, 'No se pudo borrar la editorial, consulte con el administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            lblIdEliminar.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "closeModal('#mdlEliminarEditorial');", true);
        }

        protected void btnCloseModal_Click(object sender, EventArgs e)
        {
            lblId.Text = string.Empty;
            lblTituloModal.InnerText = string.Empty;
            txtDescripcion.Text = string.Empty;
            btnCrearEditorial.Visible = false;
            btnActualizarEditorial.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "closeModal('#mdlEditoriales');", true);
        }

        protected void gvEditoriales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "OpenEditModal")
            {
                string strId, strDescripcion;

                int rowIndex = Convert.ToInt32(e.CommandArgument); // Obtener el índice de la fila

                GridViewRow row = gvEditoriales.Rows[rowIndex]; // Obtener la fila correspondiente

                // Obtener los datos de la fila
                strId = row.Cells[0].Text;
                strDescripcion = row.Cells[1].Text;
                lblId.Text = strId;
                txtDescripcion.Text = strDescripcion;
                lblTituloModal.InnerText = "Actualizar editorial";
                btnCrearEditorial.Visible = false;
                btnActualizarEditorial.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "showModal('#mdlEditoriales');", true);
            }

            if (e.CommandName == "OpenModal")
            {
                string id = e.CommandArgument.ToString();
                lblIdEliminar.Text = id;
                ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "showModal('#mdlEliminarEditorial');", true);
            }
        }
    }
}