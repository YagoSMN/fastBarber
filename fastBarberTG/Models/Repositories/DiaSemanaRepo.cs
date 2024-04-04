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

        public void SalvarDiaSemana(DiaSemana model)
        {
            using (contexto = new Contexto())
            {
                var id = new SqlParameter("@Id", SqlDbType.Int) { Value = model.Id};
                var horaIni = new SqlParameter("@Horario_Ini", SqlDbType.Time) {Value = model.Horario_Inicio};
                var horaAlmocoIni = new SqlParameter("@Horario_AlmocoIni", SqlDbType.Time) { Value = model.Horario_AlmocoInicio };
                var horaAlmocoFim = new SqlParameter("@Horario_AlmocoFim", SqlDbType.Time) { Value = model.Horario_AlmocoFim };
                var horaFim = new SqlParameter("@Horario_Fim", SqlDbType.Time) { Value = model.Horario_Fim };
                var ind_Ativo = new SqlParameter("@Ind_Ativo", SqlDbType.Char) { Value = model.Ind_Ativo};
                contexto.ExecutaProcedure("FBSP_SalvarDiaSemana", id, horaIni, horaAlmocoIni, horaAlmocoFim, horaFim, ind_Ativo);
            }
        }
    }
}