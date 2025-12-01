using System;
using System.Collections.Generic;

namespace Uchebka2025_Ikhsanov.Data;

public partial class Login
{
    public int IdLogin { get; set; }

    public string Login1 { get; set; } = null!;

    public string PasswordL { get; set; } = null!;

    public int? IdStudent { get; set; }

    public int? IdEmp { get; set; }

    public virtual Employee? IdEmpNavigation { get; set; }

    public virtual Student? IdStudentNavigation { get; set; }
}
