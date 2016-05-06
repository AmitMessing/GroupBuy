using System;
using System.Collections.Generic;
using SeedProject.Models;

namespace SeedProject
{
    public class SeedLogic
    {
        public Semantics3Logic Semantics3Logic { get; set; }

        public SeedLogic()
        {
            Semantics3Logic = new Semantics3Logic("SEM3DA0FE4E36F0F76F66177649FD8C0CE89", 
                                                  "NmEyMDQzNzE5MDkwNDllM2JhZjU0YmIyMWE5NGUzZDU");
        }

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

        public int SeddCategories()
        {
            int success = 0;
            List<Category> allCategories = Semantics3Logic.LoadAllCategories();

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
    }
}
