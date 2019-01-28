using System;
using System.Threading.Tasks;

namespace CoffeeManager.Core
{
    public interface IBackgroundChecker
    {
        Task StartBackgroundChecks();
        Task StopBackgroundChecks();
    }
}
