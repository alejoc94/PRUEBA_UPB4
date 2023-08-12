using Microsoft.AspNetCore.Mvc;
using PRUEBA_UPB.DATOS.Models;
using PRUEBA_UPB.SERVICIOS.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PRUEBA_UPB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioDot : Usuario
    {
    }

    public class UsuarioController : Controller
    {
        private readonly IUsuarioServicio _usuarioServicio;

        public UsuarioController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
            //UsuarioDot user = new UsuarioDot();
        }
        // GET: api/<UsuarioController>
        [HttpGet]
        [Route("ObtenerUsuarios")]
        public async Task<object> ObtenerUsuarios()
        {
            return await _usuarioServicio.ObtenerUsuarios();
        }

        [HttpGet("{id}", Name = "ObtenerUsuarioPorId")]
        public async Task<object> ObtenerUsuarioPorId(int id)
        {
            return await _usuarioServicio.ObtenerUsuarioPorId(id);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        [Route("CrearUsuario")]
        public ActionResult AgregarUsuario([FromBody]UsuarioDot Usuario)
        {
            _usuarioServicio.GuardarUsuario(Usuario);
            return CreatedAtRoute("ObtenerUsuarioPorId", new { id = Usuario.Id }, Usuario);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut]
        [Route("ActualizarUsuario")]
        public ActionResult ActualizarUsuario([FromBody] UsuarioDot Usuario)
        {
            _usuarioServicio.GuardarUsuario(Usuario);
            return Ok(Usuario.Id);
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}", Name = "BorrarUsuario")]
        public ActionResult BorrarUsuario(int id)
        {
            _usuarioServicio.BorrarUsuario(id);
            return Ok(id);
        }
    }
}
