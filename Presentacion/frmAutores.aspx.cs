using ReglaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class frmAutores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Autor.CargarGrilla(gvAutores);
            ClientScript.RegisterStartupScript(GetType(), "InitializeDatatable", "$(document).ready(function() { $('.tblAutores').prepend($('<thead></thead>').append($('.tblAutores').find('tr:first'))).DataTable({language: {url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json'}});; });", true);
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            lblId.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            lblTituloModal.InnerText = "Crear autor";
            btnCrearAutor.Visible = true;
            btnActualizarAutor.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "showModal('#mdlAutores');", true);
        }

        protected void btnCrearAutor_Click(object sender, EventArgs e)
        {
            var autor = new Autor();
            int numReg;
            string script;
            autor.Descripcion = txtDescripcion.Text;
            autor.Estado = "A";

            numReg = autor.Insertar();
            if (numReg > 0)
            {
                btnCrearAutor.Enabled = false;
                txtDescripcion.Text = string.Empty;
                script = "executeAlertify(true, 'Autor creado exitosamente');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
                Response.AppendHeader("Refresh", "3; URL=frmAutores.aspx");
            }
            else
            {
                script = "executeAlertify(false, 'No se pudo crear al autor, consulte con el administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
        }


        protected void btnActualizarAutor_Click(object sender, EventArgs e)
        {
            var autor = new Autor();
            int numReg;
            string script;
            autor.Descripcion = txtDescripcion.Text;
            autor.Id = int.Parse(lblId.Text);

            numReg = autor.Actualizar();
            if (numReg > 0)
            {
                btnActualizarAutor.Enabled = false;
                txtDescripcion.Text = string.Empty;
                lblId.Text = string.Empty;
                script = "executeAlertify(true, 'Autor actualizado exitosamente');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
                Response.AppendHeader("Refresh", "3; URL=frmAutores.aspx");
            }
            else
            {
                script = "executeAlertify(false, 'No se pudo actualizar al autor, consulte con el administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
        }

        protected void btnEliminarAutor_Click(object sender, EventArgs e)
        {
            var autor = new Autor();
            int numReg;
            string script;
            autor.Id = int.Parse(lblIdEliminar.Text);
            numReg = autor.Eliminar();

            if (numReg > 0)
            {
                Response.AppendHeader("Refresh", "1; URL=frmAutores.aspx");
            }
            else
            {
                script = "executeAlertify(false, 'No se pudo borrar al autor, consulte con el administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            lblIdEliminar.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "closeModal('#mdlEliminarAutor');", true);
        }

        protected void btnCloseModal_Click(object sender, EventArgs e)
        {
            lblId.Text = string.Empty;
            lblTituloModal.InnerText = string.Empty;
            txtDescripcion.Text = string.Empty;
            btnCrearAutor.Visible = false;
            btnActualizarAutor.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "closeModal('#mdlAutores');", true);
        }

        protected void gvAutores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "OpenEditModal")
            {
                string strId, strDescripcion;

                int rowIndex = Convert.ToInt32(e.CommandArgument); // Obtener el índice de la fila

                GridViewRow row = gvAutores.Rows[rowIndex]; // Obtener la fila correspondiente

                // Obtener los datos de la fila
                strId = row.Cells[0].Text;
                strDescripcion = row.Cells[1].Text;
                lblId.Text = strId;
                txtDescripcion.Text = strDescripcion;
                lblTituloModal.InnerText = "Actualizar autor";
                btnCrearAutor.Visible = false;
                btnActualizarAutor.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "showModal('#mdlAutores');", true);
            }

            if (e.CommandName == "OpenModal")
            {
                string id = e.CommandArgument.ToString();
                lblIdEliminar.Text = id;
                ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "showModal('#mdlEliminarAutor');", true);
            }
        }
    }
}