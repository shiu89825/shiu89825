using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Categorys { get; set; }

    public int? Order { get; set; }
}
