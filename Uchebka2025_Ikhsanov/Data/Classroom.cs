using System;
using System.Collections.Generic;

namespace Uchebka2025_Ikhsanov.Data;

public partial class Classroom
{
    public int IdClass { get; set; }

    public string? ClassRoom { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
