using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoffeeManager.Core
{
    public class Bot
    {
        public static void SendMessage(string message)
        {
            var Bot = new Telegram.Bot.TelegramBotClient("‎‎299668169:AAFE_gJyB7T9GbX_YymkC17nCohXCMgNVXo");
            var me = Bot.SendTextMessageAsync("@CoffeeRoom", "test").Result;
        }
    }
}
