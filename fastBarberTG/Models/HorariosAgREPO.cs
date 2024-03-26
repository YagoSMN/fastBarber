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
                        Sobrenome = reader["SNome"].ToString(),
                        DataNasc = DateTime.Parse(reader["DataNasc"].ToString()),
                        Tel = reader["Tel"].ToString(),
                        Email = reader["Email"].ToString()
                    };
                    obj.Add(hasObj);
                }

                return obj;
            }
        }

        public HorariosMarcadosModel BuscarCorteCliente(Costumer costumer)
        {
            using (contexto = new Contexto())
            {
                var Cpf = new SqlParameter("@Cpf", SqlDbType.Decimal) { Value = costumer.Cpf };
                var reader = contexto.ExecutaProcedureComRetorno("FBSP_BuscarCorteCliente", Cpf);

                if (reader.Read())
                {
                    var obj = new HorariosMarcadosModel()
                    {
                        HorarioId = int.Parse(reader["Id"].ToString()),
                        Id_Cliente = int.Parse(reader["Id_Cliente"].ToString()),
                        StatusCorte = int.Parse(reader["StatusCorte"].ToString()),
                        BarberId = int.Parse(reader["BarberId"].ToString()),
                        DataCorte = DateTime.Parse(reader["DataCorte"].ToString()),
                        TempoCorte = reader["TempoCorte"].ToString()
                    };

                    return obj;
                }
                else
                {
                    return null;
                }
            }
        }

        public void DesmarcarCorte(int id)
        {
            using (contexto = new Contexto())
            {
                var ID = new SqlParameter("@Id", SqlDbType.Int) { Value = id };
                contexto.ExecutaProcedure("FBSP_DesmarcarHorario", ID);
            }
        }

        public IEnumerable<HorariosOcupados> BuscaOcupado (string data)
        {
            using(contexto = new Contexto())
            {
                var Data = new SqlParameter("@DataMarcacao", SqlDbType.NVarChar, 10) { Value = data };
                var reader = contexto.ExecutaProcedureComRetorno("FBSP_BuscaOcupadoData", Data);
                var listObj = new List<HorariosOcupados>();
                while (reader.Read())
                {
                    var obj = new HorariosOcupados()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        DataCorte = (reader["DataCorte"].ToString())
                    };
                    listObj.Add(obj);
                }
                return listObj;
            }
        }

        public void MarcarHorario(decimal cpf, string data)
        {
            using(contexto = new Contexto())
            {
                var Cpf = new SqlParameter("@Cpf", SqlDbType.Decimal) { Value = cpf };
                var Data = new SqlParameter("@DataCorte", SqlDbType.NVarChar, 20) { Value = data };
                contexto.ExecutaProcedure("FBSP_MarcarHorario", Cpf, Data);
            }
        }

        public HorariosMarcadosModel BuscaCostumer(int Id)
        {
            using (contexto = new Contexto())
            {
                var id = new SqlParameter("@Id", SqlDbType.Int) { Value = Id};
                var reader = contexto.ExecutaProcedureComRetorno("FBSP_SelCliente", id);
                var horarioMarcado = new HorariosMarcadosModel();
                while (reader.Read())
                {
                    horarioMarcado.HorarioId = int.Parse(reader["Id"].ToString());
                    horarioMarcado.Id_Cliente = int.Parse(reader["Id_Cliente"].ToString());
                    horarioMarcado.StatusCorte = int.Parse(reader["StatusCorte"].ToString());
                    horarioMarcado.BarberId = int.Parse(reader["BarberId"].ToString());
                    horarioMarcado.DataCorte = DateTime.Parse(reader["DataCorte"].ToString());
                    horarioMarcado.TempoCorte = reader["TempoCorte"].ToString();
                }
                return horarioMarcado;
            }
        }
    }
}