using Nations.Models;

using Prism.Navigation;

namespace Nations.ViewModels
{
    public class CountryDetailPageViewModel : ViewModelBase
    {
        Country _country;

        public CountryDetailPageViewModel(
            INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Country details";
        }

        public Country Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("country"))
            {
                Country = parameters.GetValue<Country>("country");
                Title = Country.Name;
            }
        }
    }
}
