using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HangavioesDaFiel.Models
{
    public class Administrator : Person
    {
        [Required]
        [DisplayName("Senha")]
        public string Password { get; set; }
        public Administrator() { }
    }
}
