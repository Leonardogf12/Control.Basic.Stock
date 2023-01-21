using Controle_Estoque_Basico.Models;
using System.Threading.Tasks;

namespace Controle_Estoque_Basico.Interfaces
{
    public interface IProdutosRepositorio
    {
        Task<Produto> Salvar(Produto _registro);
        Task<bool> Excluir(int Id);
    }
}
