using ReglaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class frmAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var adm = new Admin();
                lblWelcome.InnerText = "Bienvenido " + Session["nombre"];
                lblUsuarios.Text = adm.getCantUsuarios();
                lblLectores.Text = adm.getCantLectores();
                lblLibros.Text = adm.getCantLibros();
                lblPrestamos.Text = adm.getCantPrestamos();
            }
            
        }
    }
}