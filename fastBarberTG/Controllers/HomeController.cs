using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using fastBarberTG.Models.Repositories;

namespace fastBarberTG.Controllers
{
    public class HomeController : Controller
    {
        private BarberREPO _login;

        public HomeController()
        {
            _login = new BarberREPO();
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string senha)
        {

            if (IsValidUser(email, senha))
            {
                FormsAuthentication.SetAuthCookie(email, false);

                return RedirectToAction("Index", "BarberControl");
            }

            return HttpNotFound();

        }

        private bool IsValidUser(string email, string senha)
        {
            return _login.LoginUsuario(email, senha);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home"); 
        }
    }
}