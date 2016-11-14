using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeManager.Api.Mappers
{
	public static class DTOMapper
	{
		public static Models.User ToDTO (this User dbUser)
		{
			return new Models.User () { CoffeeRoomNo = dbUser.CoffeeRoomNo.Value, Name = dbUser.Name, Id = dbUser.Id };
		}

		public static Models.Product ToDTO (this Product prodDb)
		{
			return new Models.Product () { Id = prodDb.Id, Name = prodDb.Name, Price = prodDb.Price, PolicePrice = prodDb.PolicePrice, ProductType = prodDb.ProductType.Value, CupType = prodDb.CupType.Value, SuplyId = prodDb.SuplyProductId };
		}

		public static Models.ExpenseType ToDTO (this ExpenseType expenseDb)
		{
			return new Models.ExpenseType () { Id = expenseDb.Id, Name = expenseDb.Name, CoffeeRoomNo = expenseDb.CoffeeRoomNo.Value };
		}

		public static Models.CupType ToDTO (this CupType cupDb)
		{
			return new Models.CupType () { Id = cupDb.Id, Name = cupDb.Name, CoffeeRoomNo = cupDb.CoffeeRoomNo.Value };
		}

		public static Models.Sale ToDTO (this Sale saleDb)
		{
		    return new Models.Sale()
		    {
		        Id = saleDb.Id,
		        Product1 = new Models.Product() {Name = saleDb.Product1.Name, ProductType = saleDb.Product1.ProductType.Value},
		        CoffeeRoomNo = saleDb.CoffeeRoomNo,
		        Amount = saleDb.Amount,
		        ShiftId = saleDb.ShiftId,
		        IsPoliceSale = saleDb.IsPoliceSale.Value,
                IsUtilized = saleDb.IsUtilized,
                IsRejected = saleDb.IsRejected,
		        Time = saleDb.Time
		    };
		}

		public static Models.SupliedProduct ToDTO (this SupliedProduct productDb)
		{
			return new Models.SupliedProduct () { Id = productDb.Id, Name = productDb.Name, CoffeeRoomNo = productDb.CoffeeRoomNo.Value, Quatity = productDb.Quantity, Price = productDb.Price };
		}

		public static Models.Expense ToDTO (this Expense expenseDb)
		{
			return new Models.Expense () { Id = expenseDb.Id, Name = expenseDb.ExpenseType1.Name, CoffeeRoomNo = expenseDb.CoffeeRoomNo.Value, Amount = expenseDb.Amount };
		}

		public static Models.UsedCupPerShift ToDTO (this UsedCupsPerShift cupDb)
		{
			return new Models.UsedCupPerShift () { Id = cupDb.Id, CoffeeRoomNo = cupDb.CoffeeRoomNo, C110 = cupDb.C110, C170 = cupDb.C170, C250 = cupDb.C250, C400 = cupDb.C400, Plastic = cupDb.Plastic };
		}

        public static Models.Order ToDTO(this SuplyOrder orderDb)
        {
            return new Models.Order() { Id = orderDb.Id, CoffeeRoomNo = orderDb.CoffeeRoomNo, Date = orderDb.Date, IsDone = orderDb.IsDone, Price = orderDb.Price };
        }

        public static Models.OrderItem ToDTO(this SuplyOrderItem orderItemDb)
        {
            var item = new Models.OrderItem() { Id = orderItemDb.Id, CoffeeRoomNo = orderItemDb.CoffeeRoomNo, Quantity = orderItemDb.Quantity, IsDone = orderItemDb.IsDone, Price = orderItemDb.Price, OrderId = orderItemDb.SuplyOrderId };
            if(orderItemDb.SupliedProduct != null)
            {
                item.SuplyProductName = orderItemDb.SupliedProduct.Name;
            }
            return item;
        }
    }
}