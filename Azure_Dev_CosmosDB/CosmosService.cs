using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.Azure.Cosmos;

namespace Azure_Dev_CosmosDB
{
	public class CosmosService
	{
		string dbName = "stackup";
		string containerName = "products";
		string cosmosAccountURI = "<URL>";
		string cosmosKey = "<Primary Key>";
		Database _cosmosDb;
		Container _container;
		public CosmosService()
		{
			CosmosClient _client = new CosmosClient(cosmosAccountURI, cosmosKey);
			//get the database
			_cosmosDb = _client.GetDatabase(dbName);

			//get the container
			_container = _cosmosDb.GetContainer(containerName);
		}

		public async Task<List<Product>> GetProducts()
		{
			var productsfromDB = new List<Product>();
			//read the data
			var query = new QueryDefinition(query: "Select * from products p where p.category = @category").WithParameter("@category", "smartphone");
			using FeedIterator<Product> feed = _container.GetItemQueryIterator<Product>(query);

			while (feed.HasMoreResults)
			{
				FeedResponse<Product> response = await feed.ReadNextAsync();
				foreach (Product item in response)
				{
					productsfromDB.Add(item);
				}

			}

			return productsfromDB;
		}

		public async Task CreateProduct()
		{
			var products = new List<Product>();
			products.Add(new Product()
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Apple iPhone 14 Pro Max",
				Category = "smartphone",
				Price = 1000,
				Description = "Smartphone from Apple"
			});

			products.Add(new Product()
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Samsung S23 Ultra",
				Category = "smartphone",
				Price = 1000,
				Description = "Smartphone from Samsung"
			});

			products.Add(new Product()
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Nike Air Max",
				Category = "sneakers",
				Price = 1000,
				Description = "Sneakers from Nike"
			});

			foreach (var item in products)
			{
				var response = await _container.CreateItemAsync<Product>(item);
				Console.WriteLine($"{item.Name} with category {item.Category} has been created with {response.RequestCharge} request charge");
			}

		}

		public async Task DeleteItem()
		{
			ItemResponse<Product> response = await _container.DeleteItemAsync<Product>(partitionKey: new PartitionKey("smartphone"), id: "00000000-0000-0000-0000-000000000000");
			Console.WriteLine($"{response.RequestCharge} RU used for deleting");
		}
	}
}
