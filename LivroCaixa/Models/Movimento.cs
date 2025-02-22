﻿using LivroCaixa.Models;
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMovimento { get; set; }
        [Display(Name = "Descrição")]
        public string Descicao { get; set; }
        public decimal Total { get; set; }
        [Display(Name = "Data")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime Data { get; set; }
//        [DataType(DataType.Currency)]
  //      [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [NumeroBrasil(DecimalRequerido =true,ErrorMessage ="Valor inválido",PontoMilharPermitido =true)]
        public decimal Valor { get; set; }
        [ForeignKey("TipoMovimento")]
        public int TipoMovimentoId { get; set; }
        
        public virtual TipoMovimento TipoMovimento { get; set; }
        [ForeignKey("Mei")]
        [Required]
        [Range(double.Epsilon, double.MaxValue,ErrorMessage ="Nenhum Mei está logado para associar este movimento")]
        public int IdMei { get; set; }
        public virtual Mei Mei { get; set; }
        /// <summary>
        /// usuário que fez o lançamento...
        /// </summary>
        public string userName { get; set; }
    }
}
