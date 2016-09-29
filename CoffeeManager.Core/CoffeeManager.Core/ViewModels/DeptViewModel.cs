using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Core.Managers;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class DeptViewModel : ViewModelBase
    {
        private readonly DeptManager _deptManager = new DeptManager();
        private string _deptToAdd;
        private string _deptToRemove;
        private string _currentDeptSum;

        private ICommand _addDeptCommand;
        private ICommand _removeDeptCommand;

        public string DeptToAdd
        {
            get { return _deptToAdd; }
            set
            {
                _deptToAdd = value;
                RaisePropertyChanged(nameof(DeptToAdd));
            }
        }

        public string DeptToRemove
        {
            get { return _deptToRemove; }
            set
            {
                _deptToRemove = value;
                RaisePropertyChanged(nameof(DeptToRemove));
            }
        }


        public string CurrentDeptSum
        {
            get { return _currentDeptSum; }
            set
            {
                _currentDeptSum = value;
                RaisePropertyChanged(nameof(CurrentDeptSum));
            }
        }

        public bool IsEnabled => true;

        public ICommand AddDeptCommand => _addDeptCommand;
        public ICommand RemoveDeptCommand => _removeDeptCommand;

        public DeptViewModel()
        {
             _addDeptCommand = new MvxAsyncCommand(DoAddDept);
            _removeDeptCommand = new MvxAsyncCommand(DoRemoveDept);
        }

        public async void Init()
        {
             await _deptManager.GetCurrentDeptSum().ContinueWith((sum) => CurrentDeptSum = sum.Result.ToString());
        }

        private async Task DoRemoveDept()
        {
            if (!string.IsNullOrEmpty(DeptToRemove))
            {
                UserDialogs.Confirm(new ConfirmConfig()
                {
                    Message = $"Списать сумму {DeptToRemove}?",
                    OnAction = async (ok) =>
                    {
                        await _deptManager.RemoveDept(int.Parse(DeptToRemove));
                        Close(this);
                    }
                });
            }
        }

        private async Task DoAddDept()
        {
            if (!string.IsNullOrEmpty(DeptToAdd))
            {
                UserDialogs.Confirm(new ConfirmConfig()
                {
                    Message = $"Добавить сумму {DeptToRemove}?",
                    OnAction = async (ok) =>
                    {
                        await _deptManager.AddDept(int.Parse(DeptToAdd));
                        Close(this);
                    }
                });
            }
        }
    }
}
