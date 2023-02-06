using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azure_Dev_CosmosDB
{
	public class Product
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("name")]

		public string Name { get; set; }
		[JsonProperty("description")]

		public string Description { get; set; }
		[JsonProperty("category")]

		public string Category { get; set; }
		[JsonProperty("price")]

		public double Price { get; set; }
	}
}
