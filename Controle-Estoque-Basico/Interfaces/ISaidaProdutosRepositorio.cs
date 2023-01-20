using Controle_Estoque_Basico.Models;
using System.Threading.Tasks;

namespace Controle_Estoque_Basico.Interfaces
{
    public interface ISaidaProdutosRepositorio
    {
        Task<SaidaProduto> Salvar(SaidaProduto _registro);
        Task<bool> Excluir(int Id);
    }
}
