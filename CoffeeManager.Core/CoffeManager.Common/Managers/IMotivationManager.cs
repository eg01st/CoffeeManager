using System;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.StaffMotivation;

namespace CoffeManager.Common.Managers
{
    public interface IMotivationManager
    {
        Task<ShiftMotivationDTO[]> GetAllMotivationItems();
        
        Task<UserMotivationDTO[]> GetUsersMotivation(int motivationId);
        
        Task<MotivationDTO> StartNewMotivation();
        
        Task FinishMotivation(int motivationId);
        
        Task<MotivationDTO> GetCurrentMotivation();
    }
}