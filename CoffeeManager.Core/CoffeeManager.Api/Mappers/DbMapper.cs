using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeManager.Api.Mappers
{
    public static class DbMapper
    {
        public static Sale Map(Models.Sale sale)
        {
            return new Sale() { Amount = sale.Amount, CoffeeRoomNo = sale.CoffeeRoomNo, IsPoliceSale = sale.IsPoliceSale, Product = sale.Product, ShiftId = sale.ShiftId, Time = sale.Time };

        }

        public static Expense Map(Models.Expense expense)
        {
            return new Expense() {CoffeeRoomNo = expense.CoffeeRoomNo, ShiftId = expense.ShiftId, Amount = expense.Amount, ExpenseType = expense.ExpenseId};

        }
    }
}