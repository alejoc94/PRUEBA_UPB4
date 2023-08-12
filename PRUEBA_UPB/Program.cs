using PRUEBA_UPB.API.Conf.Extensiones;
using PRUEBA_UPB.COMUN;

var builder = WebApplication.CreateBuilder(args);
var appSettings = new AppSettings();
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
configuration.Bind(appSettings);
services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

//Se carga el .ini y se inicializa conexion
services.Configure<AppSettings>(configuration);
services.AgregarProveedorCon(appSettings);
services.AgregarRepositorios();
services.AgregarServicios();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:3000");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
}
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
