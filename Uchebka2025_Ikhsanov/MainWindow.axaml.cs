using Avalonia.Controls;
using Shop;
using Uchebka2025_Ikhsanov.ControlPages;


namespace Uchebka2025_Ikhsanov;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        NavigationService.Initialize(MainControl);
        NavigationService.NavigateTo<Authorization>();
    }
}