using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fastBarberTG.Models.Repositories;

namespace fastBarberTG.Controllers
{
    public class UserController : Controller
    {
        private DiaSemanaRepo diarepo = new DiaSemanaRepo();
        // GET: User
        public ActionResult Index() => View();

        public ActionResult Horarios() => View();

        public ActionResult BuscarDiaSemana(int id)
        {
            return View("_formDiaSemana", diarepo.BuscaDiaSemana(id));
        }
    }
}