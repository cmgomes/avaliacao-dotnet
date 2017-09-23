using Neppo.Contexts;
using Neppo.Services.Interfaces;

namespace Neppo.Services
{
    public class InitializeDBService : IInitializeDBService
    {
        private readonly Context _contexto;

        public InitializeDBService(Context contexto) {
            _contexto = contexto;
        }

        public void InicializeDB() {
            //this._contexto.Database.EnsureCreated();
        }
    }
}
