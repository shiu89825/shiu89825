using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Member
{
    public int Id { get; set; }

    public string? Account { get; set; }

    public string? Passwd { get; set; }

    public string? Email { get; set; }

    public string? Name { get; set; }
}
