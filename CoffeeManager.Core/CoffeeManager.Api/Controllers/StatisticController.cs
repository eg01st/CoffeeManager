using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CoffeeManager.Api.Controllers
{
    public class StatisticController : ApiController
    {

        public async Task<HttpResponseMessage> GetProfitExpense([FromUri] int coffeeroomno, HttpRequestMessage message)
        {
            
        }
    }
}
