using LivroCaixa.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivroCaixa.Models
{
    [Table("Movimento")]
    public partial class Movimento
    {
        [Key]
        public decimal IdMovimento { get; set; }
        public string Descicao { get; set; }
        public decimal Total { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        [ForeignKey("TipoMovimento")]
        public int TipoMovimentoId { get; set; }
        
        public virtual TipoMovimento TipoMovimento { get; set; }
        [ForeignKey("Mei")]
        public int IdMei { get; set; }
        public virtual Mei Mei { get; set; }
        /// <summary>
        /// usuário que fez o lançamento...
        /// </summary>
        public string userName { get; set; }
    }
}
