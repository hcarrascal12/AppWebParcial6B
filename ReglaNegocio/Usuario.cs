using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDato;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Collections;
using System.Security.Cryptography;

namespace ReglaNegocio
{
    public class Usuario
    {
        BaseDeDato bd;
        private int id, idTipoUsuario, bdCodeError;
        private string nombre, apellido, password, email, estado, username, bdMsgError;

        public int Id
        {
            set { this.id = value; }
            get { return this.id; }
        }

        public string Password
        {
            set { this.password = value; }
            get { return this.password; }
        }

        public int IdTipoUsuario
        {
            set { this.idTipoUsuario = value; }
            get { return this.idTipoUsuario; }
        }

        public string Nombre
        {
            set { this.nombre = value; }
            get { return this.nombre; }
        }

        public string Apellido
        {
            set { this.apellido = value; }
            get { return this.apellido; }
        }

        public string Email
        {
            set { this.email = value; }
            get { return this.email; }
        }

        public string Estado
        {
            set { this.estado = value; }
            get { return this.estado; }
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

        public string Username
        {
            set { this.username = value; }
            get { return this.username; }
        }

        public Usuario()
        {
             bd = new BaseDeDato();
        }

        public static void CargarGrillaUsuarios(GridView pGrilla)
        {
            var vSql = "SELECT u.[Id], u.[Username], u.[Apellido], u.[Nombre], u.[Email], tu.[Nombre] as 'Tipo Usuario', CASE u.[Estado] WHEN 'A' THEN 'Activo' WHEN 'I' THEN 'Inactivo' ELSE u.[Estado] END AS 'Estado' " +
                       "FROM Usuarios u, tipo_usuario tu " +
                       "WHERE u.[IdTipoUsuario] = tu.[Id] AND " +
                       "u.[estado] = 'A'";
            var gv = new Grilla();
            gv.Cargar(pGrilla, vSql, CommandType.Text);

        }

        public static void CargarCombo(DropDownList pCombo, string vComando)
        {
            var cmb = new Combo();
            cmb.Cargar(pCombo, vComando, CommandType.Text, "Id", "Nombre");
        }

        public List<string> Consultar(int pId)
        {
            var vSql = "SELECT [Username], [Apellido], [Nombre], [Estado], [Email], [IdTipoUsuario] FROM usuarios WHERE [Id] =?";
            var datos = new List<string>();
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            bd.AsignarParametro("?", OleDbType.Integer, pId);
            OleDbDataReader dr = bd.EjecutarConsultaReader();
            if (dr.Read())
            {
                datos.Add(dr["Username"].ToString());
                datos.Add(dr["Nombre"].ToString());
                datos.Add(dr["Apellido"].ToString());
                datos.Add(dr["Email"].ToString());
                datos.Add(dr["Estado"].ToString());
                datos.Add(dr["IdTipoUsuario"].ToString());
            }
            bd.Desconectar();
            return datos;

        }

        public List<string> ConsultarPorUsername(string pUsername)
        {
            var vSql = "SELECT [Id], [Username], [Password], [Apellido], [Nombre], [Estado], [Email], [IdTipoUsuario] FROM usuarios WHERE [Username] =?";
            var datos = new List<string>();
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            bd.AsignarParametro("?", OleDbType.VarChar, pUsername);
            OleDbDataReader dr = bd.EjecutarConsultaReader();
            if (dr.Read())
            {
                datos.Add(dr["Id"].ToString());
                datos.Add(dr["Username"].ToString());
                datos.Add(dr["Password"].ToString());
                datos.Add(dr["Nombre"].ToString());
                datos.Add(dr["Apellido"].ToString());
                datos.Add(dr["Email"].ToString());
                datos.Add(dr["Estado"].ToString());
                datos.Add(dr["IdTipoUsuario"].ToString());
            }
            bd.Desconectar();
            return datos;

        }

        public int Insertar()
        {
            int numReg = 0;
            var vSql = "insert into Usuarios ([Username], [Nombre], [Apellido], [Email], [Password], [IdTipoUsuario], [estado]) values (?,?,?,?,?,?,?)";
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Username);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Nombre);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Apellido);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Email);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Password);
            bd.AsignarParametro("?", OleDbType.Integer, this.IdTipoUsuario);
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
            var vUpdate1 = "UPDATE Usuarios SET [Username] = ?, [Nombre] = ?, [Apellido] = ?, [Email] = ?, [IdTipoUsuario] = ?, [estado] = ?";
            var vWhere = " WHERE [id]=?";
            string vUpdate2 = ", [Password]= ?";
            string vSql = "";

            if (this.Password != null)
            {
                vSql = vUpdate1 + vUpdate2 + vWhere;
            }
            else
            {
                vSql = vUpdate1 + vWhere;
            }

            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Username);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Nombre);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Apellido);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Email);
            bd.AsignarParametro("?", OleDbType.Integer, this.IdTipoUsuario);
            bd.AsignarParametro("?", OleDbType.VarChar, this.Estado);
            if (!string.IsNullOrEmpty(this.Password))
            {
                bd.AsignarParametro("?", OleDbType.VarChar, this.Password);
            }
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
            var vSql = "UPDATE usuarios SET [Estado] = 'I' WHERE [Id] = ?";
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

        public static string HashPassword(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(32); // Tamaño del hash
                byte[] hashBytes = new byte[48]; // Tamaño del hash + tamaño de la sal
                Buffer.BlockCopy(salt, 0, hashBytes, 0, 16);
                Buffer.BlockCopy(hash, 0, hashBytes, 16, 32);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[16];
            Buffer.BlockCopy(hashBytes, 0, salt, 0, 16);
            string expectedHash = HashPassword(password, salt);
            return hashedPassword == expectedHash;
        }

        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            return salt;
        }

        public bool PermitirAcceso(string username, string password)
        {
            bool permitir = false;
            List<string> datos = ConsultarPorUsername(username);
            if (datos.Count > 0)
            {
                bool isMatch = VerifyPassword(password, datos[2]);
                if (isMatch)
                {
                    if (datos[6] == "A")
                    {
                        permitir = true;
                    }

                }
            }

            return permitir;
        }
    }
}
