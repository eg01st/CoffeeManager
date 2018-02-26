using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.User;

namespace CoffeeManager.Api.Mappers
{
    public static class DTOMapper
    {
        public static UserDTO ToDTO(this User dbUser)
        {
            var user = new UserDTO()
            {
                CoffeeRoomNo = dbUser.CoffeeRoomNo.Value,
                Name = dbUser.Name,
                Id = dbUser.Id,
                CurrentEarnedAmount = dbUser.CurrentEarnedAmount,
                EntireEarnedAmount = dbUser.EntireEarnedAmount,
                ExpenceId = dbUser.ExpenceId,
                IsActive = dbUser.IsActive,
            };
            if(dbUser.UserPenalties != null && dbUser.UserPenalties.Any())
            {
                var penalties = new List<Models.UserPenalty>();
                foreach (var penalty in dbUser.UserPenalties)
                {
                    penalties.Add(new Models.UserPenalty()
                    {
                        Amount = penalty.Amount,
                        Date = penalty.Date,
                        Reason = penalty.Reason,
                        Id = penalty.Id,
                        UserId = penalty.UserId
                    });
                }
                user.Penalties = penalties.ToArray();
            }
            if (dbUser.UserEarningsHistories != null && dbUser.UserEarningsHistories.Any())
            {
                var earnings = new List<Models.UserEarningsHistory>();
                foreach (var earning in dbUser.UserEarningsHistories)
                {
                    earnings.Add(new Models.UserEarningsHistory()
                    {
                        Amount = earning.Amount,
                        Date = earning.Date,
                        IsDayShift = earning.IsDayShift,
                        Id = earning.Id,
                        UserId = earning.UserId,
                         ShiftId = earning.ShiftId
                    });
                }
                user.Earnings = earnings.ToArray();
            }

            if (dbUser.UserPaymentStrategies != null && dbUser.UserPaymentStrategies.Any())
            {
                user.PaymentStrategies = dbUser.UserPaymentStrategies.ToList().Select(s => s.ToDTO()).ToArray();
            }
            return user;
        }

        public static Models.UserPaymentStrategy ToDTO(this UserPaymentStrategy item)
        {
            return new Models.UserPaymentStrategy()
            {
                Id = item.Id,
                UserId = item.UserId,
                CoffeeRoomId = item.CoffeeRoomId,
                DayShiftPersent = item.DayShiftPersent,
                NightShiftPercent = item.NightShiftPercent,
                SimplePayment = item.SimplePayment,
                MinimumPayment = item.MinimumPayment,
            };
        }

        public static Models.Product ToDTO(this Product prodDb)
        {
            return new Models.Product()
            {
                Id = prodDb.Id,
                Name = prodDb.Name,
                Price = prodDb.Price,
                PolicePrice = prodDb.PolicePrice,
                ProductType = prodDb.ProductType.Value,
                CupType = prodDb.CupType.Value,
                SuplyId = prodDb.SuplyProductId,
                IsActive = prodDb.IsActive,
                IsSaleByWeight = prodDb.IsSaleByWeight
            };
        }

        public static Models.ExpenseType ToDTO(this ExpenseType expenseDb , int coffeeroomno)
        {
            return new Models.ExpenseType()
            {
                Id = expenseDb.Id,
                Name = expenseDb.Name,
                CoffeeRoomNo = expenseDb.CoffeeRoomNo.Value,
                IsActive = expenseDb.IsActive,
                SuplyProducts = expenseDb.SupliedProducts.Select(s => s.ToDTO(coffeeroomno)).ToArray()
            };
        }

        public static Models.CupType ToDTO(this CupType cupDb)
        {
            return new Models.CupType() { Id = cupDb.Id, Name = cupDb.Name, CoffeeRoomNo = cupDb.CoffeeRoomNo.Value };
        }

        public static Models.Sale ToDTO(this Sale saleDb)
        {
            return new Models.Sale()
            {
                Id = saleDb.Id,
                ProductTypeId = saleDb.Product1.ProductType.Value,
                ProductName = saleDb.Product1.Name,
                CoffeeRoomNo = saleDb.CoffeeRoomNo,
                Amount = saleDb.Amount,
                ShiftId = saleDb.ShiftId,
                IsPoliceSale = saleDb.IsPoliceSale.Value,
                IsUtilized = saleDb.IsUtilized,
                IsRejected = saleDb.IsRejected,
                Time = saleDb.Time,
                IsCreditCardSale = saleDb.IsCreditCardSale,
                IsSaleByWeight = saleDb.IsSaleByWeight,
                Weight = saleDb.Weight
            };
        }

