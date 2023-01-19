using Controle_Estoque_Basico.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controle_Estoque_Basico.Interfaces
{
    public interface ICategoriasRepositorio
    {
        Task<List<Categoria>> BuscaCategorias();
        Task<Categoria> Salvar(Categoria _registro);
        Task<bool> Excluir(int Id);
        
    }
}
