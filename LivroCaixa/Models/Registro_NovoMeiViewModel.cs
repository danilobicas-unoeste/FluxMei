using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LivroCaixa.Models
{
    public class Registro_NovoMeiViewModel
    {
        public int IdMei { get; set; }
        [Required]
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
        [Required]
        [StringLength(14)]
        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Proprietário")]
        public string NomeProprietario { get; set; }
        [Display(Name = "Fone")]
        [StringLength(15)]
        public string Telefone { get; set; }

        //aqui é a parte do login
        [Required]
        [EmailAddress]
        [Display(Name = "Nome do Responsável pelo MEI")]
        public string Nome { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}