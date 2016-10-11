﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeManager.Api.Mappers
{
    public static class DTOMapper
    {
        public static Models.User ToDTO(this User dbUser)
        {
            return new Models.User() { CoffeeRoomNo = dbUser.CoffeeRoomNo.Value, Name = dbUser.Name, Id = dbUser.Id};
        }

        public static Models.Product ToDTO(this Product prodDb)
        {
            return new Models.Product() { Id = prodDb.Id, Name = prodDb.Name, Price = prodDb.Price,  PolicePrice = prodDb.PolicePrice, ProductType = prodDb.ProductType.Value, CupType = prodDb.CupType.Value };
        }

        public static Models.ExpenseType ToDTO(this ExpenseType expenseDb)
        {
            return new Models.ExpenseType() { Id = expenseDb.Id, Name = expenseDb.Name, CoffeeRoomNo = expenseDb.CoffeeRoomNo.Value};
        }

        public static Models.CupType ToDTO(this CupType cupDb)
        {
            return new Models.CupType() { Id = cupDb.Id, Name = cupDb.Name, CoffeeRoomNo = cupDb.CoffeeRoomNo.Value };
        }

        public static Models.Sale ToDTO(this Sale saleDb)
        {
            return new Models.Sale() { Id = saleDb.Id, Product1 = new Models.Product() {Name = saleDb.Product1.Name, ProductType = saleDb.Product1.ProductType.Value}, CoffeeRoomNo = saleDb.CoffeeRoomNo, Amount = saleDb.Amount, ShiftId = saleDb.ShiftId, IsPoliceSale = saleDb.IsPoliceSale.Value, Time = saleDb.Time};
        }
    }
}