using CoffeManager.Common;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public void ShowErrorMessage(string v)
        {
            UserDialogs.Alert(v);
        }


        public MainViewModel()
        {

        }

    }
}
