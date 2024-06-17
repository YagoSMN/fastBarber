using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace fastBarberTG.Models
{
    public class Costumer
    {
        public int Id { get; set; }
        public decimal Cpf { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNasc { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }

        public string CpfFormatado()
        {
            string cpfSemFormato = Cpf.ToString("00000000000"); // garante 11 dígitos

            // Aplica o regex para formatar o CPF
            string cpfFormatado = Regex.Replace(cpfSemFormato, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");

            return cpfFormatado;
        }
    }
}