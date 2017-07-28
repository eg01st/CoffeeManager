using System.Collections.Generic;
using CoffeeManager.Models;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using System;

namespace CoffeeManager.Core.ViewModels
{
    public class ErrorsViewModel : ViewModelBase
    {
        public List<Request> Requests => RequestExecutor.GetRequests();
        public List<string> Errors => RequestExecutor.GetErrors();


        public ICommand ClearErrorsCommand { get; set; }

        public ErrorsViewModel()
        {
            ClearErrorsCommand = new MvxCommand(DoClear);
        }

        private void DoClear()
        {
            RequestExecutor.ClearErrors();
        }
    }
}
