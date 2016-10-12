using System.Collections.Generic;

namespace CoffeeManager.Models
{
    public class RequestStorage
    {
        public List<Request> Requests { get; set; }

        public List<string> Errors => new List<string>();
    }
}
