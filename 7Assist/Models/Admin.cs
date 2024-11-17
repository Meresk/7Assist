using System;
using System.Collections.Generic;

namespace _7Assist.Models;

public partial class Admin
{
    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public int IdUser { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
