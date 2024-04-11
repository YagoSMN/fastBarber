using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fastBarberTG.Models.dto
{
    public class HorariosDisponiveis
    {
        public IEnumerable<HorariosOcupados> horariosOcupados { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime Data { get; set; }
    }
}