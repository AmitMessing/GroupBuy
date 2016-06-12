using System;
using System.Collections.Generic;
using SeedProject.Models;

namespace SeedProject
{
    public static class UsersHelper
    {
        public static List<User> Users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Noam",
                    LastName = "Horovitz",
                    UserName = "noamh",
                    Password = "Aa123456",
                    Email = "mymail@gmail.com",
                    Image = ImagesHelper.Users[0]
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Amit",
                    LastName = "Messing",
                    UserName = "amitm",
                    Password = "Aa123456",
                                        Email = "mymail@gmail.com",
                    Image = ImagesHelper.Users[1]
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Yulia",
                    LastName = "Teverovsky",
                    UserName = "yuliat",
                    Password = "Aa123456",
                    Email = "mymail@gmail.com",
                    Image = ImagesHelper.Users[2]
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Yoni",
                    LastName = "Yoni Abitbol",
                    UserName = "yonia",
                    Password = "Aa123456",
                    Email = "mymail@gmail.com",
                    Image = ImagesHelper.Users[3]
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Rachel",
                    LastName = "Horovitz",
                    UserName = "rachel",
                    Password = "Aa123456",
                    Email = "mymail@gmail.com",
                    Image = ImagesHelper.Users[4]
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Roy",
                    LastName = "Cohen",
                    UserName = "royc",
                    Password = "Aa123456",
                    Email = "mymail@gmail.com",
                    Image = ImagesHelper.Users[5]
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Noy",
                    LastName = "Cohen",
                    UserName = "noyc",
                    Password = "Aa123456",
                    Email = "mymail@gmail.com",
                    Image = ImagesHelper.Users[6]
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Nadav",
                    LastName = "Mary",
                    UserName = "nadavm",
                    Password = "Aa123456"
                    ,
                    Email = "mymail@gmail.com",
                    Image = ImagesHelper.Users[7]
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Koby",
                    LastName = "Shimony",
                    UserName = "kobys",
                    Password = "Aa123456",
                    Email = "mymail@gmail.com",
                    Image = ImagesHelper.Users[8]
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Bar",
                    LastName = "Ezra",
                    UserName = "bare",
                    Password = "Aa123456",
                    Email = "mymail@gmail.com",
                    Image = ImagesHelper.Users[9]
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Amit",
                    LastName = "Wolfus",
                    UserName = "amitw",
                    Password = "Aa123456",
                    Email = "mymail@gmail.com",
                    Image = ImagesHelper.Users[10]
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Amir",
                    LastName = "Schlezinger",
                    UserName = "amirs",
                    Password = "Aa123456",
                    Email = "mymail@gmail.com",
                    Image = ImagesHelper.Users[11]
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Roni",
                    LastName = "Dalomi",
                    UserName = "ronid",
                    Password = "Aa123456",
                    Email = "mymail@gmail.com",
                    Image = ImagesHelper.Users[12]
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Roni",
                    LastName = "Dalomi2",
                    UserName = "ronid2",
                    Password = "Aa123456",
                    Email = "mymail@gmail.com",
                    Image = ImagesHelper.Users[13]
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Ingrid",
                    LastName = "Mickel",
                    UserName = "ingridm",
                    Password = "Aa123456",
                    Email = "mymail@gmail.com",
                    Image = ImagesHelper.Users[14]
                },
            };
    }
}
