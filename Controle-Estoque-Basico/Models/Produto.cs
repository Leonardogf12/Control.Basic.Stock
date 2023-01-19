using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Controle_Estoque_Basico.Models
{
    public class Produto
    {       
        [Key]
        public int PRO_ID { get; set;}

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "O Produto deve ter no mínimo 5 e no máximo 100 caracteres.")]
        public string PRO_NOME { get; set; }

        [Display(Name = "Descrição")]               
        public string PRO_DESCRICAO { get; set; }

        [Display(Name = "Quantidade UN", Description = "Digite um valor valido")]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal PRO_QUANTIDADE { get; set; }

        [Display(Name = "Entrada")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PRO_DATAENTRADA { get; set; }

        [Display(Name = "Validade")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PRO_VALIDADE { get; set; }

        [Display(Name = "Status")]
        public bool PRO_STATUS { get; set; } // true = VENDIDO, false = EM ESTOQUE

        public int PRO_IDCATEGORIA { get; set; }

        public bool PRO_ISDELETED { get; set; }

        [ForeignKey("PRO_IDCATEGORIA")]
        public virtual Categoria Categoria { get; set; }       
    }
}
