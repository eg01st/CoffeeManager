using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Models.Interfaces;

namespace CoffeeManager.Core.ServiceProviders
{
    public class UserServiceProvider : BaseServiceProvider
    {
        private const string Users = "Users";

        public async Task<IUser[]> GetUsers()
        {
            return await Get<IUser[]>(Users);
        }

        public async Task<IUser> AddUser(string userName)
        {
           return await Post<IUser, IUser>(Users, new User() {CofferRoomNo = CoffeeRoomNo, Name = userName});
        }

        public async Task DeleteUser(int userId)
        {
            await Delete($"{Users}/{userId}");
        }
    }
}
