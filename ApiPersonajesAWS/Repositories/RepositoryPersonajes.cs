using ApiPersonajesAWS.Data;
using ApiPersonajesAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPersonajesAWS.Repositories
{
    public class RepositoryPersonajes
    {
        private PersonajesContext context;

        public RepositoryPersonajes(PersonajesContext context)
        {
            this.context = context;
        }

        public async Task<List<Personajes>> GetPersonajesAsync()
        {
            return await this.context.personajes.ToListAsync();
        }

        public async Task<Personajes> FindePersonajesAsync(int id)
        {
            return await this.context.personajes.FirstOrDefaultAsync(x => x.IdPersonaje == id);
        }

        private async Task<int> GetMaxIdPersonjeAsync()
        {
            return await this.context.personajes.MaxAsync(x=>x.IdPersonaje)+1;
        }

        public  async Task CreatePersonajesAsync(string nombre, string imagen)
        {
            Personajes per = new Personajes
            {
                IdPersonaje = await this.GetMaxIdPersonjeAsync(),
                Nombre = nombre,
                Imagen = imagen
            };
            this.context.personajes.Add(per);
            await this.context.SaveChangesAsync();
        }
    }
}
