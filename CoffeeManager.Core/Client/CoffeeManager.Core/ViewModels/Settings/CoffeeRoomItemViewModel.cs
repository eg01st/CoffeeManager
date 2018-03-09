using System;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;

namespace CoffeeManager.Core
{
    public class CoffeeRoomItemViewModel : ListItemViewModelBase
    {
        private bool isSelected;

        public int Id { get; set; }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisePropertyChanged(nameof(IsSelected));

            }
        }

        public CoffeeRoomItemViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
