
using Nations.Interfaces;
using Nations.Resources;

using Xamarin.Forms;

namespace Nations.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept => Resource.Accept;

        public static string Email => Resource.Email;

        public static string EmailError => Resource.EmailError;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string Error => Resource.Error;

        public static string Forgot => Resource.Forgot;

        public static string Login => Resource.Login;

        public static string LoginError => Resource.LoginError;

        public static string Password => Resource.Password;

        public static string PasswordError => Resource.PasswordError;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        public static string Register => Resource.Register;

        public static string Rememberme => Resource.Rememberme;
      

        public static string Saving => Resource.Saving;

        public static string Save => Resource.Save;


        public static string Confirm => Resource.Confirm;


        public static string Yes => Resource.Yes;

        public static string No => Resource.No;

        public static string Ok => Resource.Ok;
    }
}