using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fastBarberTG.Models.Repositories
{
    public class DayOfWeekREPO
    {
        private Contexto contexto;

        public IEnumerable<DayOfWeek> DaysOfWeek()
        {
            using (contexto = new Contexto())
            {
                var reader = contexto.ExecutaProcedureComRetorno("FBSP_BuscaHorarioFunc");
                var obj = new List<DayOfWeek>();
                while (reader.Read())
                {
                    var hasObj = new DayOfWeek()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Nome_Dia = reader["Nome_Dia"].ToString(),
                        Horario_Inicio = TimeSpan.Parse(reader["Horario_Inicio"].ToString()),
                        Horario_AlmocoInicio = TimeSpan.Parse(reader["Horario_AlmocoInicio"].ToString()),
                        Horario_AlmocoFim = TimeSpan.Parse(reader["Horario_AlmocoFim"].ToString()),
                        Horario_Fim = TimeSpan.Parse(reader["Horario_Fim"].ToString()),
                        Ind_Ativo = char.Parse(reader["Ind_Ativo"].ToString())
                    };
                    obj.Add(hasObj);
                }

                return obj;
            }
        }
    }
}