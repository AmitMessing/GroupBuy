using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using FluentNHibernate.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NHibernate.Dialect;
using NHibernate.Linq;
using SeedProject.Models;

namespace SeedProject
{
    public class SeedLogic
    {
        private const string commandString =
            "curl -G -H \"api_key: SEM39F749F6C099026F730BBF7CCFF0FC859\" https://api.semantics3.com/test/v1/products --data-urlencode 'q={\"cat_id\":\"{0}\", \"offset\": {1} }' > C:\\GroupBuy\\GroupBuyServer\\SeedProject\\ProductsData\\{2}.txt";

        public List<Category> Categories { get; set; }

        public int SeddUsers()
        {
            int success = 0;
            foreach (var user in UsersHelper.Users)
            {
                using (var session = NHibernateHandler.CurrSession)
                {
                    try
                    {
                        session.Save(user);
                        session.Flush();
                        success++;
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            return success;
        }

        public int SeddCategories(bool isCreateBat)
        {
            int success = 0;
            List<Category> allCategories = LoadAllCategories(isCreateBat);

            foreach (var category in allCategories)
            {
                using (var session = NHibernateHandler.CurrSession)
                {
                    try
                    {
                        session.Save(category);
                        session.Flush();
                        success++;
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            return success;
        }

        public int SeddProducts()
        {
            List<Product> allProducts = LoadAllProducts();
            int success = 0;

            foreach (var product in allProducts)
            {
                using (var session = NHibernateHandler.CurrSession)
                {
                    using (var tran = session.BeginTransaction())
                    {
                        try
                        {
                            session.Save(product);
                            foreach (var discount in product.Discounts)
                            {
                                session.Save(discount);
                            }
//                            session.Flush();
                            tran.Commit();
                            success++;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }

            return success;
        }

        private DateTime RandomPastDate(Random random)
        {
            var start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }

        private DateTime RandomFutureDate(Random random)
        {
            var start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return DateTime.Now.AddDays(random.Next(range));
        }

        private void LoadAllCategoriesFromDb()
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                Categories = session.Query<Category>().ToList();
            }
        }

        private List<Product> LoadAllProducts()
        {
            var products = new List<Product>();
            int productSquence = 1;

            foreach (string line in File.ReadLines("../../ProductsData/categoriesFiles.txt", Encoding.UTF8))
            {
                List<Product> newProducts = LoadAllCurrentFileProducts(line, productSquence);
                products.AddRange(newProducts);
                productSquence = productSquence + newProducts.Count;
            }

            return products;
        }

        private List<Product> LoadAllCurrentFileProducts(string fileName, int productSquence)
        {
            var allProducts = new List<Product>();
            if (Categories == null)
            {
                LoadAllCategoriesFromDb();
            }

            var random = new Random();

            try
            {
                var file = File.ReadAllText("../../ProductsData/" + fileName);


                if (!string.IsNullOrEmpty(file))
                {

                    JToken products = JToken.Parse(file)["results"];

                    foreach (var product in products)
                    {
                        Category category = Categories.FirstOrDefault(x => x.Id == product["cat_id"].Value<int>());
                        if (category != null && product["price"] != null)
                        {
                            var newProduct = new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = product["name"].Value<string>(),
                                Categories = new List<Category> {category},
                                BasicPrice = product["price"].Value<double>(),
                                PublishDate = RandomPastDate(random),
                                EndDate = RandomFutureDate(random),
                                Seller = UsersHelper.Users[random.Next(0, UsersHelper.Users.Count)],
                                Buyers = new List<User>(),
                                Description = "No folks we’re not pulling you leg! This rare Chinese tea is carefully picked by specially trained monkeys in a remote mountain region of China. Legend has it that monkeys were first used to collect tea ten centuries ago, because upon seeing it’s master trying to reach some tea growing wild on a mountain face, the monkey climbed up the steep face and collected the tea growing there and brought it down to his master. This wild tea was considered so delicious that other people began to train monkeys to collect this rare wild tea. Nowadays the practice of monkeys picking tea has all but died out, except in one small remote village where they still continue this remarkable tradition. No monkeys are harmed or mistreated in order for us to bring this rare brew to you! In fact the monkeys and their ancestors before them have been doing this job for generations and are treated as respected members of their human keeper’s families."
                            };

                            newProduct.Discounts = new List<Discount>
                            {
                                new Discount {Id = Guid.NewGuid(), ProductId = newProduct.Id, UsersAmount = 2, Present = 10},
                                new Discount {Id = Guid.NewGuid(), ProductId = newProduct.Id, UsersAmount = 5, Present = 12},
                                new Discount {Id = Guid.NewGuid(), ProductId = newProduct.Id, UsersAmount = 7, Present = 23},
                            };

                            if (product["description"] != null)
                            {
                                newProduct.Description = product["description"].Value<string>();
                            }

                            var numOfByers = random.Next(0, UsersHelper.Users.Count - 1);
                            for (var i = 0; i < numOfByers; i++)
                            {
                                bool found = false;
                                var user = new User();
                                while (!found)
                                {
                                    user = UsersHelper.Users[random.Next(0, UsersHelper.Users.Count)];
                                    found = (!newProduct.Buyers.Contains(user)) && (newProduct.Seller != user);
                                }
                                newProduct.Buyers.Add(user);
                            }

                            allProducts.Add(newProduct);
                            productSquence++;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return allProducts;
        }

        private List<Category> LoadAllCategories(bool isCreateBat)
        {
            var random = new Random();
            var allProductsFiles = new List<string>();
            Categories = new List<Category>();
            Category level1 = null;

            using (var file = new StreamWriter("../../ProductsData/createProductsFiles.bat", true))
            {
                foreach (string line in File.ReadLines("../../CategoriesData/Categories.txt", Encoding.UTF8))
                {
                    var categoryByLevel = line.Replace(" ", string.Empty).Split(new[] {'.'}, 2);
                    var cat = categoryByLevel[1].Split('(', ')');
                    bool isWriteCat = false;
                    switch (int.Parse(categoryByLevel[0]))
                    {
                        case 1:
                        {
                            level1 = new Category
                            {
                                Id = int.Parse(cat[cat.Length - 2]),
                                Name = cat[0],
                                ParentId = 1
                            };

                            Categories.Add(level1);
                            isWriteCat = true;
                            break;
                        }
                        case 2:
                        {
                            Categories.Add(new Category
                            {
                                Id = int.Parse(cat[cat.Length - 2]),
                                Name = cat[0],
                                ParentId = level1.Id
                            });
                            isWriteCat = true;
                            break;
                        }
                        default:
                        {
                            break;
                        }
                    }

                    // Add to the create products file
                    if (isCreateBat && isWriteCat)
                    {
                        
                        int offset = random.Next(0, 6);

                        for (int currOffset = 0; currOffset < offset; currOffset++)
                        {
                            var fileName = cat[0].Replace("&", "and") + currOffset + ".txt";
                            allProductsFiles.Add(fileName);

                            file.WriteLine(
                            "curl -G -H \"api_key: SEM3DA0FE4E36F0F76F66177649FD8C0CE89\" https://api.semantics3.com/test/v1/products --data-urlencode 'q={\"cat_id\":\"" +
                            cat[1] + "\", \"offset\": " + (10 * currOffset) +
                            " }' > C:\\GroupBuy\\GroupBuyServer\\SeedProject\\ProductsData\\" + fileName);
                        }
                    }
                }
            }

            if (isCreateBat)
            {
                // Write all the categories created to file
                using (var file = new StreamWriter("../../ProductsData/categoriesFiles.txt", true))
                {
                    allProductsFiles.ForEach(file.WriteLine);
                }
            }

            return Categories;
        }
    }
}
