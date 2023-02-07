using System;
using System.Threading.Tasks;

namespace Azure_Dev_CosmosDB
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			Console.WriteLine("Starting the cosmosdb demo");
			CosmosService _cosmosService = new CosmosService();
			
			Console.WriteLine("Creating few documents");
			await _cosmosService.CreateProduct();

			Console.WriteLine("Getting smartphones...");
			var products = await _cosmosService.GetProducts();

			products.ForEach(x => Console.WriteLine(x.Name));
			
			Console.WriteLine("Deleting one of the item");
			await _cosmosService.DeleteItem();

			Console.ReadLine();

		}
	}
}
