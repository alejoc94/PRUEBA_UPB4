using PRUEBA_UPB.COMUN;
using PRUEBA_UPB.DATOS.Models;
using PRUEBA_UPB.DATOS.Repositorio.Implementaciones;
using PRUEBA_UPB.DATOS.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace PRUEBA_UPB.API.Conf.Extensiones
{

    internal static class DataExtension
    {
        internal static IServiceCollection AgregarProveedorCon(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddDbContextPool<PruebaUpbContext>(options =>
                options
                       .UseNpgsql(appSettings.ConnectionStrings.DefaultConnection,
                       providerOptions =>
                       {
                           providerOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: new List<string> { "258", "47073" });
                       })

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