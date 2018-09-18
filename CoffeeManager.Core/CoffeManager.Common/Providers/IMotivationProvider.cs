using System;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.StaffMotivation;

namespace CoffeManager.Common.Providers
{
    public interface IMotivationProvider
    {
        Task<ShiftMotivationDTO[]> GetAllMotivationItems();
        
        Task<UserMotivationDTO[]> GetUsersMotivation(int motivationId);
        
        Task<MotivationDTO> StartNewMotivation();
        
        Task FinishMotivation(int motivationId);
        
        Task<MotivationDTO> GetCurrentMotivation();
    }
}