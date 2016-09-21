using CoffeeManager.Models;

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

        public BaseItemViewModel(ItemBase item)
        {
            Id = item.Id;
            Name = item.Name;
        }

        public BaseItemViewModel()
        {
            
        }
    }
}
