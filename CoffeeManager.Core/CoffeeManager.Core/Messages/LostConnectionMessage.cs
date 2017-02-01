using System;
using CoffeeManager.Core.Messages;
namespace CoffeeManager.Core
{
	public class LostConnectionMessage : BaseMessage
	{
		public LostConnectionMessage (object sender) : base (sender)
		{
		}
	}
}
