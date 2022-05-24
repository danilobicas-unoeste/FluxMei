using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LivroCaixa.Models
{
    [FirestoreData]
    [Table("TipoMovimento")]
    public class TipoMovimento:IFirebaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [FirestoreProperty]
        public int tipoid { get; set; }
        [StringLength(50)]
        [Required]
        [FirestoreProperty]
        [Display(Name ="Descrição(Tipo)")]
        public string descricao { get; set; }
        [StringLength(1)]
        [FirestoreProperty]
        [Display(Name ="Tipo (Receita/Despesa")]
        [Required]
        public string receitadespesa { get; set; }
        [ForeignKey("Movimento")]
        [FirestoreProperty]
        public virtual  ICollection<Movimento> Movimento { get; set; }
        [ForeignKey("Mei")]
        [Required]
        [Range(1, double.MaxValue, ErrorMessage ="Nenhum MEI está logado para associar este novo tipo de movimento")]
        [FirestoreProperty]
        public int IdMei { get; set; }
        [FirestoreProperty]
        public virtual Mei Mei { get; set; }
        [FirestoreProperty]
        public string Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}