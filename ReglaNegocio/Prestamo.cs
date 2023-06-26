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
    public class Prestamo
    {
        BaseDeDato bd;
        private DateTime fechaPrestamo, fechaDevolucion, fechaConfirmacionDevolucion;
        private int id, idLector, idLibro, bdCodeError;
        private string estadoEntregado, estadoRecibido, estadoPrestamo, bdMsgError;

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

        public DateTime FechaPrestamo
        {
            set { this.fechaPrestamo = value; }
            get { return this.fechaPrestamo; }
        }

        public DateTime FechaDevolucion
        {
            set { this.fechaDevolucion = value; }
            get { return this.fechaDevolucion; }
        }

        public DateTime FechaConfirmacionDevolucion
        {
            set { this.fechaConfirmacionDevolucion = value; }
            get { return this.fechaConfirmacionDevolucion; }
        }

        public int Id
        {
            set { this.id = value; }
            get { return this.id; }
        }

        public int IdLector
        {
            set { this.idLector = value; }
            get { return this.idLector; }
        }

        public int IdLibro
        {
            set { this.idLibro = value; }
            get { return this.idLibro; }
        }

        public string EstadoEntregado
        {
            set { this.estadoEntregado = value; }
            get { return this.estadoEntregado; }
        }

        public string EstadoRecibido
        {
            set { this.estadoRecibido = value; }
            get { return this.estadoRecibido; }
        }

        public string EstadoPrestamo
        {
            set { this.estadoPrestamo = value; }
            get { return this.estadoPrestamo; }
        }

        public Prestamo() { 
            bd = new BaseDeDato();
        }

        public static void CargarGrillaLibros(GridView pGrilla)
        {
            var vSql = "SELECT l.[Isbn], l.[Nombre], c.[Descripcion] as 'Categoria' FROM libro l, categorias c WHERE l.[IdCategoria] = c.[Id] AND l.[Estado] = 'A' and l.[Disponible] = 'S'";
            var gv = new Grilla();
            gv.Cargar(pGrilla, vSql, CommandType.Text);

        }
        public static void CargarGrillaLector(GridView pGrilla)
        {
            var vSql = "SELECT [n_ide], [Nombre], [Apellido] FROM lectores WHERE estado = 'A'";
            var gv = new Grilla();
            gv.Cargar(pGrilla, vSql, CommandType.Text);

        }

        public static void CargarGrillaPrestamos(GridView pGrilla)
        {
            var vSql = "SELECT p.[Id], CASE p.[EstadoPrestamo] WHEN 'A' THEN 'Prestado' WHEN 'I' THEN 'Devuelto' END AS 'EstadoPrestamo',  " +
                       "l.[N_ide], lb.[Nombre] AS 'NombreLibro', p.[FechaDevolucion], p.[FechaConfirmacionDevolucion] " +
                       "FROM Prestamo p, Lectores l, Libro lb " +
                       "WHERE p.[IdLector] = l.[Id] AND p.[IdLibro] = lb.[Id]";
            var gv = new Grilla();
            gv.Cargar(pGrilla, vSql, CommandType.Text);

        }

        public static void CargarComboLector(DropDownList pCombo)
        {
            var vSql = "SELECT [Id], [Nombre] FROM lectores WHERE estado = 'A'";
            var cmb = new Combo();
            cmb.Cargar(pCombo, vSql, CommandType.Text, "Id", "Nombre");
        }

        public bool PrestarLibro()
        {
            bool exitoso = true;
            var numRegPrestado = 0;
            var numRegLibro = 0;
            numRegPrestado = this.InsertarPrestamo();
            numRegLibro = this.ActualizarDisponibleLibro();

            if (numRegPrestado <= 0)
            {
                exitoso = false;
            }else if(numRegLibro <= 0)
            {
                exitoso = false;
            }

            return exitoso;
        }

        public int InsertarPrestamo()
        {
            int numReg = 0;
            var vSql = "insert into Prestamo ([IdLector], [IdLibro], [FechaDevolucion], [EstadoEntregado], [EstadoPrestamo]) values (?,?,?,?,?)";
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            bd.AsignarParametro("?", OleDbType.Integer, this.IdLector);
            bd.AsignarParametro("?", OleDbType.Integer, this.IdLibro);
            bd.AsignarParametro("?", OleDbType.Date, this.FechaDevolucion);
            bd.AsignarParametro("?", OleDbType.VarChar, this.EstadoEntregado);
            bd.AsignarParametro("?", OleDbType.VarChar, this.EstadoPrestamo);
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

        public int ActualizarDisponibleLibro()
        {
            int numReg = 0;
            var vSql = "Update libro SET [Disponible] = 'N' WHERE [Id] = ?";
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            bd.AsignarParametro("?", OleDbType.Integer, this.IdLibro);
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
