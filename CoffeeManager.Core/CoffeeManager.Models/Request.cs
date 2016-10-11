using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManager.Models
{
    public class Request
    {
        public string Method { get; set; }
        public string Path { get; set; }
        public string ObjectJson { get; set; }
    }
}
