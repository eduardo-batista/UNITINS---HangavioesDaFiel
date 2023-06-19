using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HangavioesDaFiel.Models
{
    public class Airplane
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Identificador")]
        public string Identifier { get; set; }
        [Required]
        [DisplayName("Classificação por motor")]
        public string ClassMotor { get; set; }
        [Required]
        [DisplayName("Classificação por altura")]
        public string ClassHeight { get; set; }
        [Required]
        [DisplayName("Classificação por porte")]
        public string ClassSize { get; set; }
        [Required]
        [DisplayName("Em viagem")]
        public Boolean Traveling { get; set; } = false;
        [Required]
        [DisplayName("Status")]
        public Boolean Status { get; set; } = true;

        public Airplane() { }
    }
}
