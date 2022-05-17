using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivroCaixa.ViewModels
{
    public class MeiViewModel
    {
        public int IdMei { get; set; }
        [StringLength(100)]
        [Display(Name = "Login")]
        public string Login { get; set; }
        public string Senha { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Empresa")]
        public string NomeEmpresa { get; set; }
        
        [StringLength(100)]
        [Display(Name = "Endereço")]
        public string Logradouto { get; set; }
        [StringLength(100)]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }
        [StringLength(100)]
        [Display(Name = "CEP")]
        public string Cep { get; set; }
        [Required]
        [StringLength(14)]
        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Proprietário")]
        public string NomeProprietario { get; set; }
        [Display(Name ="Fone")]
        [StringLength(15)]
        public string Telefone { get; set; }        
    }
}
