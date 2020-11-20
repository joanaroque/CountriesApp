
using Nations.Views;

using Prism.Commands;
using Prism.Navigation;

namespace Nations.ViewModels
{
    public class CovidViewModel : Covid19Data
    {
        readonly INavigationService _navigationService;
        DelegateCommand _selectCovidCommand;

        public CovidViewModel(
            INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectCovidCommand => _selectCovidCommand ??
            (_selectCovidCommand = new DelegateCommand(SelectCountryAsync));

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
