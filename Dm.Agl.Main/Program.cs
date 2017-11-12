using System;
using System.IO;
using System.Threading.Tasks;
using Dm.Agl.Cats.Output;
using Dm.Agl.Cats.Output.Service;
using Dm.Agl.Cats.Output.ServiceAbstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DM.Agl.Main
{
    class Program
    {
	    
		private static void Main(string[] args)
        {
	        GetCatsPerGenderAsync().Wait();
        }

	    public static IConfigurationRoot Configuration { get; set; }
	    public IServiceProvider ServiceProvider { get; set; }
		private static async Task GetCatsPerGenderAsync()
	    {
			//	set up configration
		    var builder = new ConfigurationBuilder()
			    .SetBasePath(Directory.GetCurrentDirectory())
			    .AddJsonFile("appsettings.json", optional: false);
			
			Configuration = builder.Build();

			// set up services
			var services = new ServiceCollection();

		    services
			    .AddScoped<IPetOwnerService, PetOwnerService>()
			    .AddScoped<ICatService, CatService>();
		    services.AddOptions();

		    services.Configure<AppSettings>(Configuration.GetSection("Agl"));

		    var serviceProvider = services.BuildServiceProvider();

			// Get all the cats under each gender from CatService
			var catService = serviceProvider.GetService<ICatService>();
		    var catsPerGender = await catService.GetCatsPerGenderAsync();

		    if (catsPerGender == null) return;

			// Display cats under each gender
		    foreach (var person in catsPerGender)
		    {
			    Console.WriteLine(person.Gender);
				Console.WriteLine("-------------");
			    foreach (var catName in person.CatNames)
			    {
				    Console.WriteLine(catName);
			    }

				Console.WriteLine();
		    }
		    Console.WriteLine("Press any key..");

			Console.Read();
		    

		}
	}
}