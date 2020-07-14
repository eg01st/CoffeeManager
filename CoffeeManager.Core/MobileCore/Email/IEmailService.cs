using System;
using System.Threading.Tasks;
namespace MobileCore.Email
{
    public interface IEmailService
    {
        Task SendErrorEmail(string title, string message);
    }
}
