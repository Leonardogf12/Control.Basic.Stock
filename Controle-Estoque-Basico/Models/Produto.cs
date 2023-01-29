using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Controle_Estoque_Basico.Models
{
    public class Produto
    {       
        [Key]
        public int PRO_ID { get; set;}

        [Display(Name = "Produto")]
        [Required(ErrorMessage = "O campo Produto é obrigatório.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "O Produto deve ter no mínimo 5 e no máximo 50 caracteres.")]
        public string PRO_NOME { get; set; }

        [Display(Name = "Descrição")]        
        [StringLength(200, MinimumLength = 5, ErrorMessage = "A Descrição deve ter no mínimo 5 e no máximo 200 caracteres.")]
        public string PRO_DESCRICAO { get; set; }

        [Display(Name = "Quantidade UN")]
        [Column(TypeName = "decimal(18, 3)")]
        [Required(ErrorMessage = "O campo Quantidade é obrigatório.")]
        public decimal PRO_QUANTIDADE { get; set; }
       
        [Display(Name = "Entrada")]
        [DataType(DataType.Date)]
        public DateTime PRO_DATAENTRADA { get; set; }

        [Display(Name = "Validade")]
        [DataType(DataType.Date)]
        public DateTime PRO_VALIDADE { get; set; }

        [Display(Name = "Status")]
        public bool PRO_STATUS { get; set; }

        [Display(Name = "Estoque Mínimo")]
        [Column(TypeName = "decimal(18, 3)")]
        [Required(ErrorMessage = "O campo Estoque Mínimo é obrigatório.")]
        public decimal PRO_ESTOQUEMINIMO { get; set; }

        public int PRO_IDCATEGORIA { get; set; }

        public bool PRO_ISDELETED { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload")]       
        public string ImagemProdutoModel { get; set; }


        [ForeignKey("PRO_IDCATEGORIA")]
        public virtual Categoria Categoria { get; set; }
        
       
    }
}