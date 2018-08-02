Minimal Orleans sample
==============

Server 
------
.Net Framework Console App

    Install-Package Microsoft.Orleans.Core.Abstractions
    Install-Package Microsoft.Orleans.OrleansCodeGenerator.Build
    Install-Package Microsoft.Orleans.Server

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
                //.ConfigureLogging(logging => logging.AddConsole()) //Install-Package Microsoft.Extensions.Logging.Console
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



Interfaces
----------
.Net Standard class library

    Install-Package Microsoft.Orleans.Core.Abstractions
    Install-Package Microsoft.Orleans.OrleansCodeGenerator.Build

    public interface ISample : IGrainWithStringKey
    {
        Task<string> Ping(string message);
    }

Client
------
.Net Core Console App

    Install-Package Microsoft.Orleans.OrleansCodeGenerator.Build
    Install-Package Microsoft.Orleans.Client

    static async Task Main(string[] args)
    {
        await PingSample();
    }

    static async Task PingSample()
    {
        using (var client = new ClientBuilder()
            .UseLocalhostClustering()
            //.ConfigureLogging(logging => logging.AddConsole()) //Install-Package Microsoft.Extensions.Logging.Console
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


