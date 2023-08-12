using Microsoft.AspNetCore.Mvc;
using PRUEBA_UPB.DATOS.Models;
using PRUEBA_UPB.SERVICIOS.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PRUEBA_UPB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioDot
    {
        public int Id { get; set; }

        public string Nombres { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Pwd { get; set; } = null!;
    }

    public class UsuarioController : Controller
    {
        private readonly IUsuarioServicio _usuarioServicio;

        public UsuarioController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }
        // GET: api/<UsuarioController>
        [HttpGet]
        [Route("ObtenerUsuarios")]
        public async Task<object> ObtenerUsuarios()
        {
            return await _usuarioServicio.ObtenerUsuarios();
        }

        [HttpGet]
        [Route("ObtenerUsuarioPorId")]
        public async Task<object> ObtenerUsuarioPorId(int id)
        {
            return await _usuarioServicio.ObtenerUsuarioPorId(id);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        [Route("CrearUsuario")]
        public ActionResult AgregarUsuario([FromBody] UsuarioDot Usuario)
        {
            //await _usuarioServicio.GuardarUsuario<Usuario>(Usuario);
            return CreatedAtRoute("DefaultApi", Usuario.Id, Usuario); 
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete]
        [Route("BorrarUsuario")]
        public async void BorrarUsuario(int id)
        {
            await _usuarioServicio.BorrarUsuario(id);
        }
    }
}
