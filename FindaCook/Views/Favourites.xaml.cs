using FindaCook.ViewModels;

namespace FindaCook;

public partial class Favourites : ContentPage
{
	public Favourites()
	{
		InitializeComponent();
		BindingContext =new	 FavouriteViewModel();
	}
}
