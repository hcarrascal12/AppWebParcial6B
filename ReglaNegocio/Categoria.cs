﻿using AccesoDato;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ReglaNegocio
{
    public class Categoria
    {
        BaseDeDato bd;
        private int id, bdCodeError;
        private string descripcion, estado, bdMsgError;

        public int Id
        {
            set { this.id = value; }
            get { return this.id; }
        }

        public int BdCodeError
        {
            set { this.bdCodeError = value;}
            get { return this.bdCodeError; }
        }

        public string Descripcion
        {
            set { this.descripcion = value;}
            get { return this.descripcion;}
        }

        public string Estado
        {
            set { this.estado = value; }
            get { return this.estado; }
        }

        public string BdMsgError
        {
            set { this.bdMsgError = value;}
            get { return this.bdMsgError; }
        }

        public Categoria()
        {
            bd = new BaseDeDato();
        }

        public static void CargarGrilla(GridView pGrilla)
        {
            var vSql = "SELECT [Id], [Descripcion] FROM categorias WHERE estado = 'A'";
            var gv = new Grilla();
            gv.Cargar(pGrilla, vSql, CommandType.Text);

        }

        public int Insertar()
        {
            int numReg = 0;
            var vSql = "insert into Categorias ([Descripcion], [Estado]) values (?,?)";
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Descripcion);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Estado);
            numReg = bd.EjecutarComando();
            bd.Desconectar();
            if (numReg <= 0)
            {
                if (bd.BdCodeError != 0)
                {
                    BdCodeError = bd.BdCodeError;
                    BdMsgError = bd.BdMsgError;
                }
            }

            return numReg;
        }

        public int Actualizar()
        {
            int numReg = 0;
            var vSql = "UPDATE Categorias SET [Descripcion] = ? WHERE [ID] = ?";
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Descripcion);
            bd.AsignarParametro("?", OleDbType.Integer, this.id);
            numReg = bd.EjecutarComando();
            bd.Desconectar();
            if (numReg <= 0)
            {
                if (bd.BdCodeError != 0)
                {
                    BdCodeError = bd.BdCodeError;
                    BdMsgError = bd.BdMsgError;
                }
            }

            return numReg;
        }

        public int Eliminar()
        {
            int numReg = 0;
            var vSql = "UPDATE Categorias SET [estado] = 'I' WHERE [ID] = ?";
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            bd.AsignarParametro("?", OleDbType.Integer, this.id);
            numReg = bd.EjecutarComando();
            bd.Desconectar();
            if (numReg <= 0)
            {
                if (bd.BdCodeError != 0)
                {
                    BdCodeError = bd.BdCodeError;
                    BdMsgError = bd.BdMsgError;
                }
            }

            return numReg;
        }

    }
}
