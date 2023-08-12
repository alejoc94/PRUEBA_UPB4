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

        public async Task GuardarUsuario() 
        {
           /* var result = new T();
            var properties = typeof(T).GetProperties();
           */
            //var sqlParametros = new NpgsqlParameter[typeof(T).GetProperties().Count()];
            var sqlParametros = new NpgsqlParameter[1];
            var cont = 0;
           /*
            foreach (var property in properties)
            {
                sqlParametros[cont] = new NpgsqlParameter("@p_" + property.Name.ToLower(), property.GetValue(1));
                cont++;
                //property.GetValue.
            }
           */
            await _utilitarioSQLComando.StoredProcNonQueryAsync(Constantes.ProcAlmacenado.spGuardarUsuario, sqlParametros);
            /*
            var sqlParametros = new NpgsqlParameter[]
              {
                    new NpgsqlParameter("@p_id", id),
                    new NpgsqlParameter("@p_nombre", id),
                    new NpgsqlParameter("@p_apellidos", id),
                    new NpgsqlParameter("@p_email", id),
                    new NpgsqlParameter("@p_username", id),
                    new NpgsqlParameter("@p_pdw", id)
              };
        */
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
