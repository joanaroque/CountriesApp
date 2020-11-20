using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

using Nations.Models;
using Nations.Services;

using Prism.Commands;
using Prism.Navigation;

using Xamarin.Essentials;

namespace Nations.ViewModels
{
    class CountriesPageViewModel : ViewModelBase
    {
        readonly INavigationService _navigationService;
        readonly IApiService _apiService;

        bool _isRunning;

        string _search;
        List<Country> _myCountries;
        DelegateCommand _searchCommand;

        ObservableCollection<CountryViewModel> _countries;

        public CountriesPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            Title = "Countries";
            _navigationService = navigationService;
            _apiService = apiService;

            LoadCountriesAsync();
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand =
            new DelegateCommand(ShowCountries));

        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowCountries();
            }
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public ObservableCollection<CountryViewModel> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }

        async void LoadCountriesAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No internet connection", "OK");
                return;
            }

            IsRunning = true;

            string url = App.Current.Resources["UrlAPI"].ToString();

            Response response = await _apiService.GetCountriesAsync<Country>(
                url, "/rest", "/v2/all");

            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "OK");
                return;
            }

            _myCountries = (List<Country>)response.Result;

            ShowCountries();
        }

        void ShowCountries()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Countries = new ObservableCollection<CountryViewModel>(
                    _myCountries.Select(c =>
                    new CountryViewModel(_navigationService)
                    {
                        Alpha2Code = _apiService.CheckString(c.Alpha2Code),
                        Alpha3Code = _apiService.CheckString(c.Alpha3Code),
                        AltSpellings = _apiService.CheckStringList(c.AltSpellings),
                        Area = c.Area,
                        Borders = _apiService.CheckStringList(c.Borders),
                        CallingCodes = _apiService.CheckStringList(c.CallingCodes),
                        Capital = _apiService.CheckString(c.Capital),
                        Cioc = _apiService.CheckString(c.Cioc),
                        Currencies = c.Currencies,
                        Demonym = _apiService.CheckString(c.Demonym),
                        Flag = c.Flag,
                        Gini = c.Gini,
                        Languages = c.Languages,
                        Latlng = c.Latlng,
                        Name = c.Alpha2Code == RegionInfo.CurrentRegion.Name ?
                        _apiService.CheckString(c.Name) + " (Current country)" :
                        _apiService.CheckString(c.Name),
                        NativeName = _apiService.CheckString(c.NativeName),
                        NumericCode = _apiService.CheckString(c.NumericCode),
                        Population = c.Population,
                        Region = _apiService.CheckString(c.Region),
                        RegionalBlocs = _apiService.CheckRegionalBlocsList(c.RegionalBlocs),
                        Subregion = _apiService.CheckString(c.Subregion),
                        Timezones = _apiService.CheckStringList(c.Timezones),
                        TopLevelDomain = _apiService.CheckStringList(c.TopLevelDomain),
                        Translations = c.Translations
                    })
                    .ToList());
            }
            else
            {
                Countries = new ObservableCollection<CountryViewModel>(
                    _myCountries.Select(c =>
                    new CountryViewModel(_navigationService)
                    {
                        Alpha2Code = _apiService.CheckString(c.Alpha2Code),
                        Alpha3Code = _apiService.CheckString(c.Alpha3Code),
                        AltSpellings = _apiService.CheckStringList(c.AltSpellings),
                        Area = c.Area,
                        Borders = _apiService.CheckStringList(c.Borders),
                        CallingCodes = _apiService.CheckStringList(c.CallingCodes),
                        Capital = _apiService.CheckString(c.Capital),
                        Cioc = _apiService.CheckString(c.Cioc),
                        Currencies = c.Currencies,
                        Demonym = _apiService.CheckString(c.Demonym),
                        Flag = c.Flag,
                        Gini = c.Gini,
                        Languages = c.Languages,
                        Latlng = c.Latlng,
                        Name = c.Alpha2Code == RegionInfo.CurrentRegion.Name ?
                        _apiService.CheckString(c.Name) + " (Current country)" :
                        _apiService.CheckString(c.Name),
                        NativeName = _apiService.CheckString(c.NativeName),
                        NumericCode = _apiService.CheckString(c.NumericCode),
                        Population = c.Population,
                        Region = _apiService.CheckString(c.Region),
                        RegionalBlocs = _apiService.CheckRegionalBlocsList(c.RegionalBlocs),
                        Subregion = _apiService.CheckString(c.Subregion),
                        Timezones = _apiService.CheckStringList(c.Timezones),
                        TopLevelDomain = _apiService.CheckStringList(c.TopLevelDomain),
                        Translations = c.Translations
                    })
                    .Where(c => c.Name.ToLower().Contains(Search.ToLower()))
                    .ToList());
            }
        }
    }
}
