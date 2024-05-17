using ApiPersonajesAWS.Models;
using ApiPersonajesAWS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPersonajesAWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private RepositoryPersonajes repo;

        public PersonajesController(RepositoryPersonajes repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Personajes>>> GetPersonajes()
        {
            return await this.repo.GetPersonajesAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Personajes>> GetPeronajes(int id)
        {
            return await this.repo.FindePersonajesAsync(id);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePersonaje(Personajes personajes)
        {
            await this.repo.CreatePersonajesAsync(personajes.Nombre, personajes.Imagen);
            return Ok(); 
        }

        [HttpPut]
        public async Task<IActionResult>UpdatePersonaje(Personajes personaje)
        {
            await this.repo.UpdatePersonajeAsync(personaje.IdPersonaje,personaje.Nombre,personaje.Imagen);
            return Ok();
        }
    }
}
