﻿using System.Collections.Generic;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public class ErrorsViewModel : ViewModelBase
    {
        public List<Request> Requests => RequestExecutor.GetRequests();
        public List<string> Errors => RequestExecutor.GetErrors();

    }
}