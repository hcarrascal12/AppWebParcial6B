using ReglaNegocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class frmPrestamos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cmbEstadoPrestamo.Items.Add(new ListItem("Seleccionar", "S"));
                cmbEstadoPrestamo.Items.Add(new ListItem("Prestado", "A"));
                cmbEstadoPrestamo.Items.Add(new ListItem("Devuelto", "I"));
                cmbEstadoPrestamo.SelectedValue = "S";
                Prestamo.CargarComboLector(cmbLectores);
            }
            Prestamo.CargarGrillaPrestamos(gvPrestamos);
            ClientScript.RegisterStartupScript(GetType(), "InitializeDatatable", "$(document).ready(function() { $('.tblPrestamos').prepend($('<thead></thead>').append($('.tblPrestamos').find('tr:first'))).DataTable({searching: false, language: {url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json'}}); $('.tblPrestamos thead tr th').addClass('text-center'); });", true);

        }

        protected void gvPrestamos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string id;
            int rowIndex;
            if (e.CommandName == "OpenModal")
            {
                rowIndex = Convert.ToInt32(e.CommandArgument); // Obtener el índice de la fila

                GridViewRow row = gvPrestamos.Rows[rowIndex]; // Obtener la fila correspondiente

                // Obtener los datos de la fila
                id = row.Cells[0].Text;
                lblIdPrestamo.Text = id;

                ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "showModal('#mdlConfirmarDevolucion');", true);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string strEstadoPrestamo = cmbEstadoPrestamo.SelectedValue.ToString();
            int intIdLector = int.Parse(cmbLectores.SelectedValue.ToString());
            var prestamo = new Prestamo();
            prestamo.IdLector = intIdLector;
            prestamo.EstadoPrestamo = strEstadoPrestamo;
            prestamo.CargarGrillaPrestamosConsulta(gvPrestamos);

        }

        private void Limpiar()
        {
            lblIdPrestamo.Text = string.Empty;
            txtObservacion.InnerText = string.Empty;
        }

        protected void gvPrestamos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Obtener el valor del campo 'Estado'
                string estado = DataBinder.Eval(e.Row.DataItem, "EstadoPrestamo").ToString();
                HtmlGenericControl span = new HtmlGenericControl("span");

                // Encontrar el botón dentro de la fila
                LinkButton btnOpenDevolucion = (LinkButton)e.Row.FindControl("btnAbrirModalDevolucion");

                // Verificar el valor del campo y ocultar o mostrar el botón
                if (estado.Equals("Prestado", StringComparison.InvariantCultureIgnoreCase))
                {
                    btnOpenDevolucion.Visible = true;
                    span.Attributes["class"] = "bg-success text-white p-2 rounded";
                }
                else
                {
                    btnOpenDevolucion.Visible = false;
                    span.Attributes["class"] = "bg-warning text-white p-2 rounded";
                }

                span.InnerText = estado;
                e.Row.Cells[1].Controls.Add(span);
                e.Row.Cells[1].Attributes["class"] = "text-center";
            }
        }

        protected void btnConfirmarDevolucion_Click(object sender, EventArgs e)
        {
            bool devuelto;
            var prestamo = new Prestamo();
            string script;

            prestamo.Id = int.Parse(lblIdPrestamo.Text);
            prestamo.EstadoRecibido = txtObservacion.InnerText;
            prestamo.FechaConfirmacionDevolucion = DateTime.Now;
            prestamo.EstadoPrestamo = "I";

            devuelto = prestamo.DevolverLibro();

            if (devuelto)
            {
                Limpiar();
                script = "executeAlertify(true, 'Se ha devuelto el libro exitosamente');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
                Response.AppendHeader("Refresh", "3; URL=frmPrestamos.aspx");
            }
            else
            {
                script = "executeAlertify(false, 'No se pudo devolver el libro, consulte con el administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
        }
    }
}