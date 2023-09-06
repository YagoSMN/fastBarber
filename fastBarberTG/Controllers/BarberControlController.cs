using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fastBarberTG.Controllers
{
    public class BarberControlController : Controller
    {
        // GET: BarberControl
        public ActionResult Index() //Lista todos os cortes de cabelo para o dia atual.
        {
            return View();
        }
    }
}