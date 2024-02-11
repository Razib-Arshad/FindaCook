using FindaCook.Maui.ViewModels;
using FindaCook.ViewModels;
using Microsoft.Maui.Controls;

namespace FindaCook;

public partial class Personal_info : ContentPage
{
    public Personal_info()
    {
        InitializeComponent();
        BindingContext = new Personal_infoViewModel();

    }

}
