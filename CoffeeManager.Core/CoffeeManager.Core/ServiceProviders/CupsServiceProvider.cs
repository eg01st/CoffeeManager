﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;
using Newtonsoft.Json;

namespace CoffeeManager.Core.ServiceProviders
{
    public class CupsServiceProvider : BaseServiceProvider
    {
        private const string Cups = "cups";

        public async Task<CupType[]> GetSupportedCups()
        {
            return await Get<CupType[]>(Cups);
        }

        public async Task UtilizeCup(int id, int shiftId)
        {
            await
                PutInternal(Cups,
                    JsonConvert.SerializeObject(new UtilizedCup()
                    {
                        CupTypeId = id,
                        DateTime = DateTime.Now,
                        CoffeeRoomNo = CoffeeRoomNo,
                        ShiftId = shiftId
                    }));
        }
    }
}
