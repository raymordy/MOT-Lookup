using MOT_Lookup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
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
            var vehicleRegistration = ValidateInput(registration);

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
                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                var vehicleDetails = await JsonSerializer.DeserializeAsync<IEnumerable<Vehicle>>(await response.Content.ReadAsStreamAsync(), serializeOptions);
                
                return vehicleDetails.FirstOrDefault();
            }
            else
            {
                throw new Exception(response.StatusCode + " " + response.Content);
            }
        }

        private string ValidateInput(string registration)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            var validatedInput = rgx.Replace(registration, "");
            validatedInput = validatedInput.Replace(" ", "");

            if (validatedInput.Length > 7)
                throw new Exception("Registration too long");

            return validatedInput;
        }

    }
}
