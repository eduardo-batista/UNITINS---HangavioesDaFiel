using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HangavioesDaFiel.Models
{
    public class Team
    {
        public Team()
        {
        }

        public int Id { get; set; }

        [Required]
        [DisplayName("Identificador")]
        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        public string Identifier { get; set; }
        [Required]
        [DisplayName("Aptidão")]
        public string Aptitude { get; set; }
        [Required]
        public Boolean Status { get; set; } = true;
        [Required]
        [DisplayName("Líder de Equipe")]
        public int EmployeeId { get; set; }
        [DisplayName("Líder de Equipe")]
        public virtual Employee? Employee { get; set; }


    }
}
