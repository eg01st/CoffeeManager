using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManager.Models.Interfaces
{
    public interface IUser
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
