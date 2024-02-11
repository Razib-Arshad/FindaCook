using FindaCook.Models;
using FindaCook.ViewModels;
using System.Collections.ObjectModel;

namespace FindaCook.Views;

public partial class CookLists : ContentPage
{
    public CookLists(ObservableCollection<CookProfile> cooks)
    {
        InitializeComponent();
        BindingContext = new CookListViewModel(cooks);
    }
}
