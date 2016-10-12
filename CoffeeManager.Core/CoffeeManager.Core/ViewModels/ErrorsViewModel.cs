using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public class ErrorsViewModel : ViewModelBase
    {
        public List<Request> Requests => RequestExecutor.GetRequests();
        public List<string> Errors => RequestExecutor.GetErrors();

    }
}
