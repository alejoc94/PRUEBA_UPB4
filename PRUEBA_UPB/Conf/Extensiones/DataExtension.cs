using PRUEBA_UPB.COMUN;
using PRUEBA_UPB.DATOS.Models;
using PRUEBA_UPB.DATOS.Repositorio.Implementaciones;
using PRUEBA_UPB.DATOS.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;


namespace PRUEBA_UPB.API.Conf.Extensiones
{

    internal static class DataExtension
    {
        internal static IServiceCollection AgregarProveedorCon(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddDbContextPool<PruebaUpbContext>(options =>
                options
                       .UseNpgsql(appSettings.ConnectionStrings.DefaultConnection)
                       .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            return services;
        }

        internal static IServiceCollection AgregarRepositorios(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            return services;
        }
    }
}