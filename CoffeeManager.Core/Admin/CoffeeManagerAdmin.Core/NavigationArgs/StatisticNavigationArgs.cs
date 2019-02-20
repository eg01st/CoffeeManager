using System;
using System.Collections.Generic;
using CoffeeManager.Models;

namespace CoffeeManagerAdmin.Core.NavigationArgs
{
    public class StatisticNavigationArgs
    {
        public StatisticNavigationArgs(DateTime from, DateTime to, List<Entity> coffeeRooms)
        {
            From = from;
            To = to;
            CoffeeRooms = coffeeRooms;
        }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<Entity> CoffeeRooms { get; }
    }
}