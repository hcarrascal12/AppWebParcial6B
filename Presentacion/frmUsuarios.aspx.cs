using ReglaNegocio;
using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class frmUsuarios : System.Web.UI.Page
    {
        private int intId;
        protected void Page_Load(object sender, EventArgs e)
        {

            Usuario.CargarGrillaUsuarios(gvUsuarios);
            ClientScript.RegisterStartupScript(GetType(), "InitializeDatatable", "$(document).ready(function() { $('.tblUsuarios').prepend($('<thead></thead>').append($('.tblUsuarios').find('tr:first'))).DataTable({language: {url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json'}});; });", true);
            Page.DataBind();
        }


        protected void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmCrudUsuario.aspx?op=C");
        }


        protected void btnEditar_Click(object sender, EventArgs e)
        {
            string strId;
            LinkButton btnConsultar = (LinkButton)sender;
            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;
            strId = selectedRow.Cells[0].Text;
            Response.Redirect("frmCrudUsuario.aspx?id=" + strId + "&op=U");
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "OpenModal")
            {
                string id = e.CommandArgument.ToString();
                lblID.Text = id;
                ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "showModal('#mdlEliminarUsuario');", true);
            }
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            lblID.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, GetType(), "funciones", "closeModal('#mdlEliminarUsuario');", true);
        }

        protected void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            var usu = new Usuario();
            int numReg;
            usu.Id = int.Parse(lblID.Text);
            numReg = usu.Eliminar();
            if (numReg > 0)
            {
                Response.AppendHeader("Refresh", "1; URL=frmUsuarios.aspx");
            }
            else
            {
                string script = "executeAlertify(false, 'No se pudo borrar al usuario, consulte con el administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
        }
    }
}