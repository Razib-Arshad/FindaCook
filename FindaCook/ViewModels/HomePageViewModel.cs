using FindaCook.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Threading.Tasks;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using FindaCook.Models;
using FindaCook.Services;
using FindaCook.ViewModels;
using FindaCook.Views;

namespace FindaCook
{
    public class HomePageViewModel : ObservableObject
    {
        readonly ICookRespository _CookRepository = new CookService();
        private string _selectedCategory;
        private ObservableCollection<CookProfile> _cooks;

        public HomePageViewModel()
        {
            Categories = new ObservableCollection<string>
            {
                "Desi",
                "Chinese",
                "Fast Food",
                "Italian",
                "Continental",
                "Thai"

            };

            CategoryClickedCommand = new Command<string>(async (category) => await OnCategoryClickedAsync(category));
        }





        public ObservableCollection<string> Categories { get; }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        public ObservableCollection<CookProfile> Cooks
        {
            get => _cooks;
            set => SetProperty(ref _cooks, value);
        }

        public ICommand CategoryClickedCommand { get; set; }

        private async Task OnCategoryClickedAsync(string category)
        {
            SelectedCategory = category;
            try
            {
                // Call the API to get cooks by the selected category
                ICollection<CookProfile> result = await _CookRepository.GetCookByCategory(SelectedCategory);

                List<CookProfile> resultList = result.ToList();


                Cooks = new ObservableCollection<CookProfile>(resultList);
                await App.Current.MainPage.Navigation.PushAsync(new CookLists(Cooks));


            }
            catch (Exception ex)
            {

            }
        }
    }
}
