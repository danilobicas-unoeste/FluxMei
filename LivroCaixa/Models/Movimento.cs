using Google.Cloud.Firestore;
using LivroCaixa.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivroCaixa.Models
{
    [FirestoreData]
    [Table("Movimento")]
    public partial class Movimento: IFirebaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [FirestoreProperty]
        public int IdMovimento { get; set; }
        [Display(Name = "Descrição")]
        [FirestoreProperty]
        public string Descicao { get; set; }
        [FirestoreProperty]
        public decimal Total { get; set; }
        [Display(Name = "Data")]
        [FirestoreProperty]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime Data { get; set; }
        //        [DataType(DataType.Currency)]
        //      [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [FirestoreProperty]
        [NumeroBrasil(DecimalRequerido =true,ErrorMessage ="Valor inválido",PontoMilharPermitido =true)]
        public decimal Valor { get; set; }
        [ForeignKey("TipoMovimento")]
        [FirestoreProperty]
        public int TipoMovimentoId { get; set; }
        
        public virtual TipoMovimento TipoMovimento { get; set; }
        [ForeignKey("Mei")]
        [Required]
        [Range(double.Epsilon, double.MaxValue,ErrorMessage ="Nenhum Mei está logado para associar este movimento")]
        [FirestoreProperty]
        public int IdMei { get; set; }
        [FirestoreProperty]
        public virtual Mei Mei { get; set; }
        /// <summary>
        /// usuário que fez o lançamento...
        /// </summary>
        [FirestoreProperty]
        public string userName { get; set; }
        [FirestoreProperty]
        public string Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
