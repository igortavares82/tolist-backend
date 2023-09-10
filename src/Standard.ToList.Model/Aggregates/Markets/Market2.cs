using System.Net.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Standard.ToList.Model.Aggregates.Markets
{
    public class Market2 : Entity
	{
		public string Name { get; set; }
		public string BrandIcon { get; set; }
		public string BaseUrl { get; set; }

        public Market2()
		{
		}
	}
}

