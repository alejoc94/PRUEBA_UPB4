using PRUEBA_UPB.DATOS.Repositorio.Implementaciones;
using PRUEBA_UPB.DATOS.Repositorio.Interfaces;
using PRUEBA_UPB.SERVICIOS.Interfaces;
using PRUEBA_UPB.SERVICIOS.Implementaciones;

namespace PRUEBA_UPB.API.Conf.Extensiones
{
    internal static class ServicioExtension
    {
        internal static IServiceCollection AgregarServicios(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioServicio, UsuarioServicio>();
            return services;
        }
    }
}
