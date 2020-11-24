using System.Collections.Generic;
using System.Threading.Tasks;

using Nations.Models;

namespace Nations.Services
{
    public interface IApiService
    {
        string CheckStringCountries(string property);

        Task<Response> GetCountriesAsync<T>(string urlBase);

        List<string> CheckStringCountriesList(List<string> list);

        List<RegionalBloc> CheckRegionalBlocsList(List<RegionalBloc> list);


        string CheckStringCovid(string property);


        Task<Response> GetCovidAsync<T>(string urlBase);


    }
}
