using LivroCaixa.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivroCaixa.Models
{
    public class RelatorioViewModel
    {
        public ICollection<Movimento> movimentos { get; set; }
        public decimal saldo { get; set; }
    }
}
