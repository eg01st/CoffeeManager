using System.Threading.Tasks;
using Quartz;

namespace CoffeeManager.Api.BackgroundJob
{
    public class AutoOrderJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            
        }
    }
}