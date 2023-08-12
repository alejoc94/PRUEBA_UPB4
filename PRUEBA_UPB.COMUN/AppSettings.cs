namespace PRUEBA_UPB.COMUN
{
    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; } = default!;
    }

    public class AppSettings
    {
        public string AllowedHosts { get; set; } = default!;

        public ConnectionStrings ConnectionStrings { get; set; } = default!;
    }
}