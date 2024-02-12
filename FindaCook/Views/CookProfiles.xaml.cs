using FindaCook.Models;
using FindaCook.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FindaCook.Views
{
    public partial class CookProfiles : ContentPage
    {
        public CookProfileViewModel CookProfileViewModel { get; set; }

        public CookProfiles(CookProfile cookProfile)
        {
            InitializeComponent();
            CookProfileViewModel = new CookProfileViewModel(cookProfile);
            BindingContext = CookProfileViewModel;
        }
    }
}