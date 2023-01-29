using Controle_Estoque_Basico.Data;
using Controle_Estoque_Basico.Interfaces;
using Controle_Estoque_Basico.Models;
using System;
using System.Threading.Tasks;

namespace Controle_Estoque_Basico.Repositorios
{
    public class ProdutosRepositorio : IProdutosRepositorio
    {
        private readonly AppDbContext _context;

        public ProdutosRepositorio(AppDbContext context)
        {
            _context = context;
        }

        #region POSTS

        public async Task<Produto> Salvar(Produto _registro)
        {
            try
            {
                if (_registro.PRO_ID == 0)
                {
                    _context.Produto.Add(_registro);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.Update(_registro);
                    await _context.SaveChangesAsync();
                }

                return _registro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Excluir(int Id)
        {

            Produto registro = await _context.Produto.FindAsync(Id);

            if (registro != null)
            {
                _context.Produto.Remove(registro);

                try
                {
                    registro.PRO_ISDELETED = true;
                    await Salvar(registro);
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return false;
        }

        #endregion

    }
}
