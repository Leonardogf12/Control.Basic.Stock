using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Controle_Estoque_Basico.Models
{
    public class SaidaProduto
    {
        [Key]
        public int SPRO_ID { get; set; }

        [Display(Name = "Produto")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "O Produto deve ter no mínimo 5 e no máximo 100 caracteres.")]
        public string SPRO_NOME { get; set; }

        [Display(Name = "Descrição")]
        public string SPRO_DESCRICAO { get; set; }

        [Display(Name = "Quantidade UN", Description = "Digite um valor valido")]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal SPRO_QUANTIDADE { get; set; }

        [Display(Name = "Saída")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime SPRO_DATASAIDA { get; set; }
        
        public int SPRO_IDPRODUTO { get; set; }

        public int SPRO_IDCATEGORIA { get; set; }

        public bool SPRO_ISDELETED { get; set; }

        [ForeignKey("SPRO_IDPRODUTO")]
        public virtual Produto Produto { get; set; }

        [ForeignKey("SPRO_IDCATEGORIA")]
        public virtual Categoria Categoria { get; set; }

    }
}
