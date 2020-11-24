using Nations.Helpers;
using Nations.Views;

using Prism.Commands;
using Prism.Navigation;

namespace Nations.ViewModels
{
    class LoginPageViewModel : ViewModelBase
    {
        private bool _isRunning;
        private bool _isEnabled;
        private string _password;
        private DelegateCommand _loginCommand;
        private readonly INavigationService _navigationService;


        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;

            Title = "Login";
            IsEnabled = true;
           
        }


        public DelegateCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand(LoginAsync));

       
        public string Email { get; set; }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }


        private async void LoginAsync()
        {
            if (string.IsNullOrEmpty(Email) || RegexHelper.IsValidEmail(Email))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailError,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert(
                     Languages.Error,
                    Languages.PasswordError,
                    Languages.Accept);
                return;
            }

            IsRunning = true;
            IsEnabled = false;


            await _navigationService.NavigateAsync(nameof(CountriesPage));
            Password = string.Empty;
        }
    }
}
