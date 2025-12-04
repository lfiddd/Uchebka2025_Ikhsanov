using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Shop;
using Uchebka2025_Ikhsanov.Data;
using Uchebka2025_Ikhsanov.Views;

namespace Uchebka2025_Ikhsanov.ControlPages;

public partial class StudentsList : UserControl
{
    private List<Student> _allStudents = new();
    private List<Specialty> _specialities = new();
    public StudentsList()
    {
        InitializeComponent();
        if (VariableData.authuser.IdEmpNavigation == null || VariableData.authuser.IdStudentNavigation != null)
        {
            StudBtn.IsVisible = false;
            TeachBtn.IsVisible = false;
            HeadBtn.IsVisible = false;
        }
        
        else if (VariableData.authuser.IdEmpNavigation != null &&
                 int.Parse(VariableData.authuser.IdEmpNavigation.PositionEmp) == 3)
        {
            TeachBtn.IsVisible = false;
            HeadBtn.IsVisible = false;
        }
        else if (VariableData.authuser.IdEmpNavigation != null &&
                 int.Parse(VariableData.authuser.IdEmpNavigation.PositionEmp) == 2)
        {
            HeadBtn.IsVisible = false;
        }

        LoadAllData();
    }

    private async void LoadAllData()
    {
        var students = await App.DbContext.Students
            .Include(s => s.IdSpecialityNavigation)
            .ToListAsync();

        _allStudents = students;
        StudentsDataGrid.ItemsSource = _allStudents;

        _specialities = await App.DbContext.Specialties.ToListAsync();
        SpecialityFilter.Items.Clear();
        SpecialityFilter.Items.Add(new ComboBoxItem { Content = "Все специальности", Tag = "" });
        foreach (var spec in _specialities)
        {
            SpecialityFilter.Items.Add(new ComboBoxItem 
            { 
                Content = spec.Direction, 
                Tag = spec.IdSpecialty 
            });
        }

        ApplyFilter();
    }

    private void MainPageEmployees(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<MainPageEmp>();
    }

    private void DisciplineList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<DisciplineList>();
    }

    private void ExamsList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<ExamList>();
    }

    private void Leave(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<Authorization>();
    }

    private void TeachList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<TeachersList>();
    }

    private void DepartHeadLists(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<DepartHeadList>();
    }

    private void StudList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<StudentsList>();
    }

    private async void AddStudent_Click(object? sender, RoutedEventArgs e)
    {
        VariableData.selectUser = null;
        
        var parent = this.VisualRoot as Window;
        var addnewTeacher = new CreateAndChangeStudents();
        
        await addnewTeacher.ShowDialog(parent);
    }

    private void DeleteStudent_Click(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void ApplyFilter()
    {
        var search = SearchBox.Text?.ToLower() ?? "";
        var selectedSpec = SpecialityFilter.SelectedItem as ComboBoxItem;
        var specId = selectedSpec?.Tag as int?;

        var filtered = _allStudents.Where(s =>
        {
            bool matchesSearch = string.IsNullOrEmpty(search) ||
                                 (s.Fullname?.ToLower().Contains(search) == true);
            bool matchesSpec = !specId.HasValue || s.IdSpeciality == specId.Value;
            return matchesSearch && matchesSpec;
        }).ToList();

        StudentsDataGrid.ItemsSource = filtered;
    }

    private void SearchBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyFilter();
    }

    private void SpecialityFilter_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ApplyFilter();
    }
}