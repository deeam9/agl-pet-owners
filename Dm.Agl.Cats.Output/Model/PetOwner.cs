using System.Collections.Generic;
namespace Dm.Agl.Cats.Output.Model
{
	public class PetOwner
    {
		public string Name { get; set; }

		public string Gender { get; set; }

		public int Age { get; set; }

		public List<Pet> Pets { get; set; }
    }

	
}
