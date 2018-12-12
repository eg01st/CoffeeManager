using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Quartz;

namespace CoffeeManager.Api.BackgroundJob
{
    public class AutoOrderJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var ordersStrings = new List<string>();
            var ordersHistories = new List<SuplyProductAutoOrdersHistory>();
            
            var entities = new CoffeeRoomEntities();
            var orders = entities.AutoOrders.Include(o => o.SuplyProductOrderItems).ToList();
            foreach (var order in orders)
            {
                if (order.IsActive && order.DayOfWeek == DateTime.Now.DayOfWeek &&
                    order.Time.Hours == DateTime.Now.Hour)
                {                    
                    var supliedProductsToOrder = order.SuplyProductOrderItems;
                    var currentSupliedProducts = entities.SuplyProductQuantities
                        .Include(o => o.SupliedProduct)
                        .Where(c => c.CoffeeRoomId == order.CoffeeRoomId)
                        .ToList();
                    foreach (var orderItem in supliedProductsToOrder)
                    {
                        var product =
                            currentSupliedProducts.FirstOrDefault(c => c.SuplyProductId == orderItem.SuplyProductId);
                        if (product != null)
                        {
                            var multiplier = (int)product.SupliedProduct.ExpenseNumerationMultyplier;
                            var diff = orderItem.QuantityShouldBeAfterOrder - (int)product.Quantity;
                            if (diff < 0)
                            {
                                continue;
                            }
                            var roundedCount = (diff / multiplier) + 1;
                            var quantityToOrder = multiplier * roundedCount;
                            ordersStrings.Add($"{product.SupliedProduct.Name} : {roundedCount} {product.SupliedProduct.ExpenseNumerationName}");
                            var historyItem = new SuplyProductAutoOrdersHistory()
                            {
                                SuplyProductId = product.SuplyProductId,
                                OrderedQuantity = quantityToOrder,
                                QuantityBefore = (int) product.Quantity
                            };
                            ordersHistories.Add(historyItem);
                        }
                    }

                    var coffeeRoom = entities.CoffeeRooms.First(c => c.Id == order.CoffeeRoomId);

                    string message = string.Format(Constants.AutoOrderMessage, coffeeRoom.Name,
                        string.Join("\n", ordersStrings));
                    SendEmail(message);
                    
                    var orderHistory = new AutoOrdersHistory();
                    orderHistory.CoffeeRoomId = order.CoffeeRoomId;
                    orderHistory.OrderDate = DateTime.Now;
                    orderHistory.OrderId = order.Id;
                    orderHistory.SuplyProductAutoOrdersHistories = ordersHistories;
                    entities.AutoOrdersHistories.Add(orderHistory);
                    entities.SaveChanges();
                }
            }
        }
        
        public void SendEmail(string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add("tertyshnykov@gmail.com");
                mail.From = new MailAddress("coffeemanager221@gmail.com");
                mail.Subject = "Заказ стаканов";

                mail.Body = body;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                smtp.Credentials = new System.Net.NetworkCredential
                    ("coffeemanager221@gmail.com", "Q!W@E#R$"); // ***use valid credentials***
                smtp.Port = 587;

                //Or your Smtp Email ID and Password
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Log.Warn("Exception in sendEmail:" + ex.Message);
            }
        }
    }
}