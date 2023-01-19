using Controle_Estoque_Basico.Data;
using Controle_Estoque_Basico.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controle_Estoque_Basico.Services
{
    public class CategoriasService
    {
        public readonly AppDbContext _context;

        public CategoriasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> BuscaCategorias()
        {
            return await _context.Categoria.Where(x => x.CAT_ISDELETED == false).ToListAsync();
        }
    }
}
