using System.Collections.Generic;
using System.Threading.Tasks;
using Dm.Agl.Cats.Output.Model;

namespace Dm.Agl.Cats.Output.ServiceAbstraction
{
	public interface IPetOwnerService
    {
	    Task<IEnumerable<PetOwner>> GetPetOwnersAsync();
    }
}
