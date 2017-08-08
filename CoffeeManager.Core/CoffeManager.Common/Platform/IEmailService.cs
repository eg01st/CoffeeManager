using System;
namespace CoffeManager.Common
{
    public interface IEmailService
    {
        void SendErrorEmail(string message);
    }
}
