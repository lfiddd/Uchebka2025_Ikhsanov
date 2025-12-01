using System.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Uchebka2025_Ikhsanov.Data;
using Application = Avalonia.Application;

namespace Uchebka2025_Ikhsanov;

public partial class App : Application
{
    public static AppDbContext DbContext { get; private set; } = new AppDbContext();
    public override void Initialize()
    {
        DbContext.Employees.ToList();
        DbContext.Students.ToList();
        DbContext.Logins.ToList();
        DbContext.Exams.ToList();
        DbContext.Disciplines.ToList();
        DbContext.Departments.ToList();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}