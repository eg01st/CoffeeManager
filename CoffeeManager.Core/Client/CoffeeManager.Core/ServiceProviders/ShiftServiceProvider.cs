using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ServiceProviders
{
    public class ShiftServiceProvider : BaseServiceProvider
    {
        private const string Shift = "shift";
        public async Task<int> StartUserShift(int userId, int counter)
        {
            var result =
                await
                    Post<Shift, object>(Shift, null,
                        new Dictionary<string, string>()
                        {
                            {nameof(userId), userId.ToString()},
                            {nameof(counter), counter.ToString()}
                        });
            return result.Id;
        }


        public async Task<Shift> GetCurrentShift()
        {
            try
            {
                return await Get<Shift>($"{Shift}/getCurrentShift");
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public async Task<Sale[]> GetCurrentShiftSales()
        {
            return await Get<Sale[]>($"{Shift}/getShiftSales");
        }

        public async Task<EndShiftUserInfo> EndShift(int shiftId, decimal realAmount, int endCounter)
        {
           return await
                Post<EndShiftUserInfo, EndShiftDTO>($"{Shift}/endShift",
                    new EndShiftDTO()
                    {
                        CoffeeRoomNo = CoffeeRoomNo,
                        ShiftId = shiftId,
                        RealAmount = realAmount,
                        Counter = endCounter
                    });
        }

        public async Task AssertShiftSales(SaleStorage st)
        {
            await Post($"{Shift}/assertShiftSales", st);
        }
    }
}
