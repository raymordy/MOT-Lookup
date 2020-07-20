using Microsoft.Extensions.DependencyInjection;
using MOT_Lookup.Models;
using MOT_Lookup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MOT_Lookup
{
    class Program
    {
        private static IServiceProvider serviceProvider;

        static async Task Main(string[] args)
        {
            //setup services
            ConfigureServices();
            var motService = serviceProvider.GetService<IMotLookupService>();

            Vehicle vehicle;


            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please enter a vehicle registration to lookup: ");
                var registration = Console.ReadLine();
                try
                {
                    vehicle = await motService.CreateApiRequestAsync(registration);
                    FormatVehicleOutput(vehicle, registration);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error looking up that registration, please enter a valid vehicle registration");
                }

                ConsoleKey response;
                do
                {
                    Console.WriteLine("\n\nWould you like to lookup another vehicle? (y/n)");
                    response = Console.ReadKey(false).Key;
                    if (response != ConsoleKey.Enter)
                        Console.WriteLine();

                } while (response != ConsoleKey.Y && response != ConsoleKey.N);


                if (response == ConsoleKey.N)
                    break;
            }
        }

        private static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<IMotLookupService, MotLookupService>();

            serviceProvider = services.BuildServiceProvider();
        }

        private static void FormatVehicleOutput(Vehicle vehicle, string userInput)
        {
            if (vehicle != null)
            {
                Console.WriteLine("----------------------");
                Console.WriteLine("Results for: {0}", userInput.ToUpper());
                Console.WriteLine("----------------------");
                Console.WriteLine("Make: {0}", vehicle.Make);
                Console.WriteLine("Model: {0}", vehicle.Model);
                Console.WriteLine("Colour: {0}", vehicle.PrimaryColour);
                Console.WriteLine("MOT Expiry Date: {0}", vehicle.MotTests != null ? vehicle.MotTests.FirstOrDefault().ExpiryDate : vehicle.MotTestExpiryDate);
                Console.WriteLine("Mileage at last MOT: {0}", vehicle.MotTests != null ? vehicle.MotTests.FirstOrDefault().OdometerValue : "No MOT test records to display.");
            }
        }
    }
}
