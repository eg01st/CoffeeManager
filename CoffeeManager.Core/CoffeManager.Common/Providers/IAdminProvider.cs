using System;
using System.Threading.Tasks;
using CoffeeManager.Models;
namespace CoffeManager.Common
{
    public interface IAdminProvider
    {
        Task<Entity[]> GetCoffeeRooms();
    }
}
