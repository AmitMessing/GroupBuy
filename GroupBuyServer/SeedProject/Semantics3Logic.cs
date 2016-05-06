using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SeedProject.Models;
using Semantics3;

namespace SeedProject
{
    public class Semantics3Logic
    {
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }

        public Semantics3Logic(string apiKey, string secretKey)
        {
            ApiKey = apiKey;
            SecretKey = secretKey;
        }

        public List<Category> LoadAllCategories()
        {
            var categories = new Categories(ApiKey, SecretKey);
            categories.categories_field("parent_cat_id", "1");
            JObject apiResponse = categories.get_categories();
            JToken results = apiResponse["results"];

            var allCategories = results.Select(category => new Category
            {
                Id = category["cat_id"].Value<int>(), 
                Name = category["name"].Value<string>()
            }).ToList();

            return allCategories;
        }

        public void LoadAllProducts()
        {
            var products = new Products(ApiKey, SecretKey);
            products.products_field("cat_id", "4992");
            JObject apiResponse = products.get_products();
            Console.Write(apiResponse.ToString());
        }
    }
}
