using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Core.ServiceProviders;
using CoffeeManager.Models;

namespace CoffeeManager.Core.Managers
{
    public class UserManager : BaseManager
    {
        private UserServiceProvider provider = new UserServiceProvider();
        public async Task<User[]> GetUsers()
        {
            var users = await provider.GetUsers();
            return users.Where(u => u.IsActive).ToArray();
        }

        public async Task<User> AddUser(string userName)
        {
            return await provider.AddUser(userName);
        }

        public async Task DeleteUser(int id)
        {
            await provider.DeleteUser(id);
        }
    }
}
