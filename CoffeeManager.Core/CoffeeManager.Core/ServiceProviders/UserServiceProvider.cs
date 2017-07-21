using System.Threading.Tasks;
using CoffeeManager.Models;
using System.Collections.Generic;

namespace CoffeeManager.Core.ServiceProviders
{
    public class UserServiceProvider : BaseServiceProvider
    {
        private const string Users = "Users";

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await Get<IEnumerable<User>>(Users);
        }

        public async Task<User> AddUser(string userName)
        {
           return await Post<User, User>(Users, new User() {CoffeeRoomNo = CoffeeRoomNo, Name = userName});
        }

        public async Task DeleteUser(int userId)
        {
            await Delete($"{Users}/{userId}");
        }
    }
}
