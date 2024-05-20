using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using FindaCook.Models;
using FindaCook.Views;
using FindaCook.Services;
using Newtonsoft.Json;

namespace FindaCook.ViewModels
{
    public class SearchPageViewModel : INotifyPropertyChanged
    {
        private readonly ICookRespository _cookRepository;

        public ObservableCollection<CookProfile> Cooks { get; set; }

        private ObservableCollection<FilterOption> _filterOptions;
        public ObservableCollection<FilterOption> FilterOptions
        {
            get => _filterOptions;
            set
            {
                _filterOptions = value;
                OnPropertyChanged();
            }
        }

        private FilterOption _selectedFilter;
        public FilterOption SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public SearchPageViewModel()
        {
            _cookRepository = new CookService();

            FilterOptions = new ObservableCollection<FilterOption>
            {
                new FilterOption { Name = "Name" },
                new FilterOption { Name = "Category" },
                new FilterOption { Name = "Skills" },
                new FilterOption { Name = "All" }
            };

            SearchCommand = new Command(async () => await ExecuteSearch());
        }

        private async Task ExecuteSearch()
        {
            if (SelectedFilter == null || string.IsNullOrEmpty(SearchText))
            {
                // Handle case when no filter or search text is provided
                return;
            }

            try
            {
                
                var result = await _cookRepository.SearchCook(SelectedFilter.Name, SearchText);
                Cooks = new ObservableCollection<CookProfile>(result.ToList());
                if(Cooks.Count > 0)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new CookLists(Cooks));
                }
                
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
