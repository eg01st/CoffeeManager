﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManager.Core
{
    public class UserManager
    {
        public User[] GetUsers()
        {
            return new[]
            {
                new User() {Id = 1, Name = "User1"},
                new User() {Id = 2, Name = "User 2"},
                new User() {Id = 3, Name = "User 3"},
            };
        }

        public void AddUser(User user)
        {
            
        }

        public void DeleteUser(int id)
        {
            
        }
    }
}
