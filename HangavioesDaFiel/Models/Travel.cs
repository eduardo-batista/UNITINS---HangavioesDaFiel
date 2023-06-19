using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HangavioesDaFiel.Models
{
    public class Travel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Data de saída")]
        public DateTime DepartureDate { get; set; }
        [Required]
        [DisplayName("Data de retorno")]
        public DateTime ReturnDate { get; set; }
        [AllowNull]
        [DisplayName("Equipe de Manutenção")]
        public int? TeamId { get; set; }
        [DisplayName("Equipe de Manutenção")]
        public virtual Team? Team { get; set; }
        [AllowNull]
        [DisplayName("Piloto")]
        public int? PilotId { get; set; }
        [DisplayName("Piloto")]
        public virtual Pilot? Pilot { get; set; }
        [AllowNull]
        [DisplayName("Co-Piloto")]
        public int? CopilotId { get; set; }
        [DisplayName("Co-Piloto")]
        public virtual Pilot? Copilot { get; set; }
        [Required]
        [DisplayName("Avião")]
        public int AirplaneId { get; set; }
        [DisplayName("Avião")]
        public virtual Airplane? Airplane { get; set; }
        public Travel() { }
    }
}
