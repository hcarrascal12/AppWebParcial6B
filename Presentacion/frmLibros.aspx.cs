using ReglaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class frmLibros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Libro.CargarGrilla(gvLibros);
            ClientScript.RegisterStartupScript(GetType(), "InitializeDatatable", "$(document).ready(function() { $('.tblLibros').prepend($('<thead></thead>').append($('.tblLibros').find('tr:first'))).DataTable({language: {url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json'}}); $('.tblLibros thead tr th').addClass('text-center'); });", true);
        }

        protected void btnCrearLibro_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmCrudLibro.aspx?op=C");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            string strId;
            LinkButton btnConsultar = (LinkButton)sender;
            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;
            strId = selectedRow.Cells[0].Text;
            Response.Redirect("frmCrudLibro.aspx?id=" + strId + "&op=U");
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            lblID.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "closeModal('#mdlEliminarLibro');", true);
        }

        protected void btnEliminarLibro_Click(object sender, EventArgs e)
        {
            var libro = new Libro();
            int numReg;
            libro.Id = int.Parse(lblID.Text);
            numReg = libro.Eliminar();
            if (numReg > 0)
            {
                Response.AppendHeader("Refresh", "1; URL=frmLibros.aspx");
            }
            else
            {
                string script = "executeAlertify(false, 'No se pudo borrar al libro, consulte con el administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
        }

        protected void gvLibros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "OpenModal")
            {
                string id = e.CommandArgument.ToString();
                lblID.Text = id;
                ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "showModal('#mdlEliminarLibro');", true);
            }
        }

        protected void gvLibros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Obtener el valor del campo 'Estado'
                string valor = DataBinder.Eval(e.Row.DataItem, "disponible").ToString();
                HtmlGenericControl span = new HtmlGenericControl("span");

                // Comprobar si el valor cumple con la condición deseada
                if (valor.ToUpper() == "SI")
                {
                    // Modificar el estilo de la celda correspondiente
                    span.Attributes["class"] = "bg-success text-white p-2 rounded";
                }
                else
                {
                    span.Attributes["class"] = "bg-danger text-white p-2 rounded";
                }

                span.InnerText = valor;

                e.Row.Cells[7].Controls.Add(span);

                TableCell cell = e.Row.Cells[7]; // Índice de la columna

                // Aplicar el estilo CSS para centrar el contenido
                cell.Style["text-align"] = "center";

            }

        }
    }
}