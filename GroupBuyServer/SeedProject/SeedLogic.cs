using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using SeedProject.Models;

namespace SeedProject
{
    public class SeedLogic
    {
        private const string commandString =
            "curl -G -H \"api_key: SEM3DA0FE4E36F0F76F66177649FD8C0CE89\" https://api.semantics3.com/test/v1/products --data-urlencode 'q={\"cat_id\":\"{0}\", \"offset\": {1} }' > C:\\GroupBuy\\GroupBuyServer\\SeedProject\\ProductsData\\{2}.txt";

        public int SeddUsers()
        {
            int success = 0;
            foreach (var category in UsersHelper.Users)
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
            foreach (string line in File.ReadLines("../../ProductsData/categoriesFiles.txt", Encoding.UTF8))
            {
                using (var re = File.OpenText("../../ProductsData/" + line))
                using (var reader = new JsonTextReader(re))
                {
                    JsonTextReader jsonTextReader = reader;
                }
            }
            return 0;
        }

        private List<Category> LoadAllCategories(bool isCreateBat)
        {
            var random = new Random();
            var allProductsFiles = new List<string>();
            var allCategories = new List<Category>();
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

                            allCategories.Add(level1);
                            isWriteCat = true;
                            break;
                        }
                        case 2:
                        {
                            allCategories.Add(new Category
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

            return allCategories;
        }
    }
}
