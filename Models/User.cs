using System;
using System.Collections.Generic;

namespace API_VariasBDs.Models;

public partial class User
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public DateOnly Data { get; set; }

    public string? Token { get; set; }

    public virtual ICollection<Informacao> Informacaos { get; set; } = new List<Informacao>();
}
