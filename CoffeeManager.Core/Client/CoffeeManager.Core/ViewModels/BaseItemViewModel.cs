using CoffeeManager.Models;
using CoffeManager.Common;

namespace CoffeeManager.Core.ViewModels
{
    public class BaseItemViewModel : ViewModelBase
    {
        private int _id;
        private string _name;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public BaseItemViewModel(Entity item)
        {
            Id = item.Id;
            Name = item.Name;
        }

        public BaseItemViewModel()
        {
            
        }
    }
}
