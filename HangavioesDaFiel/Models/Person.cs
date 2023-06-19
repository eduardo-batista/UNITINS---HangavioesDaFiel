using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HangavioesDaFiel.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Nome")]
        public string Name { get; set; }
        [Required]
        [DisplayName("CPF")]
        public string Cpf { get; set; }
        [Required]
        [DisplayName("Telefone")]
        public string Phone { get; set; }
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Status")]
        public Boolean Status { get; set; } = true;
    }
}
