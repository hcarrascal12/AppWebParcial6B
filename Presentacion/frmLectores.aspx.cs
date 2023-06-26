using ReglaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class frmLectores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Lector.CargarGrilla(gvLectores);
            //Required for jQuery DataTables to work.
           /* gvLectores.UseAccessibleHeader = true;
            gvLectores.HeaderRow.TableSection = TableRowSection.TableHeader;*/
            /*ClientScript.RegisterStartupScript(GetType(), "InitializeDatatable", "$(document).ready(function() { $('#" + gvLectores.ClientID + "').DataTable(); });", true);*/
            Page.DataBind();
            ClientScript.RegisterStartupScript(GetType(), "InitializeDatatable", "$(document).ready(function() { $('.tblLectores').prepend($('<thead></thead>').append($('.tblLectores').find('tr:first'))).DataTable({language: {url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json'}});; });", true);

        }

        protected void btnCrearLector_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmCrudLector.aspx?op=C");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            string strId;
            LinkButton btnConsultar = (LinkButton)sender;
            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;
            strId = selectedRow.Cells[0].Text;
            Response.Redirect("frmCrudLector.aspx?id=" + strId + "&op=U");
        }

        protected void gvLectores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "OpenModal")
            {
                string id = e.CommandArgument.ToString();
                lblID.Text = id;
                ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "showModal('#mdlEliminarLector');", true);
            }
        }

        protected void btnEliminarLector_Click(object sender, EventArgs e)
        {
            var lector = new Lector();
            int numReg;
            lector.Id = int.Parse(lblID.Text);
            numReg = lector.Eliminar();
            if (numReg > 0)
            {
                Response.AppendHeader("Refresh", "1; URL=frmLectores.aspx");
            }
            else
            {
                string script = "executeAlertify(false, 'No se pudo borrar al lector, consulte con el administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            lblID.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "closeModal('#mdlEliminarLector');", true); 
        }
    }
}