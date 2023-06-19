using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HangavioesDaFiel.Models
{
    public class Administrator : Person
    {
        [Required]
        [DisplayName("Senha")]
        [MinLength(8, ErrorMessage = "Mínimo de 8 caracteres.")]
        public string Password { get; set; }
        public Administrator() { }
    }
}
