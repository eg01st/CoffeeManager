using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Core.Managers;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class DeleteCupViewModel : ViewModelBase
    {
        private CupManager _cupManager = new CupManager();
        private List<CupViewModel> _items;

        private ICommand _submitCommand;

        public ICommand SubmitCommand => _submitCommand;

        public List<CupViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        public DeleteCupViewModel()
        {
            _submitCommand = new MvxCommand<CupViewModel>(DoSubmit);
        }

        private void DoSubmit(CupViewModel item)
        {
            UserDialogs.Confirm(new ConfirmConfig()
            {
                Message = $"Списать {item.Name} стакан? Действие нельзя отменить.",
                OnAction = (ok) =>
                {
                    if (ok)
                    {
                        _cupManager.UtilizeCup(item.Id);
                        Close(this);
                    }
                }
            });
        }

        public void Init()
        {
            Items = _cupManager.GetSupportedCups().Select(s => new CupViewModel(s)).ToList();
        }

    }
}
