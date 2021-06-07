using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly ProEventosContext _context;
        public PalestrantePersist(ProEventosContext context)
        {
            _context = context;
            //Adicionar no tracking de forma geral para todos os métodos, pois todos pertencem ao Palestrante
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            // No tracking pode ser feito tb incluindo .AsNoTracking() em cada método que precisar
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            //Buscar incluindo lote e redes sociais
            IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }

            //Ordenar a busca usando o id
            query = query.OrderBy(e => e.Id);
            
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            //Buscar incluindo lote e redes sociais
            IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }

            //Ordenar a busca usando o id
            query = query.OrderBy(p => p.Id).Where(p => p.Nome.ToLower().Contains(nome.ToLower()));
            
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false)
        {
            //Buscar incluindo lote e redes sociais
            IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }

            //Ordenar a busca usando o id
            query = query.OrderBy(p => p.Id).Where(p => p.Id == palestranteId);
            
            return await query.FirstOrDefaultAsync();
        }
    }
}