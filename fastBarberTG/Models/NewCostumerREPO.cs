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

        public string AddCostumer (Costumer costumer)
        {
            using (contexto = new Contexto())
            {
                var Cpf = new SqlParameter("@Cpf", SqlDbType.Int) { Value = costumer.Cpf };
                var Nome = new SqlParameter("@Nome", SqlDbType.NVarChar, 20) { Value = costumer.Nome };
                var SNome = new SqlParameter("@SNome", SqlDbType.NVarChar, 10) { Value = costumer.Sobrenome };
                var DataNasc = new SqlParameter("@DataNasc", SqlDbType.Date) { Value = costumer.DataNasc };
                var Tel = new SqlParameter("@Tel", SqlDbType.NVarChar, 11) { Value = costumer.Tel };
                var Email = new SqlParameter("@Email", SqlDbType.NVarChar, 25) { Value = costumer.Email };
                var result = contexto.ExecutaProcedureComRetorno("FBSP_AddOrDenyCostumer");
                if (result.ToString() == "0")
                    return "Cliente adicionado com sucesso.";
                if(result.ToString() == "1")
                    return "Cliente já existe.";

                return "Problema na requisição";
            }
        }
    }
}