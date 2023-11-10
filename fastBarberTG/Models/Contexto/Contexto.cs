using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace fastBarberTG.Models
{
    public class Contexto : IDisposable
    {
        private readonly SqlConnection connection;
        public Contexto()
        {
            connection = new SqlConnection(@"Data Source=K25\SQLEXPRESS; Integrated Security=SSPI;Initial Catalog=Fastbarber");
            connection.Open();
        }
        
        public void ExecutaProcedure(string ProcedureName, params SqlParameter[] parametros)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = ProcedureName,
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };

            if (parametros != null)
            {
                foreach (SqlParameter parametro in parametros)
                {
                    cmd.Parameters.Add(parametro);
                }
            }

            cmd.ExecuteNonQuery();
        }


        public SqlDataReader ExecutaProcedureComRetorno(string ProcedureName, params SqlParameter[] parametros)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = ProcedureName,
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };


            if (parametros != null)
            {
                foreach (SqlParameter parametro in parametros)
                {
                    cmd.Parameters.Add(parametro);
                }
            }

            return cmd.ExecuteReader();
        }

        public void Dispose()
        {
            if(connection.State == ConnectionState.Open)
                connection.Close();
        }
    }
}