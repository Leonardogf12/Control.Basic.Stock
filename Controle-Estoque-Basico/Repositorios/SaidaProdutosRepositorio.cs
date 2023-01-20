using Controle_Estoque_Basico.Data;
using Controle_Estoque_Basico.Interfaces;
using Controle_Estoque_Basico.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controle_Estoque_Basico.Repositorios
{
    public class SaidaProdutosRepositorio : ISaidaProdutosRepositorio
    {
        private readonly AppDbContext _context;

        public SaidaProdutosRepositorio(AppDbContext context)
        {
            _context = context;
        }

        #region GETS
        public async Task<List<SaidaProduto>> BuscaSaidaProdutos()
        {
            return await _context.SaidaProduto.Where(x => x.SPRO_ISDELETED == false).ToListAsync();
        }
        #endregion

        #region POSTS

        public async Task<SaidaProduto> Salvar(SaidaProduto _registro)
        {

            try
            {
                if (_registro.SPRO_ID == 0)
                    _context.SaidaProduto.Add(_registro);

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

            SaidaProduto registro = await _context.SaidaProduto.FindAsync(Id);

            if (registro != null)
            {
                _context.SaidaProduto.Remove(registro);

                try
                {
                    registro.SPRO_ISDELETED = true;
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
