using System.Collections.Generic;
using System.Linq;
using Neppo.Models;

namespace Neppo.Repositories.Interfaces
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
        List<Pessoa> Buscar(string campo, string valor, int pagina = 1, int itensPorPagina = 5);

        int GetTotalPaginas(int itensPorPagina);
    }
}
