using System.Collections.Generic;


namespace Dm.Agl.Cats.Output.Model
{
    public class CatsPerGender
    {
		public string Gender { get; set; }
		public IEnumerable<string> CatNames { get; set; }
    }
}
