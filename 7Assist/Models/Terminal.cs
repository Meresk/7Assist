using System;
using System.Collections.Generic;

namespace _7Assist.Models;

public partial class Terminal
{
    public string Address { get; set; } = null!;

    public int IdUser { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
