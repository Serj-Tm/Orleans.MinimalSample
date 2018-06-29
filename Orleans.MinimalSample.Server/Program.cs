using Orleans.Hosting;
using Orleans.MinimalSample.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.MinimalSample.Server
{
        class Program
        {
            static async Task Main(string[] args)
            {
                await StartSilo();
            }
            static async Task StartSilo()
            {
                using (var host = new SiloHostBuilder()
                    .UseLocalhostClustering()
                    //.ConfigureLogging(logging => logging.AddConsole())
                    .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(SampleGrain).Assembly).WithReferences())
                    .Build())
                {
                    await host.StartAsync();

                    Console.WriteLine("Silo started. Press any key to terminate...");
                    Console.ReadKey();
                }
            }


        }

        public class SampleGrain : Grain, ISample
        {
            public Task<string> Ping(string message)
            {
                Console.WriteLine($"Pinged with '{message}'");
                return Task.FromResult($"Message '{message}' received");
            }
        }
}
