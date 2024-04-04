using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using fastBarberTG.Models.dto;

namespace fastBarberTG.Models.Repositories
{
    public class DiaSemanaRepo
    {
        private Contexto contexto;

        public DiaSemana BuscaDiaSemana(int id)
        {
            using (contexto = new Contexto())
            {
                var Id = new SqlParameter("@Id", SqlDbType.Int) { Value = id };
                var reader = contexto.ExecutaProcedureComRetorno("FBSP_BuscaDiaSemana", Id);
                var obj = new DiaSemana();
                while (reader.Read())
                {

                    {
                        obj.Id = Byte.Parse(reader["Id"].ToString());
                        obj.Nome_Dia = reader["Nome_Dia"].ToString();
                        obj.Horario_Inicio = TimeSpan.Parse(reader["Horario_Inicio"].ToString());
                        obj.Horario_AlmocoInicio = TimeSpan.Parse(reader["Horario_AlmocoInicio"].ToString());
                        obj.Horario_AlmocoFim = TimeSpan.Parse(reader["Horario_AlmocoFim"].ToString());
                        obj.Horario_Fim = TimeSpan.Parse(reader["Horario_Fim"].ToString());
                        obj.Ind_Ativo = reader["Ind_Ativo"].ToString()[0];
                    };

                }

                return obj;
            }
        }
    }
}