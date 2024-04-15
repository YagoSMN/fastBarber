using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fastBarberTG.Models.dto;
using fastBarberTG.Models.Repositories;

namespace fastBarberTG.Controllers
{
    public class UserController : Controller
    {
        private DiaSemanaRepo diarepo = new DiaSemanaRepo();
        private BarberREPO barberrepo = new BarberREPO();
        // GET: User
        [Authorize]
        public ActionResult Index() => View();

        [Authorize]
        public ActionResult Horarios() => View();

        [Authorize]
        public ActionResult BuscarDiaSemana(int id)
        {
            return View("_formDiaSemana", diarepo.BuscaDiaSemana(id));
        }

        [Authorize]
        public void SalvarDiaSemana(DiaSemana model)
        {
            diarepo.SalvarDiaSemana(model);
        }

        [Authorize]
        public ActionResult AlterarSenha()
        {
            return View("AlterarSenha");
        }

        [Authorize]
        public void SalvarSenha(string Senha)
        {
            barberrepo.AlterarSenha(Senha);
        }
    }
}