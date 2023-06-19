using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HangavioesDaFiel.Models
{
    public class Employee : Person
    {
        [Required]
        [DisplayName("Função")]
        [MaxLength(30, ErrorMessage = "Máximo de 30 caracteres")]
        public string Function { get; set; }

        public Employee() { }
    }
}
