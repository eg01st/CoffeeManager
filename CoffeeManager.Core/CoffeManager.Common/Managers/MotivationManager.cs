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

        public async Task<UserMotivationDTO[]> GetUsersMotivation(int motivationId)
        {
            return await motivationProvider.GetUsersMotivation(motivationId);
        }

        public async Task<MotivationDTO> StartNewMotivation()
        {
            return await motivationProvider.StartNewMotivation();
        }

        public async Task FinishMotivation(int motivationId)
        {
            await motivationProvider.FinishMotivation(motivationId);
        }

        public async Task<MotivationDTO> GetCurrentMotivation()
        {
            return await motivationProvider.GetCurrentMotivation();
        }
    }
}