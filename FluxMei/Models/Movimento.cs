using System;
using System.Collections.Generic;

namespace FluxMei.Models
{
    public partial class Movimento
    {
        [primarykey]
        public decimal IdMovimento { get; set; }
        public string Descicao { get; set; }
        public decimal Total { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string IdMei { get; set; }

        public Mei IdMeiNavigation { get; set; }
    }
}
