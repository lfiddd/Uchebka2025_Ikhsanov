using System;
using System.Collections.Generic;

namespace Uchebka2025_Ikhsanov.Data;

public partial class Student
{
    public int RegNumber { get; set; }

    public int? IdSpeciality { get; set; }

    public string? Fullname { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual Specialty? IdSpecialityNavigation { get; set; }

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();
}
