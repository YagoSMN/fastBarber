﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using fastBarberTG.Models;
using fastBarberTG.Models.dto;

namespace fastBarberTG.Models
{
    public class HorariosAgREPO
    {
        private Contexto contexto;

        public IEnumerable<HorariosMarcadosModel> HorariosMarcados(DateTime? Date)
        {
            using (contexto = new Contexto())
            {
                var Data = new SqlParameter("@Data", SqlDbType.Date) {Value = Date};
                var reader = contexto.ExecutaProcedureComRetorno("FBSP_MostraHorariosMarc", Data);
                var obj = new List<HorariosMarcadosModel>();
                while (reader.Read())
                {
                    var hasObj = new HorariosMarcadosModel()
                    {
                        HorarioId = int.Parse(reader["Id"].ToString()),
                        Id_Cliente = int.Parse(reader["Id_Cliente"].ToString()),
                        DataCorte = DateTime.Parse(reader["DataCorte"].ToString()),
                        StatusCorte = int.Parse(reader["StatusCorte"].ToString()),
                        Cpf = decimal.Parse(reader["CPF"].ToString()),
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
                        DataCorte = DateTime.Parse(reader["DataCorte"].ToString())
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

        public BuscaCustumer BuscaCostumer(int Id)
        {
            using (contexto = new Contexto())
            {
                var id = new SqlParameter("@Id", SqlDbType.Int) { Value = Id};
                var reader = contexto.ExecutaProcedureComRetorno("FBSP_SelCliente", id);
                var horarioMarcado = new BuscaCustumer();
                while (reader.Read())
                {
                    horarioMarcado.horario = new HorariosMarcadosModel
                    {
                        HorarioId = int.Parse(reader["Id"].ToString()),
                        Id_Cliente = int.Parse(reader["Id_Cliente"].ToString()),
                        StatusCorte = int.Parse(reader["StatusCorte"].ToString()),
                        BarberId = int.Parse(reader["BarberId"].ToString()),
                        DataCorte = DateTime.Parse(reader["DataCorte"].ToString()),
                        TempoCorte = reader["TempoCorte"].ToString()
                    };

                    horarioMarcado.costumer = new Costumer
                    {
                        Id = int.Parse(reader["Id_Cliente"].ToString()),
                        Nome = reader["Nome"].ToString(),
                        Cpf = decimal.Parse(reader["CPF"].ToString()),
                        Sobrenome = reader["SNome"].ToString(),
                        DataNasc = DateTime.Parse(reader["DataNasc"].ToString())
                    };
                    
                    horarioMarcado.barber = new Barber
                    {
                        Id = int.Parse(reader["BarberId"].ToString()),
                        Nome = reader["BarberName"].ToString()
                    };
                    
                }

                return horarioMarcado;
            }
        }

        public void finalizarCorte(int id, string TempoCorte)
        {
            using (contexto = new Contexto())
            {
                var Id = new SqlParameter("@Id", SqlDbType.Int) { Value = id };
                var tempoCorte = new SqlParameter("@TempoCorte", SqlDbType.NVarChar, 8) { Value = TempoCorte };
                contexto.ExecutaProcedure("FBSP_finalizarHorario", Id, tempoCorte);
            }
        }

        public void iniciarCorte(int id)
        {
            using (contexto = new Contexto())
            {
                var Id = new SqlParameter("@Id", SqlDbType.Int) {Value = id};
                contexto.ExecutaProcedure("FBSP_IniciarCorte", Id);
            }
        }

    }
}