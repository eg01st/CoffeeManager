using System;
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
            return new Models.Product() { Id = prodDb.Id, Name = prodDb.Name, Price = prodDb.Price, PolicePrice = prodDb.PolicePrice, ProductType = prodDb.ProductType.Value};
        }

        public static Models.ExpenseType ToDTO(this ExpenseType expenseDb)
        {
            return new Models.ExpenseType() { Id = expenseDb.Id, Name = expenseDb.Name, CoffeeRoomNo = expenseDb.CoffeeRoomNo.Value};
        }
    }
}