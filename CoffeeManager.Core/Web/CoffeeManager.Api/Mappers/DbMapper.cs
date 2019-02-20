using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.AutoOrder;
using CoffeeManager.Models.Data.DTO.AutoOrder.History;
using CoffeeManager.Models.Data.DTO.Category;
using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;
using CoffeeManager.Models.Data.DTO.User;
using CoffeeManager.Models.Data.Product;

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

        public static User Map(UserDTO user)
        {
            var us = new User()
            {
                Name = user.Name,
                CoffeeRoomNo = user.CoffeeRoomNo,
                CurrentEarnedAmount = user.CurrentEarnedAmount,
                EntireEarnedAmount = user.EntireEarnedAmount,
                ExpenceId = user.ExpenceId,
                IsActive = user.IsActive,
            };
            if (user.PaymentStrategies != null && user.PaymentStrategies.Any())
            {
                us.UserPaymentStrategies = user.PaymentStrategies.Select(s => Map(s)).ToArray();
            }
            return us;
        }

	    public static UserPaymentStrategy Map(Models.UserPaymentStrategy strategy)
	    {
	        return new UserPaymentStrategy
	        {
                UserId = strategy.UserId,
                CoffeeRoomId = strategy.CoffeeRoomId,
                DayShiftPersent = strategy.DayShiftPersent,
                NightShiftPercent = strategy.NightShiftPercent,
                MinimumPayment = strategy.MinimumPayment,
                SimplePayment = strategy.SimplePayment
	        };
	    }


        public static User Update(UserDTO user, User userDb)
        {
            userDb.Name = user.Name;
            userDb.IsActive = user.IsActive;
            userDb.ExpenceId= user.ExpenceId;

            if (user.PaymentStrategies != null && user.PaymentStrategies.Any())
            {
                foreach (var strategy in user.PaymentStrategies)
                {
                    var strategyDb = userDb.UserPaymentStrategies.FirstOrDefault(s => s.Id == strategy.Id);
                    if (strategyDb == null)
                    {
                        var newStrategy = Map(strategy);
                        userDb.UserPaymentStrategies.Add(newStrategy);
                    }
                    else
                    {
                        Update(strategy, strategyDb);
                    }
                }
               // userDb.UserPaymentStrategies = user.PaymentStrategies.Select(s => Map(s)).ToArray();
            }
            return userDb;
        }


        public static Product Map(this ProductDTO productDTO)
		{

			var prod = new Product () {
				CoffeeRoomNo = productDTO.CoffeeRoomNo,
				CupType = productDTO.CupType,
				Name = productDTO.Name,
				PolicePrice = productDTO.PolicePrice,
				Price = productDTO.Price,
                IsActive = productDTO.IsActive,
                IsSaleByWeight = productDTO.IsSaleByWeight,
				CategoryId = productDTO.CategoryId,
                Color = productDTO.Color,
                Description = productDTO.Description,
                IsPercentPaymentEnabled = productDTO.IsPercentPaymentEnabled
			};
			if (productDTO.SuplyId.HasValue) {
				prod.SuplyProductId = productDTO.SuplyId;
			}
			return prod;

		}
		
		public static Product Map(this ProductDetaisDTO productDTO)
		{

			var prod = new Product () {
				CoffeeRoomNo = productDTO.CoffeeRoomNo,
				CupType = productDTO.CupType,
				Name = productDTO.Name,
				IsActive = productDTO.IsActive,
				IsSaleByWeight = productDTO.IsSaleByWeight,
				CategoryId = productDTO.CategoryId,
				Color = productDTO.Color,
				Description = productDTO.Description,
				IsPercentPaymentEnabled = productDTO.IsPercentPaymentEnabled
			};
			if (productDTO.SuplyId.HasValue)
            {
				prod.SuplyProductId = productDTO.SuplyId;
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
				Name = sProduct.Name,
				Price = sProduct.Price,
                ExpenseNumerationName = "Штуки",
                InventoryNumerationName = "Штуки",
                ExpenseNumerationMultyplier = 1,
                InventoryNumerationMultyplier = 1               
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

	    public static void Update(Models.UserPaymentStrategy strategy, UserPaymentStrategy strategyDb)
	    {

	        strategyDb.UserId = strategy.UserId;
	        strategyDb.CoffeeRoomId = strategy.CoffeeRoomId;
	        strategyDb.DayShiftPersent = strategy.DayShiftPersent;
	        strategyDb.NightShiftPercent = strategy.NightShiftPercent;
	        strategyDb.MinimumPayment = strategy.MinimumPayment;
	        strategyDb.SimplePayment = strategy.SimplePayment;
	    }

	    public static Category Map(this CategoryDTO category)
	    {
	        return new Category()
	        {
	            Id = category.Id,
	            CoffeeRoomNo = category.CoffeeRoomNo,
	            Name = category.Name,
	        };
	    }

	    public static void Update(CategoryDTO category, Category categoryyDb)
	    {
	        categoryyDb.Name = category.Name;
	        categoryyDb.ParentId = category.ParentId;
	    }

	    public static CoffeeCounterForCoffeeRoom Map(this CoffeeCounterForCoffeeRoomDTO counter)
	    {
	        return new CoffeeCounterForCoffeeRoom()
	        {
	            Id = counter.Id,
	            CoffeeRoomNo = counter.CoffeeRoomNo,
	            Name = counter.Name,
                CategoryId = counter.CategoryId,
                SuplyProductId = counter.SuplyProductId
	        };
	    }

	    public static void Update(CoffeeCounterForCoffeeRoomDTO counter, CoffeeCounterForCoffeeRoom counterDb)
	    {
	        counterDb.Name = counter.Name;
	        counterDb.CategoryId = counter.CategoryId;
	        counterDb.SuplyProductId = counter.SuplyProductId;
        }

	    public static ProductPaymentStrategy Map(this ProductPaymentStrategyDTO dto)
	    {
	        return new ProductPaymentStrategy()
	        {
	            Id = dto.Id,
                ProductId = dto.ProductId,  
	            CoffeeRoomId = dto.CoffeeRoomId,
	            DayShiftPercent = dto.DayShiftPercent,
	            NightShiftPercent = dto.NightShiftPercent
	        };  
	    }

	    public static ProductPrice Map(this ProductPriceDTO dto)
	    {
	        return new ProductPrice()
	        {
	            Id = dto.Id,
	            ProductId = dto.ProductId,
	            CoffeeRoomNo = dto.CoffeeRoomNo,
	            Price = dto.Price,
                DiscountPrice = dto.DiscountPrice
	        };
	    }
		
		public static AutoOrder Map(this AutoOrderDTO dto)
		{
			return new AutoOrder()
			{
				Id = dto.Id,
				CoffeeRoomId = dto.CoffeeRoomId,
				DayOfWeek = dto.DayOfWeek,
				Time = dto.OrderTime,
				IsActive = dto.IsActive,
				EmailToSend = dto.EmailToSend,
				Subject = dto.Subject,
				CCToSend = dto.CCToSend,
				SenderEmail = dto.SenderEmail,
				SenderEmailPassword = dto.SenderEmailPassword,
				SuplyProductOrderItems = dto.OrderItems.Select(s => s.Map()).ToList()
			};
		}
		
		public static AutoOrder Update(AutoOrderDTO order, AutoOrder dbOrder)
		{
			dbOrder.DayOfWeek = order.DayOfWeek;
			dbOrder.Time = order.OrderTime;
			dbOrder.IsActive = order.IsActive;
			dbOrder.Subject = order.Subject;
			dbOrder.EmailToSend = order.EmailToSend;
			dbOrder.CCToSend = order.CCToSend;
			dbOrder.SenderEmail = order.SenderEmail;
			dbOrder.SenderEmailPassword = order.SenderEmailPassword;
			return dbOrder;
		}
		
		public static SuplyProductOrderItem Map(this SuplyProductToOrderItemDTO dto)
		{
			return new SuplyProductOrderItem()
			{
				Id = dto.Id,
                OrderId = dto.OrderId,
				QuantityShouldBeAfterOrder = dto.QuantityShouldBeAfterOrder,
				SuplyProductId = dto.SuplyProductId,
				ShouldUpdateQuantityBeforeOrder = dto.ShouldUpdateQuantityBeforeOrder
			};
		}
    }
}