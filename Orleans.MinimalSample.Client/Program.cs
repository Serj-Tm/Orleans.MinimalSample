using Orleans.MinimalSample.Interfaces;
using System;
using System.Threading.Tasks;

namespace Orleans.MinimalSample.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await PingSample();
        }

        static async Task PingSample()
        {
            using (var client = new ClientBuilder()
                .UseLocalhostClustering()
                //.ConfigureLogging(logging => logging.AddConsole())
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(ISample).Assembly).WithReferences())
                .Build())
            {
                await client.Connect();
                Console.WriteLine("Client successfully connected to silo host.");

                var sample = client.GetGrain<ISample>("one");

                var result = await sample.Ping("hello");
                Console.WriteLine(result);
            }

        }

    }

}





