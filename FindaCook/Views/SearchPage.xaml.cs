using FindaCook.ViewModels;

namespace FindaCook.Views;

public partial class SearchPage : ContentPage
{
    public SearchPage()
    {
        InitializeComponent();
        BindingContext = new SearchPageViewModel();
    }
}
