using System.ComponentModel.DataAnnotations;

namespace ExpoCenter.Mvc.Models
{
    public class EventoGridViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Data { get; set; }

        public string? Local { get; set; }

        [Display(Name = "Preço")]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }

        public bool Selecionado { get; set; }
    }
}