using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fastBarberTG.Models.dto
{
    public class BuscaCustumer
    {
        public HorariosMarcadosModel horario { get; set; }
        public Costumer costumer { get; set; }
        public Barber barber { get; set; }
    }
}