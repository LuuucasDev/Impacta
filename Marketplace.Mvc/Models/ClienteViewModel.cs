using System;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Mvc.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression("^\\d{3}.\\d{3}.\\d{3}-\\d{2}$", ErrorMessage ="Digite o CPF no formato 123.456.789-10.")]
        public string Documento { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public Guid? GuidCartao { get; set; }
    }
}