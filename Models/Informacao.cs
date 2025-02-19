using System;
using System.Collections.Generic;

namespace API_VariasBDs.Models;

public partial class Informacao
{
    public int IdInformacao { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Decricao { get; set; }

    public byte? Nivel { get; set; }

    public int IdUser { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
