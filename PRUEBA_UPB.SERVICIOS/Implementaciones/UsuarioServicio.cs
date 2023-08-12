using PRUEBA_UPB.DATOS.Models;
using PRUEBA_UPB.DATOS.Repositorio.Interfaces;
using PRUEBA_UPB.SERVICIOS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA_UPB.SERVICIOS.Implementaciones
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioServicio(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuarios()
        {
            return await _usuarioRepositorio.ObtenerUsuarios();
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuarioPorId(int id)
        {
            return await _usuarioRepositorio.ObtenerUsuarioPorId(id);
        }

        public async Task GuardarUsuario(Usuario usuario)
        {
            await _usuarioRepositorio.GuardarUsuario(usuario);
        }

        public async Task BorrarUsuario(int id)
        {
            await _usuarioRepositorio.BorrarUsuario(id);
        }
       
    }
}
