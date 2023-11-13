using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using fastBarberTG.Models;

namespace fastBarberTG.Models
{
    public class HorariosMarcadosModel: Costumer
    {
        public int HorarioId { get; set; }
        public int Id_Cliente { get; set; }
        public DateTime DataCorte { get; set; }
        public int StatusCorte { get; set; }
        public int BarberId { get; set; }
        public string TempoCorte { get; set; }

        public string RetornoIdade()
        {
            DateTime dataApenas = DataNasc.Date;
            DateTime _dataAtual = Geral.DataAtual;

            int idade = _dataAtual.Year - DataNasc.Year;

            if (_dataAtual < DataNasc.AddYears(idade))
            {
                idade--;
            }

            return idade.ToString();
        }

        public string HoraCorte()
        {
            return DataCorte.ToShortTimeString();
        }

        public string DataCorteFormatado() => DataCorte.ToShortDateString();

        public string RetornaStatus()
        {
            if (StatusCorte == 2)
                return "yellow";

            if (StatusCorte == 1)
                return "green";

            if (StatusCorte == 3)
                return "red";

            return "";
        }

        public int RetornaHoraDiaInt() => int.Parse(DataCorte.Hour.ToString());
    }
}