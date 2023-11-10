using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using fastBarberTG.Models;

namespace fastBarberTG.Models
{
    public class HorariosAgREPO
    {
        private Contexto contexto;

        private enum Procedures
        {
            FBSP_MostraHorariosMarc
        }

        public IEnumerable<HorariosMarcadosModel> HorariosMarcados()
        {
            using (contexto = new Contexto())
            {
                var reader = contexto.ExecutaProcedureComRetorno("FBSP_MostraHorariosMarc");
                var obj = new List<HorariosMarcadosModel>();
                while (reader.Read())
                {
                    var hasObj = new HorariosMarcadosModel()
                    {
                        HorarioId = int.Parse(reader["Id"].ToString()),
                        Id_Cliente = int.Parse(reader["Id_Cliente"].ToString()),
                        DataCorte = DateTime.Parse(reader["DataCorte"].ToString()),
                        StatusCorte = int.Parse(reader["StatusCorte"].ToString()),
                        Cpf = reader["CPF"].ToString(),
                        Nome = reader["Nome"].ToString(),
                        Sobrenome = reader["Sobrenome"].ToString(),
                        DataNasc = DateTime.Parse(reader["DataNasc"].ToString()),
                        Tel = reader["Tel"].ToString(),
                        Email = reader["Email"].ToString()
                    };
                    obj.Add(hasObj);
                }

                return obj;
            }
        }
    }
}