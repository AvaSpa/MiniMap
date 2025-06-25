using MiniMap.ViewModels;

namespace MiniMap.Views;

public partial class NavigationView : ContentPage
{
    public NavigationView(NavigationViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}