using MOT_Lookup.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MOT_Lookup.Services
{
    class MotLookupService : IMotLookupService
    {
        private static string ApiKey = "fZi8YcjrZN1cGkQeZP7Uaa4rTxua8HovaswPuIno";
        private static string ApiUri = "https://beta.check-mot.service.gov.uk/trade/vehicles/mot-tests";
        private static readonly HttpClient HttpClient = new HttpClient();

        public async Task<Vehicle> CreateApiRequestAsync(string registration)
        {
            var vehicleRegistration = CleanInput(registration);

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{ApiUri}?registration={vehicleRegistration}"),
                Method = HttpMethod.Get,
                Headers = {
                    { "x-api-key", ApiKey }
                }
            };

            var response = await HttpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var vehicleDetails = JsonConvert.DeserializeObject<IEnumerable<Vehicle>>(await response.Content.ReadAsStringAsync());

                return vehicleDetails.FirstOrDefault();
            }
            else
            {
                throw new Exception(response.StatusCode + " " + response.Content);
            }

            
        }

        private string CleanInput(string registration)
        {
            return registration.Replace(" ", "");
        }

    }
}
