using Microsoft.Maui.Controls;
using FindaCook.ViewModels;
using FindaCook.Services;

namespace FindaCook.Views
{
    public partial class DashboardPage : ContentPage
    {
        private DashboardViewModel _viewModel;

        public DashboardPage()
        {
            InitializeComponent();
            _viewModel = new DashboardViewModel(new CookService());  // Assuming CookService implements ICookRespository
            BindingContext = _viewModel;
            this.Appearing += DashboardPage_Appearing;
        }

        private async void DashboardPage_Appearing(object sender, EventArgs e)
        {
            if (_viewModel.LoadDataCommand.CanExecute(null))
            {
                _viewModel.LoadDataCommand.Execute(null);
            }
        }
    }
}
