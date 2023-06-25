using AccesoDato;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ReglaNegocio
{
    public class Combo
    {
        BaseDeDato bd;

        public Combo()
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

        public void Cargar(DropDownList pCombo, string pValueField, string pTextField)
        {
            var dt = new DataTable();
            dt = bd.EjecutarConsulta();
            bd.Desconectar();
            pCombo.DataSource = dt;
            pCombo.DataValueField = pValueField;
            pCombo.DataTextField = pTextField;
            pCombo.DataBind();
        }

        public void Cargar(DropDownList pCombo, string pComando, CommandType pTipo, string pValueField, string pTextField)
        {
            Preparar(pComando, pTipo);
            Cargar(pCombo, pValueField, pTextField);
        }
    }
}
