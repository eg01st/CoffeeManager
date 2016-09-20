using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Core.Managers;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class DeleteCupViewModel : ViewModelBase
    {
        private CupManager _cupManager = new CupManager();
        private List<CupViewModel> _items;
        private CupViewModel _selectedItem;

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

        public CupViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }


        public DeleteCupViewModel()
        {
            _submitCommand = new MvxCommand(DoSubmit);
        }

        private void DoSubmit()
        {
            _cupManager.UtilizeCup(SelectedItem.Id);
            ShowSuccessMessage("Стакан успешно списан!");
            Close(this);
        }

        public void Init()
        {
            Items = _cupManager.GetSupportedCups().Select(s => new CupViewModel(s)).ToList();
        }

    }
}
