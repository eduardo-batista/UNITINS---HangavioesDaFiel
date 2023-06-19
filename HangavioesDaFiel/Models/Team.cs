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
        public string Identifier { get; set; }
        [Required]
        [DisplayName("Aptidão")]
        public string Aptitude { get; set; }
        [Required]
        public Boolean Status { get; set; } = true;
        [Required]
        [DisplayName("Líder de Equipe")]
        public int EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }


    }
}
