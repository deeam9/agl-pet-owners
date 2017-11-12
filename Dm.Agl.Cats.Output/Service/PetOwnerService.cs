using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dm.Agl.Cats.Output.Model;
using Dm.Agl.Cats.Output.ServiceAbstraction;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Dm.Agl.Cats.Output.Service
{
    public class PetOwnerService : IPetOwnerService
    {
		private static readonly HttpClient Client = new HttpClient();
	    private readonly string _url;
	    public PetOwnerService(IOptions<AppSettings> appSettings)
	    {
		    _url = appSettings.Value.Url;
	    }
		public async Task<IEnumerable<PetOwner>> GetPetOwnersAsync()
	    {
			//Http Get
			// Get all the petOwners from the Api
			var response = await Client.GetAsync(_url);
		    try
		    {
			    response.EnsureSuccessStatusCode();
			    var jsonString = await response.Content.ReadAsStringAsync();

			    if (string.IsNullOrEmpty(jsonString)) return null;

				var deserialisedJson = JsonConvert.DeserializeObject<IEnumerable<PetOwner>>(jsonString);
			    return deserialisedJson;
		    }
		    catch (HttpRequestException e)
		    {
			    Console.WriteLine($"Unsuccessful web request. Message : {e}");
			    throw;
		    }

			catch(JsonReaderException e)
			{
				Console.WriteLine($"Exception thrown in deseralising the response. Message : {e}");
				throw;
			}
	    }

    }
}
