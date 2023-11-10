using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fastBarberTG.Models;
using System.Data.SqlClient;

namespace fastBarberTG.Controllers
{
    [Route("cortes")]
    public class MarcarCorteController : Controller
    {
        private NewCostumerREPO _newCostumerREPO;

        public MarcarCorteController()
        {
            _newCostumerREPO = new NewCostumerREPO();
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
    }
}