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
            /*bool vacio = false;*/
            var dt = new DataTable();
            /*string spanHtml = "<span style='display:block; width:100%; height:100%; text-align:center;'>No hay datos disponibles</span>";*/
           /* gvUsuarios.Controls.Add(new LiteralControl(spanHtml));*/
            dt = bd.EjecutarConsulta();
            bd.Desconectar();
          /*  if (dt.Rows.Count == 0)
            {
                *//*vacio = true;*//*
                dt.Rows.Add();
            }*/
            pGrilla.DataSource = dt;

            pGrilla.DataBind();

           /* if (vacio)
            {
                // Agregar el elemento <span> que cubra toda la tabla
                pGrilla.Controls.Add(new LiteralControl(spanHtml));
            }*/
        }

        public void Cargar(GridView pGrilla, string pComando, CommandType pTipo)
        {
            Preparar(pComando, pTipo);
            Cargar(pGrilla);
        }
    }
}
