using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dm.Agl.Cats.Output.Model;
using Dm.Agl.Cats.Output.Service;
using Dm.Agl.Cats.Output.ServiceAbstraction;
using Xunit;
using Moq;
using Newtonsoft.Json;

namespace Dm.Agl.XUnitTest
{
    public class CatServiceXUnitTest
    {
        [Fact]
        public async Task PetOwner_Service_Returns_Null_Test()
        {
	        var mockPetOwnerService = new Mock<IPetOwnerService>();

			mockPetOwnerService
			.Setup( c=> c.GetPetOwnersAsync())
			.ReturnsAsync((IEnumerable<PetOwner>) null);

			var catService = new CatService(mockPetOwnerService.Object);

	        var result = await catService.GetCatsPerGenderAsync();

			Assert.Null(result);
        }

	    [Fact]
	    public async Task PetOwner_Service_Returns_Valid_Output_Test()
	    {
		    var mockPetOwnerService = new Mock<IPetOwnerService>();

		    mockPetOwnerService
			    .Setup(c => c.GetPetOwnersAsync())
				.ReturnsAsync(JsonConvert.DeserializeObject<IEnumerable<PetOwner>>(
				    "[{\r\n  \"name\": \"Bob\",\r\n  \"gender\": \"Male\",\r\n   \"age\": 23,\r\n  " +
				    "\"pets\": [\r\n  {\r\n \"name\": \"Glenfiditch\",\r\n  \"type\": \"Cat\"\r\n }," +
				    "\r\n {\r\n  \"name\": \"Fido\",\r\n  \"type\": \"Dog\"\r\n }\r\n ]\r\n  }, " +
				    "{\r\n    \"name\": \"Jennifer\",\r\n    \"gender\": \"Female\",\r\n    \"age\": 18,\r\n    " +
				    "\"pets\": [\r\n      {\r\n        \"name\": \"Daiels\",\r\n        \"type\": \"Cat\"\r\n      }\r\n    ]\r\n  }," +
					"{\r\n    \"name\": \"Steve\",\r\n    \"gender\": \"Male\",\r\n    \"age\": 45,\r\n    \"pets\": null\r\n  }, " +
				    "{\r\n    \"name\": \"Fred\",\r\n    \"gender\": \"Male\",\r\n    \"age\": 40,\r\n    " +
				    "\"pets\": [\r\n      {\r\n        \"name\": \"Tom\",\r\n        \"type\": \"Cat\"\r\n      },\r\n" +
				    "{\r\n        \"name\": \"Max\",\r\n        \"type\": \"Cat\"\r\n      },\r\n " +
				    "     {\r\n        \"name\": \"Sam\",\r\n        \"type\": \"Dog\"\r\n      },\r\n      " +
				    "{\r\n        \"name\": \"Jim\",\r\n        \"type\": \"Cat\"\r\n      }\r\n    ]\r\n  }]"));
				
		    var catService = new CatService(mockPetOwnerService.Object);

		    var catsPerGender = await catService.GetCatsPerGenderAsync();

		    var catsPerGenderList = catsPerGender as IList<CatsPerGender> ?? catsPerGender.ToList();
		    Assert.Equal(2, catsPerGenderList.Count);
			Assert.Equal(4, catsPerGenderList.ElementAt(0).CatNames.Count());
		    Assert.Equal("Glenfiditch", catsPerGenderList.ElementAt(0).CatNames.ElementAt(0));
		    Assert.Equal("Tom", catsPerGenderList.ElementAt(0).CatNames.ElementAt(3));
			Assert.Equal(1, catsPerGenderList.ElementAt(1).CatNames.Count());
		    Assert.Equal("Daiels", catsPerGenderList.ElementAt(1).CatNames.ElementAt(0));

		}
    }
}
