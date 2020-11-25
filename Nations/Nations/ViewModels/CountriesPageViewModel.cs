using System;
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
        List<Covid19Data> _covid;
        DelegateCommand _searchCommand;


        ObservableCollection<CountryViewModel> _countries;

        ObservableCollection<CovidViewModel> _corona;

        public CountriesPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            Title = "Countries list";
            _navigationService = navigationService;
            _apiService = apiService;

            LoadCovidDataAsync();


            LoadCountriesAsync();

        }

        private async void LoadCovidDataAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No internet connection", "OK");
                return;
            }

            IsRunning = true;

            string url = "https://coronavirus-19-api.herokuapp.com/countries";

            Response response = await _apiService.GetCovidAsync<Covid19Data>(url);

            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "OK");
                return;
            }

            _covid = (List<Covid19Data>)response.Result;

           // ShowCovid();
        }

        //private void ShowCovid()
        //{
        //    if (string.IsNullOrEmpty(Search))
        //    {
        //        Corona = new ObservableCollection<CovidViewModel>(
        //            _covid.Select(c =>
        //            new CovidViewModel(_navigationService)
        //            {
        //                Country = _apiService.CheckStringCovid(c.Country),
        //                Cases = c.Cases,
        //                TodayCases = c.TodayCases,
        //                Deaths = c.Deaths,
        //                TodayDeaths = c.TodayDeaths,
        //                Recovered = c.Recovered,
        //                Active = c.Active,
        //                Critical = c.Critical,
        //                CasesPerOneMillion = c.CasesPerOneMillion
        //            })
        //            .ToList());
        //    }
        //    else
        //    {
        //        Corona = new ObservableCollection<CovidViewModel>(
        //            _covid.Select(c =>
        //            new CovidViewModel(_navigationService)
        //            {
        //                Country = _apiService.CheckStringCovid(c.Country),
        //                Cases = c.Cases,
        //                TodayCases = c.TodayCases,
        //                Deaths = c.Deaths,
        //                TodayDeaths = c.TodayDeaths,
        //                Recovered = c.Recovered,
        //                Active = c.Active,
        //                Critical = c.Critical,
        //                CasesPerOneMillion = c.CasesPerOneMillion
        //            })
        //            .Where(c => c.Country.ToLower().Contains(Search.ToLower()))
        //            .ToList());
        //    }
        //}


        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand =
            new DelegateCommand(ShowCountries));


        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowCountries();
              //  ShowCovid();
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

        public ObservableCollection<CovidViewModel> Corona
        {
            get => _corona;
            set => SetProperty(ref _corona, value);
        }

        async void LoadCountriesAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No internet connection", "OK");
                return;
            }

            IsRunning = true;

            string url = "https://restcountries.eu/rest/v2/all";

            Response response = await _apiService.GetCountriesAsync<Country>(url);

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
                    {
                        Covid19Data covidInfo = _covid.Where(covid => covid.Country.Equals(c.Alpha3Code)).FirstOrDefault();

                        return new CountryViewModel(_navigationService)
                        {
                            Alpha2Code = _apiService.CheckStringCountries(c.Alpha2Code),
                            Alpha3Code = _apiService.CheckStringCountries(c.Alpha3Code),
                            AltSpellings = _apiService.CheckStringCountriesList(c.AltSpellings),
                            Area = c.Area,
                            Borders = _apiService.CheckStringCountriesList(c.Borders),
                            CallingCodes = _apiService.CheckStringCountriesList(c.CallingCodes),
                            Capital = _apiService.CheckStringCountries(c.Capital),
                            Cioc = _apiService.CheckStringCountries(c.Cioc),
                            Currencies = c.Currencies,
                            Demonym = _apiService.CheckStringCountries(c.Demonym),
                            Flag = c.Flag,
                            Gini = c.Gini,
                            Languages = c.Languages,
                            Latlng = c.Latlng,
                            Name = c.Alpha2Code == RegionInfo.CurrentRegion.Name ?
                            _apiService.CheckStringCountries(c.Name) + " (Current country)" :
                            _apiService.CheckStringCountries(c.Name),
                            NativeName = _apiService.CheckStringCountries(c.NativeName),
                            NumericCode = _apiService.CheckStringCountries(c.NumericCode),
                            Population = c.Population,
                            Region = _apiService.CheckStringCountries(c.Region),
                            RegionalBlocs = _apiService.CheckRegionalBlocsList(c.RegionalBlocs),
                            Subregion = _apiService.CheckStringCountries(c.Subregion),
                            Timezones = _apiService.CheckStringCountriesList(c.Timezones),
                            TopLevelDomain = _apiService.CheckStringCountriesList(c.TopLevelDomain),
                            Translations = c.Translations,
                            Covid19Data = covidInfo
                        };

                    })
                    .ToList());
            }
            else
            {

                Countries = new ObservableCollection<CountryViewModel>(
            _myCountries.Select(c =>
            {
                Covid19Data covidInfo = _covid.Where(covid => covid.Country.Equals(c.Alpha3Code)).FirstOrDefault();

                return new CountryViewModel(_navigationService)
                {
                    Alpha2Code = _apiService.CheckStringCountries(c.Alpha2Code),
                    Alpha3Code = _apiService.CheckStringCountries(c.Alpha3Code),
                    AltSpellings = _apiService.CheckStringCountriesList(c.AltSpellings),
                    Area = c.Area,
                    Borders = _apiService.CheckStringCountriesList(c.Borders),
                    CallingCodes = _apiService.CheckStringCountriesList(c.CallingCodes),
                    Capital = _apiService.CheckStringCountries(c.Capital),
                    Cioc = _apiService.CheckStringCountries(c.Cioc),
                    Currencies = c.Currencies,
                    Demonym = _apiService.CheckStringCountries(c.Demonym),
                    Flag = c.Flag,
                    Gini = c.Gini,
                    Languages = c.Languages,
                    Latlng = c.Latlng,
                    Name = c.Alpha2Code == RegionInfo.CurrentRegion.Name ?
                    _apiService.CheckStringCountries(c.Name) + " (Current country)" :
                    _apiService.CheckStringCountries(c.Name),
                    NativeName = _apiService.CheckStringCountries(c.NativeName),
                    NumericCode = _apiService.CheckStringCountries(c.NumericCode),
                    Population = c.Population,
                    Region = _apiService.CheckStringCountries(c.Region),
                    RegionalBlocs = _apiService.CheckRegionalBlocsList(c.RegionalBlocs),
                    Subregion = _apiService.CheckStringCountries(c.Subregion),
                    Timezones = _apiService.CheckStringCountriesList(c.Timezones),
                    TopLevelDomain = _apiService.CheckStringCountriesList(c.TopLevelDomain),
                    Translations = c.Translations,
                    Covid19Data = covidInfo
                };

            }).Where(c => c.Name.ToLower().Contains(Search.ToLower()))
            .ToList());


            }
        }
    }
}
