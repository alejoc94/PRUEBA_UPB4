using PRUEBA_UPB.DATOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA_UPB.DATOS.Repositorio.Interfaces
{
    public interface IUsuarioRepositorio
    {
        public Task<IEnumerable<Usuario>> ObtenerUsuarios();

        public Task<IEnumerable<Usuario>> ObtenerUsuarioPorId(int id);

        public Task GuardarUsuario(Usuario usuario);

        public Task BorrarUsuario(int id);
    }
}
