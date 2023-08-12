using System;
using Npgsql;
using System.Data;
using PRUEBA_UPB.COMUN;
using PRUEBA_UPB.DATOS.Utilitarios.Extensiones;

namespace PRUEBA_UPB.DATOS.Utilitarios
{
    internal class UtilitarioSQLComando
    {
        private readonly AppSettings _appSettings;

        internal UtilitarioSQLComando(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        internal async Task<IEnumerable<TResult>> StoredProcQueryAsync<TResult>(string storedProc, NpgsqlParameter[]? parameters) where TResult : class, new()
        {
            try
            {
                if (string.IsNullOrEmpty(storedProc))
                {
                    throw new ArgumentException("El parámetro storedProc es requerido.");
                }

                return await QueryAsync<TResult>(storedProc, parameters, CommandType.StoredProcedure);

            }
            catch (Exception)
            {
                throw;
            }
        }

        internal async Task StoredProcNonQueryAsync(string storedProc, NpgsqlParameter[]? parameters)
        {
            using (var con = new NpgsqlConnection(_appSettings.ConnectionStrings.DefaultConnection))
            {
                con.Open();
                NpgsqlTransaction transaction = con.BeginTransaction();
                try
                {
                    using (var cmd = new NpgsqlCommand(storedProc, con, transaction))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        
                        await cmd.ExecuteNonQueryAsync();
                        
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }

                con.Close();
            }

        }

        private async Task<IEnumerable<TResult>> QueryAsync<TResult>(string query, NpgsqlParameter[]? parameters, CommandType type) where TResult : class, new()
        {
            var items = new List<TResult>();
            using (var con = new NpgsqlConnection(_appSettings.ConnectionStrings.DefaultConnection))
            using (var cmd = new NpgsqlCommand(query, con))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                con.Open();
                using NpgsqlDataReader result = await cmd.ExecuteReaderAsync();
                while (await result.ReadAsync())
                {
                    items.Add(result.Parse<TResult>());
                }

                con.Close();
            }

            return items;

        }

    }
}
