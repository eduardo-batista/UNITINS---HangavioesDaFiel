using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HangavioesDaFiel.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Nome")]
        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        public string Name { get; set; }
        [Required]
        [DisplayName("CPF")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "CPF Inválido.")]
        public string Cpf { get; set; }
        [Required]
        [DisplayName("Telefone")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "Telefone Inválido.")]
        public string Phone { get; set; }
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Status")]
        public Boolean Status { get; set; } = true;
    }
}
