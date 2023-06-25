using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AccesoDato
{
    public class BaseDeDato
    {
        string cadConex;
        OleDbConnection cn;
        OleDbCommand cmd;

        public int BdCodeError { get; set; }
        public string BdMsgError { get; set; }

        public BaseDeDato()
        {
            BdCodeError = 0;
            BdMsgError = string.Empty;
            cadConex = ConfigurationManager.ConnectionStrings["cadConexSqlServer"].ConnectionString;
        }

        public void Conectar()
        {
            cn = new OleDbConnection(cadConex);
            cn.Open();
        }

        public void Desconectar() => cn.Close();

        public void CrearComando(string pComando, CommandType pTipo)
        {
            cmd = new OleDbCommand(pComando, cn);
            cmd.CommandType = pTipo;
        }

        public void AsignarParametro(string pNombre, OleDbType pTipo, object pValor)
        {
            cmd.Parameters.Add(pNombre, pTipo).Value = pValor;
        }

        public int EjecutarComando()
        {
            int numReg = 0;
            try
            {
                numReg = cmd.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                BdCodeError = ex.ErrorCode;
                BdMsgError = ex.Message;
            }
            finally
            {
                if (BdCodeError != 0)
                {
                    cmd.Dispose();
                    cn.Close();

                }
            }
            return numReg;
        }

        public OleDbDataReader EjecutarConsultaReader() => cmd.ExecuteReader();

        public DataTable EjecutarConsulta()
        {
            var dt = new DataTable();
            var da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

    }
}
