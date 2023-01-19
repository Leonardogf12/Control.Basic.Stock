using Controle_Estoque_Basico.Data;
using Controle_Estoque_Basico.Interfaces;
using Controle_Estoque_Basico.Models;
using System;
using System.Threading.Tasks;

namespace Controle_Estoque_Basico.Repositorios
{
    public class CategoriasRepositorio : ICategoriasRepositorio
    {
        private readonly AppDbContext _context;

        public CategoriasRepositorio(AppDbContext context)
        {
            _context = context;
        }

        #region GETS

        #endregion

        #region POSTS

        public async Task<Categoria> Salvar(Categoria _registro)
        {

            if (_registro.CAT_ID == 0)
                _context.Categoria.Add(_registro);

            try
            {
                if (_registro.CAT_ID > 0)
                {
                    _registro.CAT_ISDELETED = true;
                }

                await _context.SaveChangesAsync();

                return _registro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Excluir(int Id)
        {

            Categoria registro = await _context.Categoria.FindAsync(Id);

            if (registro != null)
            {
                _context.Categoria.Remove(registro);

                try
                {
                    registro.CAT_ISDELETED = true;
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
