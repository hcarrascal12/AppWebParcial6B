using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class frmPrestamos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cmbEstadoPrestamo.Items.Add(new ListItem("Prestado", "A"));
            cmbEstadoPrestamo.Items.Add(new ListItem("Devuelto", "I"));
            cmbEstadoPrestamo.SelectedIndex = 0;
        }
    }
}