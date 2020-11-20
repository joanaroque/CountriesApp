using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Nations.Models;

using Newtonsoft.Json;

namespace Nations.Services
{

    public class ApiService : IApiService
    {

        public string CheckString(string property)
        {
            if (!string.IsNullOrEmpty(property))
            {
                return property;
            }

            return "N/A";
        }

        public List<string> CheckStringList(List<string> propertiesList)
        {
            if (propertiesList.Count == 0)
            {
                propertiesList.Add("N/A");

                return propertiesList;
            }

            return propertiesList;
        }
        public List<RegionalBloc> CheckRegionalBlocsList(List<RegionalBloc> list)
        {
            foreach (var rb in list)
            {
                rb.Name = CheckString(rb.Name);
                rb.Acronym = CheckString(rb.Acronym);
                rb.OtherAcronyms = CheckStringList(rb.OtherAcronyms);
                rb.OtherNames = CheckStringList(rb.OtherNames);
            }

            return list;
        }

        public async Task<Response> GetCountriesAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                var jsonSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };

                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.GetAsync(url);
                string result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                List<T> list = JsonConvert.DeserializeObject<List<T>>(result, jsonSettings);
                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
