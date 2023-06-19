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
        public string Registration { get; set; }
        public Pilot() { }
    }
}
