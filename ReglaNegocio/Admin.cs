using AccesoDato;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ReglaNegocio
{
    public class Admin
    {
        BaseDeDato bd;
        public Admin() { 
            bd = new BaseDeDato();
        }


        public string getCantUsuarios()
        {
            string numUsuarios = string.Empty;
            var vSql = "SELECT count(*) as 'numReg' FROM usuarios WHERE [Estado] = 'A'";
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            OleDbDataReader dr = bd.EjecutarConsultaReader();
            if (dr.Read())
            {
                numUsuarios = dr["numReg"].ToString();
            }
            bd.Desconectar();
            return numUsuarios;
        }

        public string getCantLectores()
        {
            string numLectores = string.Empty;
            var vSql = "SELECT count(*) as 'numReg' FROM lectores WHERE [Estado] = 'A'";
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            OleDbDataReader dr = bd.EjecutarConsultaReader();
            if (dr.Read())
            {
                numLectores = dr["numReg"].ToString();
            }
            bd.Desconectar();
            return numLectores;
        }

        public string getCantLibros()
        {
            string numLibros = string.Empty;
            var vSql = "SELECT count(*) as 'numReg' FROM libro WHERE [Estado] = 'A'";
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            OleDbDataReader dr = bd.EjecutarConsultaReader();
            if (dr.Read())
            {
                numLibros = dr["numReg"].ToString();
            }
            bd.Desconectar();
            return numLibros;
        }

        public string getCantPrestamos()
        {
            string numLibros = string.Empty;
            var vSql = "SELECT count(*) as 'numReg' FROM prestamo WHERE [EstadoPrestamo] = 'A'";
            bd.Conectar();
            bd.CrearComando(vSql, CommandType.Text);
            OleDbDataReader dr = bd.EjecutarConsultaReader();
            if (dr.Read())
            {
                numLibros = dr["numReg"].ToString();
            }
            bd.Desconectar();
            return numLibros;
        }
    }
}
