using System;

namespace CoffeeManagerAdmin.Core.NavigationArgs
{
    public class ChildStatisticNavigationArgs
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int CoffeeRoomId { get; set; }
    }
}