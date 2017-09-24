using System;
using System.Threading.Tasks;
namespace CoffeManager.Common
{
    public interface IEmailService
    {
        Task SendErrorEmail(string message);
    }
}
