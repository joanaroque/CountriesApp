using System.Collections.Generic;
using System.Threading.Tasks;

using Nations.Models;

namespace Nations.Services
{
    public interface IApiService
    {
        string CheckString(string property);

        Task<Response> GetCountriesAsync<T>(string urlBase, string servicePrefix, string controller);

        List<string> CheckStringList(List<string> list);

        List<RegionalBloc> CheckRegionalBlocsList(List<RegionalBloc> list);

    }
}
