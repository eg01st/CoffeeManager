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
				Product = sale.ProductId,
				ShiftId = sale.ShiftId,
				Time = sale.Time,
                IsCreditCardSale = sale.IsCreditCardSale,
                IsSaleByWeight = sale.IsSaleByWeight,
                Weight = sale.Weight
			};

		}

        public static User Map(Models.User user)
        {
            return new User()
            {
                Name = user.Name,
                CoffeeRoomNo = user.CoffeeRoomNo,
                CurrentEarnedAmount = user.CurrentEarnedAmount,
                DayShiftPersent = user.DayShiftPersent,
                EntireEarnedAmount = user.EntireEarnedAmount,
                ExpenceId = user.ExpenceId,
                NightShiftPercent = user.NightShiftPercent,
                IsActive = user.IsActive,
                MinimumPayment = user.MinimumPayment
            };

        }

        public static User Update(Models.User user, User userDb)
        {
            userDb.Name = user.Name;
            userDb.NightShiftPercent = user.NightShiftPercent;
            userDb.DayShiftPersent = user.DayShiftPersent;
            userDb.IsActive = user.IsActive;
            userDb.ExpenceId= user.ExpenceId;
            userDb.SimplePayment = user.SalaryRate;
            userDb.MinimumPayment = user.MinimumPayment;
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
                IsActive = product.IsActive,
                IsSaleByWeight = product.IsSaleByWeight
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

	    public static SupliedProduct Update(Models.SupliedProduct sProduct, SupliedProduct dbProd)
	    {
	        dbProd.Quantity = sProduct.Quatity;
            dbProd.ExpenseNumerationMultyplier = sProduct.ExpenseNumerationMultyplier;
	        dbProd.ExpenseNumerationName = sProduct.ExpenseNumerationName;
	        dbProd.InventoryNumerationMultyplier = sProduct.InventoryNumerationMultyplier;
	        dbProd.InventoryNumerationName = sProduct.InventoryNumerationName;
	        dbProd.Price = sProduct.Price;
	        return dbProd;
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

        public static UtilizedSuplyProduct Map(this Models.UtilizedSuplyProduct product)
        {
            return new UtilizedSuplyProduct()
            {
                Id = product.Id,
                SuplyProductId = product.SuplyProductId,
                Quantity = product.Quantity,
                Reason = product.Reason,
                CoffeeRoomNo = product.CoffeeRoomNo,
                ShiftId = product.ShiftId,
                DateTime = product.Date
            };
        }
    }
}