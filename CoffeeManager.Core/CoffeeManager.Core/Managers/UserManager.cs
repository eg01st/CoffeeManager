using System.Threading.Tasks;
using CoffeeManager.Core.ServiceProviders;
using CoffeeManager.Models;
using CoffeeManager.Models.Interfaces;

namespace CoffeeManager.Core.Managers
{
    public class UserManager : BaseManager
    {
        private UserServiceProvider provider = new UserServiceProvider();
        public async Task<IUser[]> GetUsers()
        {
             return await provider.GetUsers();
        }

        public async Task<IUser> AddUser(string userName)
        {
            return await provider.AddUser(userName);
        }

        public async Task DeleteUser(int id)
        {
            await provider.DeleteUser(id);
        }
    }
}
