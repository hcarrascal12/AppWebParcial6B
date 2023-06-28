using ReglaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class frmCrudLibro : System.Web.UI.Page
    {
        public static int sID = -1;
        public static string sOpc = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            var libro = new Libro();
            List<string> datos;

            if (!Page.IsPostBack)
            {
                Libro.CargarCombo(cmbAutores, "SELECT [Id], [Descripcion] FROM autores WHERE [Estado] = 'A'");
                Libro.CargarCombo(cmbEdicion, "SELECT [Id], [Descripcion] FROM Ediciones WHERE [Estado] = 'A'");
                Libro.CargarCombo(cmbEditorial, "SELECT [Id], [Descripcion] FROM Editorial WHERE [Estado] = 'A'");
                Libro.CargarCombo(cmbCategoria, "SELECT [Id], [Descripcion] FROM Categorias WHERE [Estado] = 'A'");

                if (Request.QueryString["id"] != null)
                {
                    
                    sID = int.Parse(Request.QueryString["id"]);
                    datos = libro.Consultar(sID);
                    txtIsbn.Text = datos[0];
                    txtNombre.Text = datos[1];
                    cmbAutores.SelectedValue = datos[2];
                    cmbEdicion.SelectedValue = datos[3];
                    cmbEditorial.SelectedValue = datos[4];
                    cmbCategoria.SelectedValue = datos[5];
                    string script = "asignarDropdownListLibro('" + datos[2] + "', '" + datos[3] + "', '"+ datos[4] + "', '"+ datos[5] + "');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);

                }

                if (Request.QueryString["op"] != null)
                {
                    sOpc = Request.QueryString["op"].ToString();
                    switch (sOpc)
                    {
                        case "C":
                            lblTitulo.Text = "Crear libro";
                            btnGuardar.Visible = true;
                            break;
                        case "U":
                            lblTitulo.Text = "Actualizar libro";
                            btnActualizar.Visible = true;
                            break;
                    }
                }
                else
                {
                    Response.Redirect("frmLibros.aspx");
                }
            }
            Page.DataBind();
        }

        protected void Limpiar()
        {
            txtIsbn.Text = string.Empty;
            txtNombre.Text = string.Empty;
            cmbAutores.SelectedIndex = 0;
            cmbEdicion.SelectedIndex = 0;
            cmbEditorial.SelectedIndex = 0;
            cmbCategoria.SelectedIndex = 0;

        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var libro = new Libro();
            int numReg;
            string script;
            libro.Isbn = txtIsbn.Text;
            libro.Nombre = txtNombre.Text;
            libro.IdAutor = int.Parse(cmbAutores.SelectedValue);
            libro.IdEdicion = int.Parse(cmbEdicion.SelectedValue);
            libro.IdEditorial = int.Parse(cmbEditorial.SelectedValue);
            libro.IdCategoria = int.Parse(cmbCategoria.SelectedValue);
            libro.Estado = "A";
            libro.Disponible = "S";

            numReg = libro.Insertar();

            if (numReg > 0)
            {
                Limpiar();
                btnGuardar.Enabled = false;
                Response.AppendHeader("Refresh", "3; URL=frmLibros.aspx");
                script = "executeAlertify(true, 'Se ha creado el libro exitosamente');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
            else
            {
                script = "executeAlertify(false, 'No se pudo crear el libro, consulte con el administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }

        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            var libro = new Libro();
            int numReg;
            string script;
            libro.Id = sID;
            libro.Isbn = txtIsbn.Text;
            libro.Nombre = txtNombre.Text;
            libro.IdAutor = int.Parse(cmbAutores.SelectedValue);
            libro.IdEdicion = int.Parse(cmbEdicion.SelectedValue);
            libro.IdEditorial = int.Parse(cmbEditorial.SelectedValue);
            libro.IdCategoria = int.Parse(cmbCategoria.SelectedValue);
            libro.Estado = "A";


            numReg = libro.Actualizar();

            if (numReg > 0)
            {
                Limpiar();
                btnActualizar.Enabled = false;
                Response.AppendHeader("Refresh", "3; URL=frmLibros.aspx");
                script = "executeAlertify(true, 'Se ha actualizado el libro exitosamente');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
            else
            {
                script = "executeAlertify(false, 'No se pudo actualizar el libro, consulte con el administrador');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "funciones", script, true);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmLibros.aspx");
        }

        protected string GetTitulo()
        {
            string strTitulo = string.Empty;
            if (Request.QueryString["op"] != null)
            {
                sOpc = Request.QueryString["op"].ToString();
                switch (sOpc)
                {
                    case "C":
                        strTitulo = "Crear libro";
                        break;
                    case "U":
                        strTitulo = "Actualizar libro";
                        break;
                }
            }
            return strTitulo;
        }
    }
}