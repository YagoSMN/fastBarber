using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using fastBarberTG.Models;
using System.Data.SqlClient;
using System.Data;

namespace fastBarberTG.Models
{
    public class NewCostumerREPO
    {
        private Contexto contexto; 

        private enum Procedures
        {
            FBSP_AddOrDenyCostumer
        }

        public string AddCostumer(Costumer costumer)
        {
            using (contexto = new Contexto())
            {
                var Cpf = new SqlParameter("@Cpf", SqlDbType.Decimal) { Value = costumer.Cpf };
                var Nome = new SqlParameter("@Nome", SqlDbType.NVarChar, 20) { Value = costumer.Nome };
                var SNome = new SqlParameter("@SNome", SqlDbType.NVarChar, 10) { Value = costumer.Sobrenome };
                var DataNasc = new SqlParameter("@DataNasc", SqlDbType.Date) { Value = costumer.DataNasc };
                var Tel = new SqlParameter("@Tel", SqlDbType.NVarChar, 11) { Value = costumer.Tel };
                var Email = new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = costumer.Email };

                var resultParameter = new SqlParameter("@Result", SqlDbType.NVarChar, 100);
                resultParameter.Direction = ParameterDirection.Output;

                var parameters = new SqlParameter[] { Cpf, Nome, SNome, DataNasc, Tel, Email, resultParameter };
                SqlDataReader reader = contexto.ExecutaProcedureComRetorno("FBSP_AddOrDenyCostumer", parameters);
                string resultMessage = resultParameter.Value.ToString();
                return resultMessage;
            } 
        }


        public int ExistsCostumer(Costumer costumer)
        {
            using (contexto = new Contexto())
            {
                var Cpf = new SqlParameter("@Cpf", SqlDbType.Decimal) { Value = costumer.Cpf };
                var reader = contexto.ExecutaProcedureComRetorno("FBSP_ExistsCostumer", Cpf);

                int result = 0;
                while (reader.Read())
                {
                    result = int.Parse(reader["Result"].ToString());
                };

                return result;

            }
        }


    }
}