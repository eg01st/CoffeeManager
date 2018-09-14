using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.StaffMotivation;

namespace CoffeManager.Common.Providers
{
    public class MotivationProvider : BaseServiceProvider, IMotivationProvider
    {
        public async Task<ShiftMotivationDTO[]> GetAllMotivationItems()
        {
            return await Get<ShiftMotivationDTO[]>(RoutesConstants.GetAllMotivationItems, null);
        }

        public async Task<UserMotivationDTO[]> GetUsersMotivationFromDate(DateTime date)
        {
            return await Get<UserMotivationDTO[]>(RoutesConstants.GetUsersMotivationFromDate,
                new Dictionary<string, string>()
                {
                    {nameof(date), date.ToString()},
                });
        }
    }
}