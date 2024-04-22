using System.ComponentModel.DataAnnotations;

namespace ExpoCenter.Mvc.Models
{
    public class ParticipanteViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "CPF")]
        [StringLength(11 , MinimumLength = 11)]
        public string Cpf { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Nascimento")]
        public DateTime DataNascimento { get; set; }
    }
}
