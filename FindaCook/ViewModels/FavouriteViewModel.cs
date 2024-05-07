using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FindaCook.Models; // Assuming FavouriteCookDetails is here
using FindaCook.Services; // Assuming CookService is here
using System.Windows.Input;

namespace FindaCook.ViewModels
{
    public class FavouriteViewModel : ObservableObject
    {
        private ObservableCollection<FavouriteCookDetails> _favouriteCooks;

        private readonly ICookRespository _cookRepository=new CookService(); // Using dependency injection or direct instantiation

        public ICommand LoadFavouritesCommand { get; }

        public FavouriteViewModel()
        {
            

            FavouriteCooks = new ObservableCollection<FavouriteCookDetails>();
            LoadFavouritesCommand = new AsyncRelayCommand(LoadFavouritesAsync);

            // Automatically load favorites on initialization
            LoadFavouritesAsync().ConfigureAwait(false);
        }

        

        public ObservableCollection<FavouriteCookDetails> FavouriteCooks
        {
            get => _favouriteCooks;
            set => SetProperty(ref _favouriteCooks, value);
        }

        // Asynchronous method to load favorites
        public async Task LoadFavouritesAsync()
        {
            try
            {


                var favourites = await _cookRepository.getFavourites(); // Assume returns ICollection<FavouriteCookDetails>


                FavouriteCooks.Clear(); // Clear existing items
                foreach (var cook in favourites)
                {
                    FavouriteCooks.Add(cook);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., logging, user notification, etc.)
                Console.WriteLine($"Error loading favourites: {ex.Message}");
            }
        }

        //// Command to remove a favourite
        //public ICommand RemoveFavouriteCommand => new AsyncRelayCommand<FavouriteCookDetails>(RemoveFavouriteAsync);

        //// Asynchronous method to remove a favourite cook
        //public async Task RemoveFavouriteAsync(FavouriteCookDetails cookProfile)
        //{
        //    try
        //    {
        //        var result = await _cookRepository.RemoveFavourite(cookProfile.Id);

        //        if (result)
        //        {
        //            FavouriteCooks.Remove(cookProfile);
        //        }
        //        else
        //        {
        //            await App.Current.MainPage.DisplayAlert("Error", "Failed to remove favourite.", "OK");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error removing favourite: {ex.Message}");
        //    }
        //}
    }
}
