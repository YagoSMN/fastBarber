using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace fastBarberTG.Models
{
    public static class Geral
    {
        public static DateTime DataAtual = DateTime.Now;

        public static string NomeDiaDaSemana()
        {
            DateTime dataAtual = DateTime.Now;
            string nomeDia = dataAtual.ToString("dddd", CultureInfo.GetCultureInfo("pt-BR")); // Usando cultura brasileira
            nomeDia = char.ToUpper(nomeDia[0]) + nomeDia.Substring(1); // Tornar a primeira letra maiúscula
            return nomeDia;
        }

        public static string Data()
        {
            return DataAtual.Date.ToString("dd/MM");
        }

        public static string HoraAtual()
        {
            return DataAtual.ToString("tt HH:mm");
        }

    }
}