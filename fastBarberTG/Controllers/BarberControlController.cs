using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fastBarberTG.Models;

namespace fastBarberTG.Controllers
{
    public class BarberControlController : Controller
    {
        private HorariosAgREPO repo = new HorariosAgREPO();

        // GET: BarberControl
        public ActionResult Index() //Lista todos os cortes de cabelo para o dia atual.
        {
            return View(repo.HorariosMarcados());
        }

        [HttpGet]
        public ActionResult CostumerDetails(int id)
        {
            return View("CostumerDetails", repo.BuscaCostumer(id));
        }

        public void DesmarcarCorte(int Id)
        {
            repo.DesmarcarCorte(Id);
        }

        public void FinalizarCorte(int Id, string TempoCorte)
        {
            repo.finalizarCorte(Id, TempoCorte);
        }
    }
}