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
    public class Libro
    {
        BaseDeDato bd;
        private int id, idautor, idEdicion, idEditorial, idCategoria, bdCodeError;
        private string isbn, nombre, estado, bdMsgError, disponible;

        public Libro()
        {
            bd = new BaseDeDato();
        }

        public int Id
        {
            set { this.id = value; }
            get { return this.id; }
        }

        public int IdAutor
        {
            set { this.idautor = value; }
            get { return this.idautor; }
        }

        public int IdEdicion
        {
            set { this.idEdicion = value; }
            get { return this.idEdicion; }
        }

        public int IdEditorial
        {
            set { this.idEditorial = value; }
            get { return this.idEditorial; }
        }

        public int IdCategoria
        {
            set { this.idCategoria = value; }
            get { return this.idCategoria; }
        }

        public int BdCodeError
        {
            set { this.bdCodeError = value; }
            get { return this.bdCodeError; }
        }

        public string Isbn
        {
            set { this.isbn = value; }
            get { return this.isbn; }
        }

        public string Nombre
        {
            set { this.nombre = value; }
            get { return this.nombre; }
        }

        public string Estado
        {
            set { this.estado = value; }
            get { return this.estado; }
        }

        public string Disponible
        {
            set { this.disponible = value; }
            get { return this.disponible; }
        }

        public string BdMsgError
        {
            set { this.bdMsgError = value; }
            get { return this.bdMsgError; }
        }

        public static void CargarCombo(DropDownList pCombo, string vComando)
        {
            var cmb = new Combo();
            cmb.Cargar(pCombo, vComando, CommandType.Text, "Id", "Descripcion");
        }

        public List<string> Consultar(int pId)
        {
            var vSql = "SELECT [Id], [Isbn], [Nombre], [Idautor], [IdEdicion], [IdEditorial], [IdCategoria] " +
                       "FROM libro WHERE [Id] =?";
            var datos = new List<string>();
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            bd.AsignarParametro("?", OleDbType.Integer, pId);
            OleDbDataReader dr = bd.EjecutarConsultaReader();
            if (dr.Read())
            {
                datos.Add(dr["Isbn"].ToString());
                datos.Add(dr["Nombre"].ToString());
                datos.Add(dr["Idautor"].ToString());
                datos.Add(dr["IdEdicion"].ToString());
                datos.Add(dr["IdEditorial"].ToString());
                datos.Add(dr["IdCategoria"].ToString());
            }
            bd.Desconectar();
            return datos;

        }

        public string TraerID(string pIsbn)
        {
            var vSql = "SELECT [Id] " +
                       "FROM libro WHERE [Isbn] =?";
            var datos = "";
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            bd.AsignarParametro("?", OleDbType.VarChar, pIsbn);
            OleDbDataReader dr = bd.EjecutarConsultaReader();
            if (dr.Read())
            {
                datos = dr["Id"].ToString();
            }
            bd.Desconectar();
            return datos;

        }

        public static void CargarGrilla(GridView pGrilla)
        {
            var vSql = "SELECT l.[Id], l.[Isbn], l.[Nombre], a.[Descripcion] AS 'Autor', c.[Descripcion] AS 'Categoria', et.[Descripcion] AS 'Editorial', ec.[Descripcion] as 'Edicion', CASE l.[Disponible] WHEN 'S' THEN 'Si' ELSE 'No' END AS 'Disponible' " +
                       "FROM Libro l, autores a, categorias c, editorial et, ediciones ec " +
                       "WHERE l.[Idautor] = a.[Id] AND l.[IdEdicion] = ec.[Id] AND l.[IdEditorial] = et.[Id] AND l.[IdCategoria] = c.[Id] AND " +
                       "l.[estado] = 'A'";
            var gv = new Grilla();
            gv.Cargar(pGrilla, vSql, CommandType.Text);

        }

        public int Insertar()
        {
            int numReg = 0;
            var vSql = "insert into Libro ([Isbn], [Nombre], [Idautor], [IdEdicion], [IdEditorial], [IdCategoria], [Estado], [Disponible]) values (?,?,?,?,?,?,?,?)";
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Isbn);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Nombre);
            bd.AsignarParametro("?", OleDbType.Integer, this.IdAutor);
            bd.AsignarParametro("?", OleDbType.Integer, this.IdEdicion);
            bd.AsignarParametro("?", OleDbType.Integer, this.IdEditorial);
            bd.AsignarParametro("?", OleDbType.Integer, this.IdCategoria);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Estado);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Disponible);
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
            var vSql = "UPDATE Libro SET [Isbn]= ?, [Nombre]= ?, [Idautor]= ?,  [IdEdicion]= ?, [IdEditorial]= ?, [IdCategoria]= ?, [Estado]= ? "+
                       "WHERE [id]=?";

            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Isbn);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Nombre);
            bd.AsignarParametro("?", OleDbType.Integer, this.IdAutor);
            bd.AsignarParametro("?", OleDbType.Integer, this.IdEdicion);
            bd.AsignarParametro("?", OleDbType.Integer, this.IdEditorial);
            bd.AsignarParametro("?", OleDbType.Integer, this.IdCategoria);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Estado);
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
            var vSql = "UPDATE Libro SET [Estado] = 'I' WHERE [Id] = ?";
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
