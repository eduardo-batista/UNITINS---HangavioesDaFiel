using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HangavioesDaFiel.Models
{
    public class Garage
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Identificador")]
        public string Identifier { get; set; }
        [Required]
        [DisplayName("Altura")]
        public double Width { get; set; }
        [Required]
        [DisplayName("Largura")]
        public double Height { get; set; }
        [Required]
        [DisplayName("Comprimento")]
        public double Length { get; set; }
        [Required]
        [DisplayName("Capacidade de porte")]
        public string Capacity { get; set; }
        [Required]
        [DisplayName("Status")]
        public Boolean Status { get; set; } = true;
        [AllowNull]
        [DisplayName("Ocupada por")]
        public int? AirplaneId { get; set; }
        [DisplayName("Ocupada por")]
        public virtual Airplane? Airplane { get; set; }

        public Garage() { }

        public Garage(int id, string identifier, double width, double height, double length, string capacity, bool status, int? airplaneId)
        {
            Id = id;
            Identifier = identifier;
            Width = width;
            Height = height;
            Length = length;
            Capacity = capacity;
            Status = status;
            AirplaneId = airplaneId;
        }
    }
}
