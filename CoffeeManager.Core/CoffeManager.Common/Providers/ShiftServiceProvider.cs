using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Common;
using RestSharp.Portable;

namespace CoffeManager.Common
{
    public class ShiftServiceProvider : ServiceBase, IShiftServiceProvider
    {
        public async Task<int> StartUserShift(int userId, int counter)
        {
            var request = CreatePostRequest(RoutesConstants.Shift);
            request.Parameters.Add(new RestSharp.Portable.Parameter() { Name = nameof(userId), Value = userId});
            request.Parameters.Add(new RestSharp.Portable.Parameter() { Name = nameof(counter), Value = counter });

            var response = await ExecuteRequestAsync<Shift>(request);
            return response.Id;

            //var result =
            //    await
            //    Post<Shift, object>(RoutesConstants.Shift, null,
            //            new Dictionary<string, string>()
            //            {
            //                {nameof(userId), userId.ToString()},
            //                {nameof(counter), counter.ToString()}
            //            });
            //return result.Id;
        }


        public async Task<Shift> GetCurrentShift()
        {
            var request = CreateGetRequest(RoutesConstants.GetCurrentShift);
            return await ExecuteRequestAsync<Shift>(request);
            //return await Get<Shift>(RoutesConstants.GetCurrentShift);
        }

        public async Task<Shift> GetCurrentShiftForCoffeeRoom(int forCoffeeRoom)
        {
            var request = CreateGetRequest(RoutesConstants.GetCurrentShiftForCoffeeRoom);
            request.Parameters.Add(new RestSharp.Portable.Parameter() { Name = nameof(forCoffeeRoom), Value = forCoffeeRoom });
            return await ExecuteRequestAsync<Shift>(request);
            
           // return await Get<Shift>(RoutesConstants.GetCurrentShiftForCoffeeRoom, new Dictionary<string, string>() { { nameof(forCoffeeRoom), forCoffeeRoom.ToString() } });
        }

        public async Task<Sale[]> GetCurrentShiftSales()
        {
            var request = CreateGetRequest(RoutesConstants.GetShiftSales);
            return await ExecuteRequestAsync<Sale[]>(request);
           // return await Get<Sale[]>(RoutesConstants.GetShiftSales);
        }

        public async Task<EndShiftUserInfo> EndShift(int shiftId, decimal realAmount, int endCounter)
        {
            var request = CreatePostRequest(RoutesConstants.EndShift);
            request.AddBody(new EndShiftDTO()
            {
                CoffeeRoomNo = Config.CoffeeRoomNo,
                ShiftId = shiftId,
                RealAmount = realAmount,
                Counter = endCounter
            });

            return await ExecuteRequestAsync<EndShiftUserInfo>(request);

            //return await
                //Post<EndShiftUserInfo, EndShiftDTO>(RoutesConstants.EndShift,
                     //new EndShiftDTO()
                     //{
                     //   CoffeeRoomNo = Config.CoffeeRoomNo,
                     //    ShiftId = shiftId,
                     //    RealAmount = realAmount,
                     //    Counter = endCounter
                     //});
        }



        public async Task<ShiftInfo[]> GetShifts(int skip)
        {
            var request = CreateGetRequest(RoutesConstants.GetShifts);
            request.Parameters.Add(new RestSharp.Portable.Parameter() { Name = nameof(skip), Value = skip });

            return await ExecuteRequestAsync<ShiftInfo[]>(request);
            //return await Get<ShiftInfo[]>(RoutesConstants.GetShifts, new Dictionary<string, string>() { { nameof(skip), skip.ToString() } });
        }

        public async Task<Sale[]> GetShiftSales(int id)
        {
            var request = CreateGetRequest(RoutesConstants.GetShiftSalesById);
            request.Parameters.Add(new RestSharp.Portable.Parameter() { Name = nameof(id), Value = id });

            return await ExecuteRequestAsync<Sale[]>(request);
            //return await Get<Sale[]>(RoutesConstants.GetShiftSalesById, new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task<ShiftInfo> GetShiftInfo(int id)
        {
            var request = CreateGetRequest(RoutesConstants.GetShiftInfo);
            request.Parameters.Add(new RestSharp.Portable.Parameter() { Name = nameof(id), Value = id });

            return await ExecuteRequestAsync<ShiftInfo>(request);
           // return await Get<ShiftInfo>(RoutesConstants.GetShiftInfo, new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

    }
}
