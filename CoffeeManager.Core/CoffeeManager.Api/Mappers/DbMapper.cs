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
				Time = sale.Time,
                IsCreditCardSale = sale.IsCreditCardSale
			};

		}

        public static User Map(Models.User user)
        {
            return new User()
            {
                Name = user.Name,
                CoffeeRoomNo = user.CoffeeRoomNo,
            };

        }

        public static User Update(Models.User user, User userDb)
        {
            userDb.Name = user.Name;
            userDb.NightShiftPercent = user.NightShiftPercent;
            userDb.DayShiftPersent = user.DayShiftPersent;
            userDb.IsActive = user.IsActive;
            userDb.ExpenceId= user.ExpenceId;
            return userDb;
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
			if (product.SuplyId.HasValue) {
				prod.SuplyProductId = product.SuplyId;
			}
			return prod;

		}

		public static Expense Map (Models.Expense expense)
		{
			return new Expense () {
				CoffeeRoomNo = expense.CoffeeRoomNo,
				ShiftId = expense.ShiftId,
				Amount = expense.Amount,
				ExpenseType = expense.ExpenseId,
                Quantity = expense.ItemCount
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
				Quantity = 0,
				CoffeeRoomNo = sProduct.CoffeeRoomNo,
				Name = sProduct.Name,
				Price = sProduct.Price
			};
		}
	}
}