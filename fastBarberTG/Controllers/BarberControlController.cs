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
        [Authorize]
        public ActionResult Index() //Lista todos os cortes de cabelo para o dia atual.
        {
            return View(repo.HorariosMarcados(null));
        }

        [Authorize]
        [HttpGet]
        public ActionResult CostumerDetails(int id)
        {
            return View("CostumerDetails", repo.BuscaCostumer(id));
        }

        [Authorize]
        public ActionResult DiaCortes(DateTime data)
        {
            return View(repo.HorariosMarcados(data));
        }

        [Authorize]
        public void DesmarcarCorte(int Id)
        {
            repo.DesmarcarCorte(Id);
        }

        [Authorize]
        public void FinalizarCorte(int Id, string TempoCorte)
        {
            repo.finalizarCorte(Id, TempoCorte);
        }

        [Authorize]
        public void IniciarCorte(int Id)
        {
            repo.iniciarCorte(Id);
        }
    }
}