using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeManager.Api.Mappers
{
	public static class DbMapper
	{
		public static Sale Map (Models.Sale sale)
		{
			return new Sale () {
				Amount = sale.Amount,
				CoffeeRoomNo = sale.CoffeeRoomNo,
				IsPoliceSale = sale.IsPoliceSale,
				Product = sale.Product,
				ShiftId = sale.ShiftId,
				Time = sale.Time
			};

		}

		public static Product Map (Models.Product product)
		{

			var prod = new Product () {
				CoffeeRoomNo = product.CoffeeRoomNo,
				CupType = product.CupType,
				Name = product.Name,
				PolicePrice = product.PolicePrice,
				Price = product.Price,
				ProductType = product.ProductType,
			};
			if (prod.SuplyProductId.HasValue) {
				prod.SuplyProductId = prod.SuplyProductId;
			}
			return prod;

		}

		public static Expense Map (Models.Expense expense)
		{
			return new Expense () {
				CoffeeRoomNo = expense.CoffeeRoomNo,
				ShiftId = expense.ShiftId,
				Amount = expense.Amount,
				ExpenseType = expense.ExpenseId
			};
		}

		public static UtilizedCup Map (Models.UtilizedCup cup)
		{
			return new UtilizedCup () {
				CoffeeRoomNo = cup.CoffeeRoomNo,
				ShiftId = cup.ShiftId,
				CupTypeId = cup.CupTypeId,
				DateTime = cup.DateTime
			};
		}

		public static Dept Map (Models.Dept dept)
		{
			return new Dept () {
				CoffeeRoomNo = dept.CoffeeRoomNo,
				ShiftId = dept.ShiftId,
				Amount = dept.Amount,
				IsPaid = dept.IsPaid
			};
		}

		public static SupliedProduct Map (Models.SupliedProduct sProduct)
		{
			return new SupliedProduct () {
				Amount = 0,
				CoffeeRoomNo = sProduct.CoffeeRoomNo,
				Name = sProduct.Name,
				Price = sProduct.Price
			};
		}
	}
}