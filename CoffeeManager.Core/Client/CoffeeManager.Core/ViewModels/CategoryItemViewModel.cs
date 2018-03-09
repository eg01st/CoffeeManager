using System;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class CategoryItemViewModel : ListItemViewModelBase
    {
        private readonly Action<int> onClickAction;
        private bool isSelected;
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                RaisePropertyChanged();
            }
        }

        public CategoryItemViewModel(Action<int> onClickAction)
        {
            this.onClickAction = onClickAction;
        }

        protected override void DoGoToDetails()
        {
            onClickAction?.Invoke(Id);
            IsSelected = true;
        }
    }
}