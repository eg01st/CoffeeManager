using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;

namespace CoffeeManager.Core.ViewModels
{
    public class ErrorsViewModel : ViewModelBase
    {

        public ICommand ClearErrorsCommand { get; set; }

        public ErrorsViewModel()
        {
            ClearErrorsCommand = new MvxCommand(DoClear);
        }

        private void DoClear()
        {
          
        }
    }
}
