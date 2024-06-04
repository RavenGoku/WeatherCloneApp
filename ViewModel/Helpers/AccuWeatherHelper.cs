using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherCloneApp.Model;

namespace WeatherCloneApp.ViewModel.Helpers
{
    public class AccuWeatherHelper
    {
        public const string API_KEY = "q5yCxaF5wtvnuaUftwHYgbYlEVusiogH";

        //create const Base_URL to get base location that can be used anywhere in our app
        public const string BASE_URL = "http://dataservice.accuweather.com/";

        //create const Autocomplete endpoint as one of the endpoint that we will request from weather {0} API Key and {1} API City
        public const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";

        //create const current conditions endpoint as one of the endpoint that we will request from weather API Condition and API Key
        public const string CURRENT_CONDITIONS_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";

        //static method is used because we wanted only call that method and not making an instance from it
        //Also that method is a 'Task' for an API to get list of cities
        public static async Task<List<City>> GetCities(string query)
        {
            //creating list of cities
            List<City> cities = new List<City>();
            //create url that combine BASE_URL and AUTOCOMPLETE ENDPOINT with {0} as API_KEY and {1} as query(city number from API)
            string url = BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, query);

            //create get request from API directive 'using' is used with brackets so it will close safely after we finish with code

            using (HttpClient client = new HttpClient())
            {
                //using await keyword it will wait for response from server(API request) till it get some response(error, success etc.)
                //when using await we need to include async keyword in our method definition
                var response = await client.GetAsync(url);
                //getting json from response object and store it in string member json
                string json = await response.Content.ReadAsStringAsync();

                cities = JsonConvert.DeserializeObject<List<City>>(json);
            };

            return cities;
        }

        public static async Task<CurrentConditions> GetCurrentCondition(string cityKey)
        {
            CurrentConditions currentConditions = new CurrentConditions();
            string url = BASE_URL + string.Format(CURRENT_CONDITIONS_ENDPOINT, cityKey, API_KEY);

            using (HttpClient client = new HttpClient())
            {
                var responseMsg = await client.GetAsync(url);

                string jsonCurrentCondition = await responseMsg.Content.ReadAsStringAsync();
                //To get one current condition from a list we can get simple casting method and get a first or default property
                currentConditions = (JsonConvert.DeserializeObject<List<CurrentConditions>>(jsonCurrentCondition)).FirstOrDefault();
            }
            return currentConditions;
        }
    }
}