using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? PAccount { get; set; }

    public string? PName { get; set; }

    public string? Category { get; set; }

    public string? Img { get; set; }

    public int? Price { get; set; }

    public int? Stock { get; set; }

    public string? Contents { get; set; }
}
