using Newtonsoft.Json.Linq;
using RatesChecker.Data.Interface;
using RatesChecker.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace RatesChecker.Data
{
    public class Repo : IRepo
    {
        private readonly HttpClient client = new HttpClient();
       

        public async Task<List<RateViewModel>> HttpRequestRates(string fromDate, string toDate)
        {
            var serializer = new DataContractJsonSerializer(typeof(List<RateViewModel>));

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "RateChecker/v0.01");

            var stringTask = client.GetStringAsync($"https://eservices.mas.gov.sg/api/action/datastore/search.json?resource_id=5f2b18a8-0883-4769-a635-879c63d3caac&limit=500&between[end_of_month]={fromDate},{toDate}");

            var msg = await stringTask;

            JObject rateSearch = JObject.Parse(msg);
            IList<JToken> results = rateSearch["result"]["records"].Children().ToList();

            IList<RateViewModel> searchResults = new List<RateViewModel>();

            foreach (JToken result in results)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
                RateViewModel searchResult = result.ToObject<RateViewModel>();
                searchResults.Add(searchResult);
            }

            return searchResults.ToList();
        }
    }
}
