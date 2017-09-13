using System;
using System.Threading.Tasks;
using CoffeeManager.Models;
namespace CoffeManager.Common
{
    public interface IAdminManager
    {
        Task<Entity[]> GetCoffeeRooms();
    }
}
