using MiniMap.ViewModels;

namespace MiniMap.Views;

public partial class MainView : ContentPage
{
    public MainView(MainViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}