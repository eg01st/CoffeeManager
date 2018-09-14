using System;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.StaffMotivation;

namespace CoffeManager.Common.Managers
{
    public interface IMotivationManager
    {
        Task<ShiftMotivationDTO[]> GetAllMotivationItems();
        
        Task<UserMotivationDTO[]> GetUsersMotivationFromDate(DateTime date);
    }
}