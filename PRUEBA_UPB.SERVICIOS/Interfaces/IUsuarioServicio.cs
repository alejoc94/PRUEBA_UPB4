using PRUEBA_UPB.DATOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA_UPB.SERVICIOS.Interfaces
{
    public interface IUsuarioServicio
    {
        public Task<IEnumerable<Usuario>> ObtenerUsuarios();

        public Task<IEnumerable<Usuario>> ObtenerUsuarioPorId(int id);

        public Task GuardarUsuario();

        public Task BorrarUsuario(int id);
    }
}
