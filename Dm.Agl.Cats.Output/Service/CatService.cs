using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dm.Agl.Cats.Output.Model;
using Dm.Agl.Cats.Output.ServiceAbstraction;

namespace Dm.Agl.Cats.Output.Service
{
    public class CatService: ICatService
    {
	    private readonly IPetOwnerService _petOwnerService;
	    public CatService(IPetOwnerService petOwnerService)
	    {
		    _petOwnerService = petOwnerService;
	    }
	    public async Task<IEnumerable<CatsPerGender>> GetCatsPerGenderAsync()
		{
			// Get all the petOwners from people service
			var petOwners = await _petOwnerService.GetPetOwnersAsync();

			return petOwners == null ? null : BuildCatsPerGender(petOwners);
		}

		private IEnumerable<CatsPerGender> BuildCatsPerGender(IEnumerable<PetOwner> petOwners)
		{
			// group all the cats under each gender in ascending order of their names
			return petOwners.Where(p => p.Pets != null).GroupBy(p => p.Gender, (key, g) =>
				new CatsPerGender
				{
					Gender = key,
					CatNames = g.SelectMany(p => p.Pets.Where(c => c.Type == "Cat"))
						.OrderBy(c => c.Name)
						.Select(c => c.Name)
				}
			);
		}
	}
}
