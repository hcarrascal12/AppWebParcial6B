using AccesoDato;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace ReglaNegocio
{
    public class Grilla
    {
        BaseDeDato bd;

        public Grilla()
        {
            bd = new BaseDeDato();
        }

        public void Preparar(string pComando, CommandType pTipo)
        {
            bd.Conectar();
            bd.CrearComando(pComando, pTipo);

        }

        public void AsignarParametro(string pNombre, OleDbType pTipo, object pValor)
        {
            bd.AsignarParametro(pNombre, pTipo, pValor);
        }

        public void Cargar(GridView pGrilla)
        {
            var dt = new DataTable();
            dt = bd.EjecutarConsulta();
            bd.Desconectar();
            pGrilla.DataSource = dt;

            pGrilla.DataBind();
        }

        public void Cargar(GridView pGrilla, string pComando, CommandType pTipo)
        {
            Preparar(pComando, pTipo);
            Cargar(pGrilla);
        }

        public void CargarComboParametros(GridView pGrilla, string pComando, CommandType pTipo)
        {
            Preparar(pComando, pTipo);
            Cargar(pGrilla);
        }
    }
}
