using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HangavioesDaFiel.Models
{
    public class Pilot : Person
    {
        [Required]
        [DisplayName("Aptidão")]
        public string Aptitude { get; set; }
        [Required]
        [DisplayName("Registro")]
        [MaxLength(6, ErrorMessage = "Máximo de 6 caracteres")]
        public string Registration { get; set; }
        public Pilot() { }
    }
}
