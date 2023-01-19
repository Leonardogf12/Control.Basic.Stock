using Controle_Estoque_Basico.Data;
using Controle_Estoque_Basico.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Controle_Estoque_Basico.ViewModels
{
    public class ProdutosViewModel
    {
        public Produto Produto { get; set; }        
        public Categoria Categoria { get; set; }        

        [DisplayName("Categorias")]
        public ICollection<Categoria> Categorias { get; set; }
        
    }
}
