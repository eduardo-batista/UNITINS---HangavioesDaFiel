using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HangavioesDaFiel.Models
{
    public class Employee : Person
    {
        [Required]
        [DisplayName("Função")]
        public string Function { get; set; }

        public Employee() { }
    }
}
