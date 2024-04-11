using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fastBarberTG.Models;
using System.Data.SqlClient;
using fastBarberTG.Models.dto;
using fastBarberTG.Models.Repositories;

namespace fastBarberTG.Controllers
{
    public class MarcarCorteController : Controller
    {
        private NewCostumerREPO _newCostumerREPO;
        private HorariosAgREPO _horariosAgREPO;
        private DayOfWeekREPO _dayOfWeekRepo;
        public MarcarCorteController()
        {
            _newCostumerREPO = new NewCostumerREPO();
            _horariosAgREPO = new HorariosAgREPO();
            _dayOfWeekRepo = new DayOfWeekREPO();
        }

        public ActionResult Index()
        {
            return View();
        }

        public string NewCostumer(Costumer costumer)
        {
            try
            {
                return _newCostumerREPO.AddCostumer(costumer);

            } catch (SqlException ex)
            {
                return ex.Message.ToString();
            }
        }

        public ActionResult GridCalendario()
        {
            return View("_GridCalendario", _dayOfWeekRepo.DaysOfWeek());
        }

        public int ExistsCostumer(Costumer costumer)
        {
            return _newCostumerREPO.ExistsCostumer(costumer);
        }

        public ActionResult procurarCortes(Costumer costumer)
        {
            if( ExistsCostumer(costumer) == 1)
            {
                var result = _horariosAgREPO.BuscarCorteCliente(costumer);
                return View("_listaCorteCliente", result);
            } else
            {
                return View("_DontExistsClient");
            }

        }

        public void DesmarcarCorte (int id )
        {
            _horariosAgREPO.DesmarcarCorte(id);
        }

        public void MarcarHorario (decimal cpf, string data)
        {
            _horariosAgREPO.MarcarHorario(cpf, data);
        }

        public ActionResult HorariosDisponiveis(string data)
        {
            return View("_GridHorariosDisponiveis", new HorariosDisponiveis()
            {
                horariosOcupados = _horariosAgREPO.BuscaOcupado(data),
                DayOfWeek = _dayOfWeekRepo.DaysOfWeek().FirstOrDefault(x => x.Id == (int)DateTime.Parse(data).DayOfWeek),
                Data = DateTime.Parse(data)
            });
        }
    }
}