﻿using System;
using CoffeeManager.Models;
using SQLite;

namespace CoffeeManager.Common
{
    public class SaleEntity : Sale
    {
        [PrimaryKey, AutoIncrement]
        public int DatabaseId { get; set; }
    }
}
