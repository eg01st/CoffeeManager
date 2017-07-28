using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManager.Core.Messages
{
    public class ExpenseAddedMessage : BaseMessage<Decimal>
    {
        public ExpenseAddedMessage(decimal data, object sender) : base(data, sender)
        {
        }
    }
}
