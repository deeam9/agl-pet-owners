using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dm.Agl.Cats.Output;
using Dm.Agl.Cats.Output.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Dm.Agl.XUnitTest
{
    public class PetOwnerServiceXUnitTest
    {
	    [Fact]
	    public async Task PeopleService_Thorws_HttpRequestException_Test()
	    {
		    var appSettings = new AppSettings
		    {
			    Url = "http://chinchpipliki.com"
			};
		    var mockConfig = new Mock<IOptions<AppSettings>>();
		    mockConfig.Setup(c => c.Value)
				.Returns(appSettings);

			var petOwnerService = new PetOwnerService(mockConfig.Object);

		    await Assert.ThrowsAsync<HttpRequestException>(async ()=>await petOwnerService.GetPetOwnersAsync());


	    }

	    [Fact]
	    public async Task PeopleService_Thorws_Json_Reader_Exception_Test()
	    {
		    var appSettings = new AppSettings
		    {
			    Url = "http://abc.com"
		    };
		    var mockConfig = new Mock<IOptions<AppSettings>>();
		    mockConfig.Setup(c => c.Value)
			    .Returns(appSettings);

		    var petOwnerService = new PetOwnerService(mockConfig.Object);

		    await Assert.ThrowsAsync<JsonReaderException>(async () => await petOwnerService.GetPetOwnersAsync());

	    }

	    [Fact]
	    public async Task PeopleService_Returns_Valid_Result_Test()
	    {
		    var appSettings = new AppSettings
		    {
			    Url = "http://agl-developer-test.azurewebsites.net/people.json"
			};
		    var mockConfig = new Mock<IOptions<AppSettings>>();
		    mockConfig.Setup(c => c.Value)
			    .Returns(appSettings);

		    var petOwnerService = new PetOwnerService(mockConfig.Object);

		    var response = await petOwnerService.GetPetOwnersAsync();

		    Assert.True(response != null);

	    }

	}
	
}
