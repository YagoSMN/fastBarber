using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fastBarberTG.Models;
using System.Data.SqlClient;

namespace fastBarberTG.Controllers
{
    public class MarcarCorteController : Controller
    {
        private NewCostumerREPO _newCostumerREPO;
        private HorariosAgREPO _horariosAgREPO;
        public MarcarCorteController()
        {
            _newCostumerREPO = new NewCostumerREPO();
            _horariosAgREPO = new HorariosAgREPO();
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

        public JsonResult BuscaOcupado(string data)
        {
            var lista = _horariosAgREPO.BuscaOcupado(data);
            return Json(lista, JsonRequestBehavior.AllowGet); 
        }
    }
}