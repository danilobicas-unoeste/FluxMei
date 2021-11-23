using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LivroCaixa.Models
{
    [Table("TipoMovimento")]
    public class TipoMovimento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tipoid { get; set; }
        [StringLength(50)]
        [Required]
        [Display(Name ="Descrição(Tipo)")]
        public string descricao { get; set; }
        [StringLength(1)]
        [Display(Name ="Tipo (Receita/Despesa")]
        [Required]
        public string receitadespesa { get; set; }
        [ForeignKey("Movimento")]
        public virtual  ICollection<Movimento> Movimento { get; set; }
        [ForeignKey("Mei")]
        [Required]
        [Range(1, double.MaxValue, ErrorMessage ="Nenhum MEI está logado para associar este novo tipo de movimento")]
        public int IdMei { get; set; }
        
        public virtual Mei Mei { get; set; }
    }
}