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

        public async Task<UserMotivationDTO[]> GetUsersMotivation(int motivationId)
        {
            return await Get<UserMotivationDTO[]>(RoutesConstants.GetUsersMotivation,
                new Dictionary<string, string>()
                {
                    {nameof(motivationId), motivationId.ToString()},
                });
        }

        public async Task<MotivationDTO> StartNewMotivation()
        {
            return await Post<MotivationDTO, object>(RoutesConstants.StartNewMotivation, null);
        }

        public async Task FinishMotivation(int motivationId)
        {
            await Post(RoutesConstants.FinishMotivation,
                new Dictionary<string, string>()
                {
                    {nameof(motivationId), motivationId.ToString()},
                });
        }

        public async Task<MotivationDTO> GetCurrentMotivation()
        {
            return await Get<MotivationDTO>(RoutesConstants.GetCurrentMotivation, null);
        }
    }
}