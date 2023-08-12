using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PRUEBA_UPB.COMUN;
using PRUEBA_UPB.DATOS.Models;
using PRUEBA_UPB.DATOS.Repositorio.Interfaces;
using PRUEBA_UPB.DATOS.Utilitarios;
using PRUEBA_UPB.DATOS.Constantes;
using Npgsql;
using System.Reflection;

namespace PRUEBA_UPB.DATOS.Repositorio.Implementaciones
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AppSettings _appSettings;
        private readonly UtilitarioSQLComando _utilitarioSQLComando; 

        public UsuarioRepositorio(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _utilitarioSQLComando = new UtilitarioSQLComando(_appSettings);
        }
        
        public async Task BorrarUsuario(int id)
        {
            var sqlParametros = new NpgsqlParameter[]
               {
                    new NpgsqlParameter("p_id", id)
               };

            await _utilitarioSQLComando.StoredProcNonQueryAsync(Constantes.ProcAlmacenado.spBorrarUsuario, sqlParametros);
        }

        public async Task GuardarUsuario(Usuario usuario) 
        {
            
            Type usu = usuario.GetType();
            PropertyInfo[] Atributos = usu.GetProperties();
            var sqlParametros = new NpgsqlParameter[Atributos.Length];
            var cont = 0;

            foreach (var item in Atributos)
            {
                sqlParametros[cont] = new NpgsqlParameter("@p_"+item.Name.ToLower(), item.GetValue(usuario));
                cont++;
            }

            await _utilitarioSQLComando.StoredProcNonQueryAsync(Constantes.ProcAlmacenado.spGuardarUsuario, sqlParametros);

        }
    

        public async Task<IEnumerable<Usuario>>ObtenerUsuarios()
        {
            return await Obtener();
        }

        public async Task<IEnumerable<Usuario>>ObtenerUsuarioPorId(int id)
        {
            return await Obtener(id);
        }

        private async Task<IEnumerable<Usuario>> Obtener(int? id = null)
        {
            if (id == null)
            {
                var sqlParametros = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("p_id", DBNull.Value)
                };
                return await _utilitarioSQLComando.StoredProcQueryAsync<Usuario>(Constantes.ProcAlmacenado.spConsultarUsuariosPorId, sqlParametros);
            }
            else
            {
               var sqlParametros = new NpgsqlParameter[]
               {
                    new NpgsqlParameter("p_id", id)
               };
                return await _utilitarioSQLComando.StoredProcQueryAsync<Usuario>(Constantes.ProcAlmacenado.spConsultarUsuariosPorId, sqlParametros);
            }

            
        }
    }
}
