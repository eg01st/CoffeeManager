using System;
namespace CoffeeManager.Models
{
    public class RoutesConstants
    {
        public const string Api = "api";

        public const string Payment = Api + "/payment";
        public const string GetCurrentShiftMoney = Payment + "/getcurrentshiftmoney";
        public const string GetEntireMoney = Payment + "/getentiremoney";
        public const string GetCreditCardEntireMoney = Payment + "/GetCreditCardEntireMoney";
        public const string SetCreditCardEntireMoney = Payment + "/SetCreditCardEntireMoney";
        public const string GetExpenseItems = Payment + "/getexpenseitems";
        public const string RemoveExpenseType = Payment + "/removeExpenseType";
        public const string ToggleExpenseEnabled = Payment + "/toggleExpenseEnabled";
        public const string MapExpenseToSuplyProduct = Payment + "/mapExpenseToSuplyProduct";
        public const string GetMappedSuplyProductsToExpense = Payment + "/getMappedSuplyProductsToExpense";
        public const string RemoveMappedSuplyProductsToExpense = Payment + "/removeMappedSuplyProductsToExpense";
        public const string AddExpenseExtended = Payment + "/addExpenseExtended";
        public const string GetExpenseDetails = Payment + "/getExpenseDetails";

        public const string DeleteExpenseItem = Payment + "/deleteexpenseItem";
        public const string AddNewExpenseType = Payment + "/addnewexpensetype";
        public const string GetShiftExpenses = Payment + "/getShiftExpenses";
        public const string GetExpenses = Payment + "/getExpenses";
        public const string GetSalesByDate = Payment + "/getSalesByDateDate";
        public const string CashOutCreditCard = Payment + "/CashOutCreditCard";
        public const string GetCashOutHistory = Payment + "/GetCashOutHistory";
        

        public const string Products = Api + "/products";
        public const string GetAllProducts = Products + "/getAll";
        public const string AddProduct = Products + "/addproduct";
        public const string EditProduct = Products + "/editproduct";
        public const string DeleteProduct = Products + "/deleteproduct";
        public const string SaleProduct = Products + "/saleproduct";
        public const string DeleteSale = Products + "/deletesale";
        public const string UtilizeSale = Products + "/UtilizeSale";
        public const string ToggleProductEnabled = Products + "/ToggleProductEnabled";

        public const string Shift = Api + "/shift";
        public const string EndShift = Shift + "/endShift";
        public const string GetCurrentShift = Shift + "/getCurrentShift";
        public const string GetCurrentShiftForCoffeeRoom = Shift + "/GetCurrentShiftForCoffeeRoom";
        public const string GetShifts = Shift + "/getShifts";
        public const string GetShiftSales = Shift + "/getShiftSales";
        public const string GetShiftInfo = Shift + "/getShiftInfo";
        public const string GetShiftSalesById = Shift + "/getShiftSalesById";

        public const string Statistic = Api + "/statistic";
        public const string StatisticGetAllSales = Statistic + "/getAllSales";
        public const string StatisticGetExpenses = Statistic + "/getExpenses";
        public const string StatisticGetCreditCardSales = Statistic + "/getCreditCardSales";
        public const string StatisticGetSalesByName = Statistic + "/getSalesByName";

        public const string SuplyOrder = Api + "/suplyorder";
        public const string GetOrders = SuplyOrder + "/getorders";
        public const string GetOrderItems = SuplyOrder + "/getorderitems";
        public const string SaveOrderItem = SuplyOrder + "/saveorderitem";
        public const string CreateOrderItem = SuplyOrder + "/createorderitem";
        public const string CreateOrder = SuplyOrder + "/createorder";
        public const string CloseOrder = SuplyOrder + "/closeorder";
        public const string DeleteOrder = SuplyOrder + "/deleteorder";
        public const string DeleteOrderItem = SuplyOrder + "/deleteorderitem";

        public const string SuplyProducts = Api + "/suplyproducts";
        public const string GetSuplyProducts = SuplyProducts + "/getsuplyproducts";
        public const string GetSuplyProduct = SuplyProducts + "/getsuplyproduct";
        public const string EditSuplyProduct = SuplyProducts + "/editSuplyProduct";
        public const string AddSuplyProduct = SuplyProducts + "/AddSuplyProduct";
        public const string DeleteSuplyProduct = SuplyProducts + "/deletesuplyproduct";
        public const string GetProductCalculationItems = SuplyProducts + "/getproductcalculationitems";
        public const string DeleteProductCalculationItem = SuplyProducts + "/deleteproductcalculationitem";
        public const string AddProductCalculationItem = SuplyProducts + "/addproductcalculationitem";
        public const string UtilizeSuplyProduct = SuplyProducts + "/utilizeSuplyProduct";
        public const string GetUtilizedSuplyProducts = SuplyProducts + "/getUtilizedSuplyProducts";
        public const string TransferSuplyProduct = SuplyProducts + "/TransferSuplyProduct";
        

        public const string Users = Api + "/users";
        public const string GetUsers = Users + "/getUsers";
        public const string GetUser = Users + "/getUser";
        public const string AddUser = Users + "/addUser";
        public const string UpdateUser = Users + "/updateUser";
        public const string PaySalary = Users + "/paySalary";
        public const string ToggleUserEnabled = Users + "/toggleUserEnabled";
        public const string Login = Users + "/login";
        public const string DeleteUser = Users + "/deleteUser";
        public const string PenaltyUser = Users + "/penaltyUser";
        public const string DismisPenalty = Users + "/dismisPenalty";
        public const string GetSalaryAmountToPay = Users + "/GetSalaryAmountToPay";

        public const string Inventory = Api + "/inventory";
        public const string GetInventoryItems = Inventory + "/getInventoryItems";
        public const string SentInventoryInfo = Inventory + "/sentInventoryInfo";
        public const string ToggleItemInventoryEnabled = Inventory + "/toggleItemInventoryEnabled";
        public const string GetInventoryReports = Inventory + "/getInventoryReports";
        public const string GetInventoryReportDetails = Inventory + "/getInventoryReportDetails";

        public const string Admin = Api + "/admin";
        public const string GetCoffeeRooms = Admin + "/getCoffeeRooms";
        public const string AddCoffeeRoom = Admin + "/AddCoffeeRoom";
        public const string DeleteCoffeeRoom = Admin + "/DeleteCoffeeRoom";
        public const string SetCoffeePortionWeight = Admin + "/SetCoffeePortionWeight";
        public const string GetCoffeePortionWeight = Admin + "/GetCoffeePortionWeight";


        public const string Account = Api + "/account";
        public const string GetAdminUsers = Account + "/GetAdminUsers";
        public const string GetUserInfo = Account + "/GetUserInfo";
        public const string Logout = Account + "/Logout";
        public const string ChangePassword = Account + "/ChangePassword";
        public const string SetPassword = Account + "/SetPassword";
        public const string RemoveLogin = Account + "/RemoveLogin";
        public const string Register = Account + "/Register";
        public const string DeleteAdminUser = Account + "/DeleteAdminUser";

        public const string Token = "Token";
        
        public const string Update = Api + "/update";
        public const string GetCurrentAdnroidVersion = Update + "/getcurrentversion";
        public const string GetAndroidPackage = Update + "/GetAndroidPackage";
    }
}
