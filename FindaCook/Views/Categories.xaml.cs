using FindaCook.ViewModels;

namespace FindaCook
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
