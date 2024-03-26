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

        public ActionResult CostumerDetails(int id)
        {
            return View(repo.BuscaCostumer(id));
        }
    }
}