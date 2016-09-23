using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ServiceProviders
{
    public class UserServiceProvider : BaseServiceProvider
    {
        private const string Users = "Users";

        public async Task<User[]> GetUsers()
        {
            return await Get<User[]>(Users);
        }

        public async Task<User> AddUser(string userName)
        {
           return await Post<User, User>(Users, new User() {CofferRoomNo = CoffeeRommNo, Name = userName});
        }

        public async Task DeleteUser(int userId)
        {
            await Delete($"{Users}/{userId}");
        }
    }
}
