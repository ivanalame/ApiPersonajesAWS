using ApiPersonajesAWS.Data;
using ApiPersonajesAWS.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

//DELIMITER //
//create PROCEDURE UPDATEPERSONAJE(IN P_ID INT, IN NOMBRE NVARCHAR(50), IN IMAGEN NVARCHAR(255))
//BEGIN
//  UPDATE PERSONAJES
//  SET PERSONAJE = NOMBRE, IMAGEN = IMAGEN
//  WHERE IDPERSONAJE = P_ID;
//END //
//DELIMITER;

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

        public async Task UpdatePersonajeAsync(int id,string nombre, string imagen)
        {
            string sql = "call UPDATEPERSONAJE (@P_ID, @NOMBRE, @IMAGEN)";
            MySqlParameter pamId = new MySqlParameter("@P_ID", id);
            MySqlParameter pamNombre = new MySqlParameter("@NOMBRE", nombre);
            MySqlParameter pamImagen = new MySqlParameter("@IMAGEN", imagen);

           this.context.Database.ExecuteSqlRaw(sql, pamId, pamNombre, pamImagen);
        }
    }
}
