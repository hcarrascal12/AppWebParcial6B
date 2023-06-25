using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class Dashboard : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!Request.IsAuthenticated)
                {
                    Response.Redirect("frmLogin.aspx");
                }
            }

            lblCurrentYear.Text = DateTime.Now.Year.ToString();
            Page.DataBind();
            string rutaActual = Request.Url.AbsolutePath;
            hlPersonas.CssClass = "nav-link dropdown-toggle";
            hlBiblioteca.CssClass = "nav-link dropdown-toggle";

            if (rutaActual.Equals("/frmUsuarios.aspx", StringComparison.OrdinalIgnoreCase) || 
                rutaActual.Equals("/frmCrudUsuario.aspx", StringComparison.OrdinalIgnoreCase) ||
                rutaActual.Equals("/frmLectores.aspx", StringComparison.OrdinalIgnoreCase) ||
                rutaActual.Equals("/frmCrudLector.aspx", StringComparison.OrdinalIgnoreCase))
            {
                hlPersonas.CssClass = hlPersonas.CssClass + " active";   
            }

            if (rutaActual.Equals("/frmCategorias.aspx", StringComparison.OrdinalIgnoreCase))
            {
                hlBiblioteca.CssClass = hlBiblioteca.CssClass + " active";
            }

        }


        protected string GetHyperLinkClass(string pageName, string additionalClasses)
        {
            string currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            bool isActive = string.Equals(currentPage, pageName, StringComparison.OrdinalIgnoreCase);

            if (isActive)
                return "active " + additionalClasses;
            else
                return additionalClasses;
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}