        public static Models.SupliedProduct ToDTO(this SupliedProduct productDb, int coffeeroomno)
        {
            return new Models.SupliedProduct()
            {
                Id = productDb.Id,
                Name = productDb.Name,
                Quatity = productDb.SuplyProductQuantities.FirstOrDefault(q => q.CoffeeRoomId == coffeeroomno)?.Quantity ?? 0,
                Price = productDb.Price,
                ExpenseTypeId = productDb.ExprenseTypeId,
                ExpenseTypeName = productDb.ExpenseType?.Name,
                InventoryEnabled = productDb.InventoryEnabled,
                ExpenseNumerationMultyplier = productDb.ExpenseNumerationMultyplier,
                ExpenseNumerationName = productDb.ExpenseNumerationName,
                InventoryNumerationName = productDb.InventoryNumerationName,
                InventoryNumerationMultyplier = productDb.InventoryNumerationMultyplier
            };
        }

        public static Models.Expense ToDTO(this Expense expenseDb)
        {
            var item = new Models.Expense()
            {
                Id = expenseDb.Id,
                Name = expenseDb.ExpenseType1.Name,
                CoffeeRoomNo = expenseDb.CoffeeRoomNo.Value,
                Amount = expenseDb.Amount
            };
            if (expenseDb.Quantity.HasValue)
            {
                item.ItemCount = expenseDb.Quantity.Value;
            }
            return item;
        }

        public static Models.UtilizedSuplyProduct ToDTO(this UtilizedSuplyProduct product)
        {
            return new Models.UtilizedSuplyProduct()
            {
                Id = product.Id,
                SuplyProductId = product.SuplyProductId,
                SuplyProductName = product.SupliedProduct.Name,
                Quantity = product.Quantity,
                Reason = product.Reason,
                CoffeeRoomNo = product.CoffeeRoomNo,
                ShiftId = product.ShiftId,
                Date = product.DateTime
            };       
        }

        public static Models.Order ToDTO(this SuplyOrder orderDb)
        {
            return new Models.Order() { Id = orderDb.Id, CoffeeRoomNo = orderDb.CoffeeRoomNo, Date = orderDb.Date, IsDone = orderDb.IsDone, Price = orderDb.Price, ExpenseTypeId = orderDb.ExpenseTypeId };
        }

        public static Models.OrderItem ToDTO(this SuplyOrderItem orderItemDb)
        {
            var item = new Models.OrderItem() { Id = orderItemDb.Id, CoffeeRoomNo = orderItemDb.CoffeeRoomNo, Quantity = orderItemDb.Quantity, IsDone = orderItemDb.IsDone, Price = orderItemDb.Price, OrderId = orderItemDb.SuplyOrderId };
            if (orderItemDb.SupliedProduct != null)
            {
                item.SuplyProductName = orderItemDb.SupliedProduct.Name;
            }
            return item;
        }

        public static Models.InventoryReport ToDTO(this InventoryReport report)
        {
            return new Models.InventoryReport() { Id = report.Id,CoffeeRoomNo  = report.CoffeeRoomNo, Date = report.Date };
        }

        public static Models.InventoryItem ToDTO(this InventoryReportItem item)
        {
            return new Models.InventoryItem() { Id = item.Id, CoffeeRoomNo = item.CoffeeRoomNo, QuantityAfer = item.QuantityAfter, QuantityBefore = item.QuantityBefore, QuantityDiff = item.QuantityDiff, SuplyProductId = item.SuplyProductId, SuplyProductName = item.SupliedProduct.Name };
        }

        public static Models.SupliedProduct ToDTO(this ExpenseSuplyProduct item)
        {
            return new Models.SupliedProduct()
            {
                Id = item.Id,
                CoffeeRoomNo = item.CoffeeRoonNo,
                Quatity = item.Quantity,
                Price = item.Amount,
                Name = item.SupliedProduct.Name,
                ExpenseNumerationName = item.SupliedProduct.ExpenseNumerationName,
                ExpenseNumerationMultyplier = item.SupliedProduct.ExpenseNumerationMultyplier,
                 InventoryEnabled = item.SupliedProduct.InventoryEnabled
            };
        }

        public static IEnumerable<Models.MetroExpense> ToDTO(this List<GetMetroExpenses_Result> result)
        {
            foreach (var item in result)
            {
                yield return new Models.MetroExpense() { Productid = item.productid, Name = item.name, Price = item.price, Quantity = item.quantity };
            }
        }

        public static IEnumerable<Models.Expense> ToDTO(this List<GetExpenses_Result> result)
        {
            foreach (var item in result)
            {
                yield return new Models.Expense() { Name = item.Name, Amount = item.amount ?? -1 };
            }
        }

        public static IEnumerable<Models.SaleInfo> ToDTO(this List<GetAllSales_Result> result)
        {
            foreach (var item in result)
            {
                yield return new Models.SaleInfo() { Name = item.name, Amount = item.amount, Quantity = item.quantity, Producttype = item.producttype };
            }
        }

        public static Models.CashoutHistory ToDTO(this CashoutHistory item)
        {
            return new Models.CashoutHistory()
            {
                Id = item.Id,
                CoffeeRoomNo = item.CoffeeRoomNo,
                Amount = item.Amount,
                ShiftId = item.ShiftId,
                Date = item.Date,
            };
        }
    }
}