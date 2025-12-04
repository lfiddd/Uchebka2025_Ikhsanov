using System.ComponentModel;
using System.Runtime.CompilerServices;
using Uchebka2025_Ikhsanov.Data;

namespace Uchebka2025_Ikhsanov;

public class ProfileViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string prop = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    private Login User => VariableData.authuser;

    
    public string Fullname =>
        User.IdStudentNavigation?.Fullname ??
        User.IdEmpNavigation?.Fullname ??
        "Неизвестно";

    public string RoleName
    {
        get
        {
            if (User.IdStudentNavigation != null) return "Студент";
            if (User.IdEmpNavigation == null) return "Неизвестно";

            return User.IdEmpNavigation.PositionEmp switch
            {
                "1" => "Инженеры",
                "3" => "Преподаватель",
                "2" => "Зав. кафедрой",
                _   => "Сотрудник"
            };
        }
    }

    
    public string Login => User.Login1;
    public string Password => User.PasswordL;

    
    public bool IsStudent => User.IdStudentNavigation != null;
    public bool IsEmployee => User.IdEmpNavigation != null;

    
    public string Position => User.IdEmpNavigation?.PositionEmp switch
    {
        "1" => "Инженер",
        "3" => "Преподаватель",
        "2" => "Заведующий кафедрой",
        _ => "—"
    };

    public decimal? Salary => User.IdEmpNavigation?.Salary;
    public string Department => User.IdEmpNavigation?.IdDepartNavigation?.NameDepart ?? "—";

    
    public int RegNumber => User.IdStudentNavigation?.RegNumber ?? 0;
    public string Specialty => User.IdStudentNavigation?.IdSpecialityNavigation?.Direction ?? "—";
}