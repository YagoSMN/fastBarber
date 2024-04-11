using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace fastBarberTG.Models.Repositories
{
    public class BarberREPO
    {
        private Contexto contexto;

        public bool LoginUsuario(string email, string senha)
        {
            using (contexto = new Contexto())
            {
                var Email = new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email };
                var Senha = new SqlParameter("@Senha", SqlDbType.NVarChar) { Value = senha };
                var reader = contexto.ExecutaProcedureComRetorno("FBSP_LoginUsuario", Email, Senha);
                var hasObj = new Barber();
                while (reader.Read())
                {

                    {
                        hasObj.Id = int.Parse(reader["Id"].ToString());
                        hasObj.Nome = reader["Nome"].ToString();
                        hasObj.Email = reader["Email"].ToString();
                        hasObj.Senha = reader["Senha"].ToString();
                    };
                }

                if (hasObj.Email == null)
                    return false;

                return true;
            }
        }
    }
}