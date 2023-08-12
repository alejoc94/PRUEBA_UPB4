using System;
using System.Collections.Generic;

namespace PRUEBA_UPB.DATOS.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Pwd { get; set; } = null!;
}
