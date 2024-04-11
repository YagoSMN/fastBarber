using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fastBarberTG.Models
{
    public class DayOfWeek
    {
        public int Id { get; set; }
        public string Nome_Dia { get; set; }
        public TimeSpan Horario_Inicio { get; set; }
        public TimeSpan Horario_AlmocoInicio { get; set; }
        public TimeSpan Horario_AlmocoFim { get; set; }
        public TimeSpan Horario_Fim { get; set; }
        public char Ind_Ativo { get; set; }
    }
}