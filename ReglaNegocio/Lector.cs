using AccesoDato;
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
    public class Lector
    {
        BaseDeDato bd;
        private int id, bdCodeError;
        private string n_ide, nombre, apellido, email, tel, estado, bdMsgError;

        public Lector() 
        {
                bd = new BaseDeDato();
        } 

        public int Id
        {
            set { this.id = value;}
            get { return this.id;}
        }

        public int BdCodeError
        {
            set { this.bdCodeError = value; }
            get { return this.bdCodeError; }
        }

        public string BdMsgError
        {
            set { this.bdMsgError = value; }
            get { return this.bdMsgError; }
        }

        public string N_ide
        {
            set { this.n_ide = value;}
            get { return this.n_ide;}
        }

        public string Nombre
        {
            set { this.nombre = value; }
            get { return this.nombre; }
        }

        public string Apellido
        {
            set { this.apellido = value;}
            get { return this.apellido; }
        }

        public string Email
        {
            set { this.email = value; }
            get { return this.email; }
        }

        public string Tel
        {
            set { this.tel = value; }
            get { return this.tel;}
        }

        public string Estado
        {
            set { this.estado = value; }
            get { return this.estado; }
        }

        public static void CargarGrilla(GridView pGrilla)
        {
            var vSql = "SELECT [Id], [N_ide], [Nombre], [Apellido], [Email], [Tel] FROM lectores WHERE [Estado] = 'A'";
            var gv = new Grilla();
            gv.Cargar(pGrilla, vSql, CommandType.Text);

        }

        public List<string> Consultar(int pId)
        {
            var vSql = "SELECT [N_ide], [Nombre], [Apellido], [Email], [Tel] FROM lectores WHERE [Id] =?";
            var datos = new List<string>();
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            bd.AsignarParametro("?", OleDbType.Integer, pId);
            OleDbDataReader dr = bd.EjecutarConsultaReader();
            if (dr.Read())
            {
                datos.Add(dr["N_ide"].ToString());
                datos.Add(dr["Nombre"].ToString());
                datos.Add(dr["Apellido"].ToString());
                datos.Add(dr["Email"].ToString());
                datos.Add(dr["Tel"].ToString());
            }
            bd.Desconectar();
            return datos;

        }

        public bool ConsultarIntegridad(int pId, string pNide)
        {
            var vSql = "SELECT count(*) AS 'NumLectores' FROM lectores WHERE [Id] <> ? AND [N_ide] = ?";
            bool existe = false;
            int intCantRegistros = 0;
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            bd.AsignarParametro("?", OleDbType.Integer, pId);
            bd.AsignarParametro("?", OleDbType.VarChar, pNide);
            OleDbDataReader dr = bd.EjecutarConsultaReader();
            if (dr.Read())
            {
                intCantRegistros = int.Parse(dr["NumLectores"].ToString());
            }
            bd.Desconectar();

            if(intCantRegistros >= 1)
            {
                existe = true;
            }

            return existe;

        }

        public int Insertar()
        {
            int numReg = 0;
            var vSql = "insert into Lectores ([N_ide], [Nombre], [Apellido], [Email], [Tel], [Estado]) values (?,?,?,?,?,?)";
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            bd.AsignarParametro("?", OleDbType.VarChar, this.N_ide);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Nombre);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Apellido);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Email);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Tel);
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
            var vSql = "UPDATE Lectores SET [N_ide] = ?, [Nombre] = ?, [Apellido] = ?, [Email] = ?, [Tel] = ? WHERE [id]=?";

            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            bd.AsignarParametro("?", OleDbType.VarChar, this.N_ide);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Nombre);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Apellido);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Email);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Tel);
            bd.AsignarParametro("?", OleDbType.Integer, this.Id);
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
            var vSql = "UPDATE lectores SET [Estado] = 'I' WHERE [Id] = ?";
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            bd.AsignarParametro("?", OleDbType.Integer, this.Id);
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
