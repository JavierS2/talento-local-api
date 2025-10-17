using System;
using System.Collections.Generic;

namespace TalentoLocal.Models;

public partial class Postulacion
{
    public int Id { get; set; }

    public DateTime? CreateTime { get; set; }

    public string? Titulo { get; set; }

    public string? Descripcion { get; set; }
}
