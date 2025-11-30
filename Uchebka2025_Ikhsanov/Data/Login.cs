using System;
using System.Collections.Generic;

namespace Uchebka2025_Ikhsanov.Data;

public partial class Login
{
    public int IdLogin { get; set; }

    public string Login1 { get; set; } = null!;

    public string LPassword { get; set; } = null!;

    public int? IdEmployee { get; set; }

    public virtual Employee? IdEmployeeNavigation { get; set; }
}
