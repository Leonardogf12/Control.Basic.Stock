using System.ComponentModel.DataAnnotations;

namespace Controle_Estoque_Basico.Models
{
    public class Categoria
    {
        [Key]
        public int CAT_ID { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "A Categoria deve ter no mínimo 5 e no máximo 30 caracteres.")]
        public string CAT_NOME { get; set; }

        public bool CAT_ISDELETED { get; set; }

    }
}
