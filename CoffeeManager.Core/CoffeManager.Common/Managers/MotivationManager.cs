using System;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.StaffMotivation;
using CoffeManager.Common.Providers;

namespace CoffeManager.Common.Managers
{
    public class MotivationManager : IMotivationManager
    {
        private readonly IMotivationProvider motivationProvider;

        public MotivationManager(IMotivationProvider motivationProvider)
        {
            this.motivationProvider = motivationProvider;
        }
        
        public async Task<ShiftMotivationDTO[]> GetAllMotivationItems()
        {
            return await motivationProvider.GetAllMotivationItems();
        }

        public async Task<UserMotivationDTO[]> GetUsersMotivationFromDate(DateTime date)
        {
            return await motivationProvider.GetUsersMotivationFromDate(date);
        }
    }
}