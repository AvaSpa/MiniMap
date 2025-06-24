using MiniMap.ViewModels;

namespace MiniMap.Views;

public partial class MainView : ContentPage
{
    public MainView(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is MainViewModel vm)
            _ = SafeOnAppearingAsync(vm);
    }

    private async Task SafeOnAppearingAsync(MainViewModel vm)
    {
        try
        {
            await vm.OnAppearing();
        }
        catch
        {
        }
    }
}