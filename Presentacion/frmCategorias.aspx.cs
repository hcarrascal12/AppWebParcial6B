using ReglaNegocio;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class frmCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Categoria.CargarGrilla(gvCategorias);
            ClientScript.RegisterStartupScript(GetType(), "InitializeDatatable", "$(document).ready(function() { $('.tblCategorias').prepend($('<thead></thead>').append($('.tblCategorias').find('tr:first'))).DataTable({language: {url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json'}});; });", true);
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            lblId.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            lblTituloModal.InnerText = "Crear categoría";
            btnCrearCategoria.Visible = true;
            btnActualizarCategoria.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "showModal('#mdlCategoria');", true);
        }

        protected void btnCrearCategoria_Click(object sender, EventArgs e)
        {
            var categoria = new Categoria();
            int numReg;
            string script;
            categoria.Descripcion = txtDescripcion.Text;
            categoria.Estado = "A";

            numReg = categoria.Insertar();
            if (numReg > 0)
            {
                btnCrearCategoria.Enabled = false;
                txtDescripcion.Text = string.Empty;
                script = "executeAlertify(true, 'Categoría creada exitosamente');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
                Response.AppendHeader("Refresh", "3; URL=frmCategorias.aspx");
            }
            else
            {
                script = "executeAlertify(false, 'No se pudo crear la categoria, consulte con el administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
        }

        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "OpenEditModal")
            {
                string strId, strDescripcion;

                int rowIndex = Convert.ToInt32(e.CommandArgument); // Obtener el índice de la fila

                GridViewRow row = gvCategorias.Rows[rowIndex]; // Obtener la fila correspondiente

                // Obtener los datos de la fila
                strId = row.Cells[0].Text;
                strDescripcion = row.Cells[1].Text; 
                lblId.Text = strId;
                txtDescripcion.Text = strDescripcion;
                lblTituloModal.InnerText = "Actualizar categoría";
                btnCrearCategoria.Visible = false;
                btnActualizarCategoria.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "showModal('#mdlCategoria');", true);
            }

            if (e.CommandName == "OpenModal")
            {
                string id = e.CommandArgument.ToString();
                lblIdEliminar.Text = id;
                ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "showModal('#mdlEliminarCategoria');", true);
            }
        }

        protected void btnActualizarCategoria_Click(object sender, EventArgs e)
        {
            var categoria = new Categoria();
            int numReg;
            string script;
            categoria.Descripcion = txtDescripcion.Text;
            categoria.Id = int.Parse(lblId.Text);

            numReg = categoria.Actualizar();
            if (numReg > 0)
            {
                btnActualizarCategoria.Enabled = false;
                txtDescripcion.Text = string.Empty;
                lblId.Text = string.Empty;
                script = "executeAlertify(true, 'Categoría actualizada exitosamente');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
                Response.AppendHeader("Refresh", "3; URL=frmCategorias.aspx");
            }
            else
            {
                script = "executeAlertify(false, 'No se pudo actualizar la categoria, consulte con el administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
        }

        protected void btnEliminarCategoria_Click(object sender, EventArgs e)
        {
            var categoria = new Categoria();
            int numReg;
            string script;
            categoria.Id = int.Parse(lblIdEliminar.Text);
            numReg= categoria.Eliminar();

            if (numReg > 0)
            {
                Response.AppendHeader("Refresh", "1; URL=frmCategorias.aspx");
            }
            else
            {
                script = "executeAlertify(false, 'No se pudo borrar la categoría, consulte con el administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            lblIdEliminar.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "closeModal('#mdlEliminarCategoria');", true);
        }

        protected void btnCloseModal_Click(object sender, EventArgs e)
        {
            lblId.Text = string.Empty;
            lblTituloModal.InnerText = string.Empty;
            txtDescripcion.Text = string.Empty;
            btnCrearCategoria.Visible = false;
            btnActualizarCategoria.Visible=false;
            ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "closeModal('#mdlCategoria');", true);
        }

    }
}