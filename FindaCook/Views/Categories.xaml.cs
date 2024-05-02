using FindaCook.ViewModels;

namespace FindaCook.Views
{
    public partial class Categories : ContentPage
    {
        public Categories()
        {
            InitializeComponent();
           
            BindingContext = new CategoriesViewModel();
        }
    }
}
