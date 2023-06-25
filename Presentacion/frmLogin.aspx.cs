using ReglaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class frmLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.IsAuthenticated)
                {
                    Response.Redirect("frmAdmin.aspx");
                }

            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var usu = new Usuario();
            List<string> usuarioEncontrado;

            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                string script = "executeAlertify(false, 'Los campos no pueden estar vacíos');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }

            if (usu.PermitirAcceso(txtUsername.Text, txtPassword.Text))
            {
                //Genero ticket
                FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, true);
                usuarioEncontrado = usu.ConsultarPorUsername(txtUsername.Text);
                Session["id"] = usuarioEncontrado[0];
                Session["username"] = usuarioEncontrado[1];
                Session["nombre"] = usuarioEncontrado[3];
                Session["IdTipoUsuario"] = usuarioEncontrado[7];
                if (int.Parse(usuarioEncontrado[7]) == 1)
                {
                    Response.Redirect("frmAdmin.aspx");
                }
            }
            else
            {
                string script = "executeAlertify(false, 'Usuario o contraseña incorrecta');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
        }
    }
}