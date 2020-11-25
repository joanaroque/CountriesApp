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

        public string CheckStringCountries(string property)
        {
            if (!string.IsNullOrEmpty(property))
            {
                return property;
            }

            return "N/A";
        }

        public List<string> CheckStringCountriesList(List<string> propertiesList)
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
                rb.Name = CheckStringCountries(rb.Name);
                rb.Acronym = CheckStringCountries(rb.Acronym);
                rb.OtherAcronyms = CheckStringCountriesList(rb.OtherAcronyms);
                rb.OtherNames = CheckStringCountriesList(rb.OtherNames);
            }

            return list;
        }

        public async Task<Response> GetCountriesAsync<T>(string urlBase)
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

                HttpResponseMessage response = await client.GetAsync(urlBase);
                string result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                Console.WriteLine($"Fetching countries from API: {DateTime.Now}");


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

        public string CheckStringCovid(string property)
        {
            if (!string.IsNullOrEmpty(property))
            {
                return property;
            }

            return "N/A";
        }

        public async Task<Response> GetCovidAsync<T>(string urlBase)
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

                
                HttpResponseMessage response = await client.GetAsync(urlBase);
                string result = await response.Content.ReadAsStringAsync();


                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,

                    };
                }

                Console.WriteLine($"Fetching covid data from API: {DateTime.Now}");


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
