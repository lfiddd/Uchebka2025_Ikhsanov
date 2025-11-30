using System;
using System.Collections.Generic;

namespace Uchebka2025_Ikhsanov.Data;

public partial class LoginStud
{
    public int IdLoginStud { get; set; }

    public string Login { get; set; } = null!;

    public string LPassword { get; set; } = null!;

    public int? IdStudent { get; set; }

    public virtual Student? IdStudentNavigation { get; set; }
}
