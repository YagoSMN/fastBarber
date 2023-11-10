using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fastBarberTG.Models
{
    public class Costumer
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNasc { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
    }
}