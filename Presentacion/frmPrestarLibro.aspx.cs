using ReglaNegocio;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class frmPrestarLibro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                if (ViewState["IdLibro"] != null)
                {
                    lblIdLibro.Text = ViewState["IdLibro"].ToString();
                }

                if (ViewState["nombreLibro"] != null)
                {
                    txtNombreLibro.Text = ViewState["nombreLibro"].ToString();
                }

                if (ViewState["IdLector"] != null)
                {
                    lblIdLector.Text = ViewState["IdLector"].ToString();
                }

                if (ViewState["NombreLector"] != null)
                {
                    txtNombreLector.Text = ViewState["NombreLector"].ToString();
                }

            }
                Prestamo.CargarGrillaLibros(gvLibros);
                Prestamo.CargarGrillaLector(gvLector);
                ClientScript.RegisterStartupScript(GetType(), "InitializeDatatable", "$(document).ready(function() { $('.datepicker').datepicker({dateFormat: 'dd/mm/yy', language: 'es'}); $('.tblLibros').prepend($('<thead></thead>').append($('.tblLibros').find('tr:first'))).DataTable({language: {url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json'}}); $('.tblLector').prepend($('<thead></thead>').append($('.tblLector').find('tr:first'))).DataTable({language: {url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json'}}); });", true);
            
        }

        protected void btnAbrirModalLibro_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "showModal('#mdlLibro');", true);
        }

        protected void btnAbrirModalLector_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "showModal('#mdlLector');", true);
        }

        protected void btnRegistrarPrestamo_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "showModal('#mdlConfirmarPrestamo');", true);
        }

        protected void gvLibros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EscogerLibro")
            {
                string strId,strIsbn, strTitulo;
                Libro libro = new Libro(); 

                int rowIndex = Convert.ToInt32(e.CommandArgument); // Obtener el índice de la fila

                GridViewRow row = gvLibros.Rows[rowIndex]; // Obtener la fila correspondiente

                // Obtener los datos de la fila
                strIsbn = row.Cells[0].Text;
                strTitulo = row.Cells[1].Text;
                strId = libro.TraerID(strIsbn);

                lblIdLibro.Text = strId;
                txtIsbn.Text = strIsbn;
                txtNombreLibro.Text = strTitulo;

                ViewState["nombreLibro"] = strTitulo;
                ViewState["IdLibro"] = strId;


                ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "closeModal('#mdlLibro');", true);
            }
        }

        protected void gvLector_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EscogerLector")
            {
                string strId, strNit, strNombre;
                Lector lector = new Lector();

                int rowIndex = Convert.ToInt32(e.CommandArgument); // Obtener el índice de la fila

                GridViewRow row = gvLector.Rows[rowIndex]; // Obtener la fila correspondiente

                // Obtener los datos de la fila
                strNit = row.Cells[0].Text;
                strNombre = row.Cells[1].Text;
                strId = lector.TraerID(strNit);

                lblIdLector.Text = strId;
                txtNitLector.Text = strNit;
                txtNombreLector.Text = strNombre;

                ViewState["NombreLector"] = strNombre;
                ViewState["IdLector"] = strId;


                ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "closeModal('#mdlLector');", true);
            }
        }

        protected void btnConfirmarPrestamo_Click(object sender, EventArgs e)
        {
            var prestamo = new Prestamo();
            string format = "dd/MM/yyyy", script;
            bool prestado;
            prestamo.IdLector = int.Parse(lblIdLector.Text);
            prestamo.IdLibro = int.Parse(lblIdLibro.Text);
            prestamo.FechaPrestamo= DateTime.Now;
            prestamo.FechaDevolucion = DateTime.ParseExact(txtFechaDevolucion.Text, format, CultureInfo.InvariantCulture);
            prestamo.EstadoPrestamo = "A";
            prestamo.EstadoEntregado = txtObservacion.InnerText;

            prestado = prestamo.PrestarLibro();

            if (prestado)
            {
                Limpiar();
                script = "executeAlertify(true, 'Se ha realizado el préstamo exitosamente');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
                Response.AppendHeader("Refresh", "3; URL=frmPrestarLibro.aspx");
            }
            else
            {
                script = "executeAlertify(false, 'No se pudo prestar el libro, consulte con el administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
        }

        private void Limpiar()
        {
            ViewState["IdLibro"] = null;
            lblIdLibro.Text = string.Empty;

            ViewState["nombreLibro"] = null;
            txtNombreLibro.Text = string.Empty;

            ViewState["IdLector"] = null;
            lblIdLector.Text = string.Empty;

            ViewState["NombreLector"] = null;
            txtNombreLector.Text = string.Empty;

            txtNitLector.Text= string.Empty;
            txtIsbn.Text = string.Empty;

            txtObservacion.InnerText = string.Empty;
            txtFechaDevolucion.Text = string.Empty;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}