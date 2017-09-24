using System;
using System.Collections.Generic;
using Neppo.Contexts;
using Neppo.Models;
using System.Linq;
using Neppo.Repositories.Interfaces;

namespace Neppo.Repositories
{
    public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(Context context) : base(context)
        {}

        public List<Pessoa> Buscar(string campo, string valor, int pagina = 1, int itensPorPagina = 1000)
        {
            try {
                return _context.pessoa
                    .Where(p => p.GetType().GetProperty(campo).GetValue(p).ToString().Contains(valor))
                    .OrderBy(p => p.id)
                    .Skip((pagina - 1) * itensPorPagina)
                    .Take(itensPorPagina)
                    .ToList();

            } catch (Exception e) {
                return new List<Pessoa>();
            }
        }

        public int GetTotalPaginas(int itensPorPagina) {
            double paginas = _context.pessoa.Count() / Convert.ToDouble(itensPorPagina);
            return Convert.ToInt32(Math.Ceiling(paginas));
        }

        override public IQueryable<Pessoa> GetAll(int pagina = 1, int itensPorPagina = 1000)
        {
            return _context.pessoa.OrderBy(p => p.id).Skip((pagina - 1) * itensPorPagina).Take(itensPorPagina);
        }
    }
}
