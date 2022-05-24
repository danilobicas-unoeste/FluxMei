using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivroCaixa.Models
{
    [FirestoreData]
    [Table("Mei")]
    public partial class Mei: IFirebaseEntity
    {
        public Mei()
        {
            Movimento = new HashSet<Movimento>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        [FirestoreProperty]
        public int IdMei { get; set; }
        [StringLength(100)]
        [Display(Name = "Login")]
        [FirestoreProperty]
        public string Login { get; set; }
        [FirestoreProperty]
        public string Senha { get; set; }
        [Required]
        [StringLength(100)]
        [FirestoreProperty]
        [Display(Name = "Empresa")]
        public string NomeEmpresa { get; set; }
        
        [StringLength(100)]
        [FirestoreProperty]
        [Display(Name = "Endereço")]
        public string Logradouto { get; set; }
        [StringLength(100)]
        [FirestoreProperty]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }
        [Required]
        [FirestoreProperty]
        [StringLength(14)]
        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }
        [Required]
        [FirestoreProperty]
        [StringLength(100)]
        [Display(Name = "Proprietário")]
        public string NomeProprietario { get; set; }
        [Display(Name ="Fone")]
        [StringLength(15)]
        [FirestoreProperty]
        public string Telefone { get; set; }
        [ForeignKey("Movimento")]
        public ICollection<Movimento> Movimento { get; set; }
        [ForeignKey("TipoMovimento")]
        public virtual ICollection<TipoMovimento> TipoMovimento { get; set; }
        [FirestoreProperty]
        public string Id { get; set; }
    }
}
