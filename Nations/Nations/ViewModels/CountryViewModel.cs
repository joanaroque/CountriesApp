﻿using Nations.Models;
using Nations.Views;

using Prism.Commands;
using Prism.Navigation;

namespace Nations.ViewModels
{
    class CountryViewModel : Country
    {
        readonly INavigationService _navigationService;
        DelegateCommand _selectCountryCommand;

        public CountryViewModel(
            INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectCountryCommand => _selectCountryCommand ??
            (_selectCountryCommand = new DelegateCommand(SelectCountryAsync));

        async void SelectCountryAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "country", this }
            };

            await _navigationService.NavigateAsync(nameof(CountryDetailPage), parameters);
        }
    }
}
