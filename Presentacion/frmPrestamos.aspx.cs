using ReglaNegocio;
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
            Prestamo.CargarComboLector(cmbLectores);
            Prestamo.CargarGrillaPrestamos(gvPrestamos);
            ClientScript.RegisterStartupScript(GetType(), "InitializeDatatable", "$(document).ready(function() { $('.tblPrestamos').prepend($('<thead></thead>').append($('.tblPrestamos').find('tr:first'))).DataTable({searching: false, language: {url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json'}});; });", true);

        }
    }
}