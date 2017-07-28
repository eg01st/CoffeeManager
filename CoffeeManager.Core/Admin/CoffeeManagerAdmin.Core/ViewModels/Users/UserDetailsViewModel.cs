using System;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CoffeeManagerAdmin.Core.Managers;
using CoffeeManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeManagerAdmin.Core
{
    public class UserDetailsViewModel : ViewModelBase
    {
        UserManager um = new UserManager();
        PaymentManager pm = new PaymentManager();

        private List<Entity> _expenseItems = new List<Entity>();
        private Entity _selectedExpenseType;
        private int? _expenseTypeId;
        private string _expenseTypeName;

        private User user;
        private int useridParameter;
        public int UserId => user.Id;
        public string UserName {get;set;}
        public decimal CurrentEarnedAmount => user.CurrentEarnedAmount;
        public decimal EntireEarnedAmount => user.EntireEarnedAmount;
        public int DayShiftPersent {get;set;}  
        public int NightShiftPercent {get;set;}

        public ICommand PaySalaryCommand {get;set;}
        public ICommand UpdateCommand {get;set;}

        public List<Entity> ExpenseItems
        {
            get { return _expenseItems; }
            set
            {
                _expenseItems = value;
                RaisePropertyChanged(nameof(ExpenseItems));
            }
        }

        public Entity SelectedExpenseType
        {
            get { return _selectedExpenseType; }
            set
            {
                if (_selectedExpenseType != value)
                {
                    _selectedExpenseType = value;
                    RaisePropertyChanged(nameof(SelectedExpenseType));
                    ExpenseTypeId = _selectedExpenseType.Id;
                    ExpenseTypeName = _selectedExpenseType.Name;
                }
            }
        }

        public int? ExpenseTypeId
        {
            get { return _expenseTypeId; }
            set
            {
                _expenseTypeId = value;
                RaisePropertyChanged(nameof(ExpenseTypeId));
            }
        }

        public string ExpenseTypeName
        {
            get { return _expenseTypeName; }
            set
            {
                _expenseTypeName = value;
                RaisePropertyChanged(nameof(ExpenseTypeName));
            }
        }


        public UserDetailsViewModel()
        {
            PaySalaryCommand = new MvxCommand(DoPaySalary);
            UpdateCommand = new MvxCommand(DoUpdateUser);
        }

        private void DoUpdateUser()
        {
            UserDialogs.Confirm(new Acr.UserDialogs.ConfirmConfig() 
            {
                Message = "Сохранить изменения?",
                OnAction = async (bool obj) => 
                {
                    if(obj)
                    {
                         await UpdateUser();
                    }                
                }
            });
        }

        private async Task UpdateUser()
        {
            user.DayShiftPersent = DayShiftPersent;
            user.NightShiftPercent = NightShiftPercent;
            user.ExpenceId = SelectedExpenseType?.Id;
            await um.UpdateUser(user);
            Close(this);        
        }

        private async void DoCreateUser()
        {
            user.Name = UserName;
            user.DayShiftPersent = DayShiftPersent;
            user.NightShiftPercent = NightShiftPercent;
            user.ExpenceId = SelectedExpenseType?.Id;
            user.CoffeeRoomNo = Config.CoffeeRoomNo;
            user.IsActive = true;
            await um.AddUser(user);
            Close(this);        
        }

        public async void Init(int id)
        {
            useridParameter = id;
            if(useridParameter == 0)
            {
                return;
            }
            user = await um.GetUser(useridParameter);
            UserName = user.Name;
            DayShiftPersent = user.DayShiftPersent;
            NightShiftPercent = user.NightShiftPercent;
            
            await InitTypes();

            RaiseAllPropertiesChanged();
        }

        public async void Init()
        {
            if(useridParameter == 0)
            {
                user = new User();
                await InitTypes();
                UpdateCommand = new MvxCommand(DoCreateUser);
    
                RaiseAllPropertiesChanged();
            }
        }

        private async Task InitTypes()
        {
            var types = await pm.GetExpenseItems();
            ExpenseItems = types.Select(s => new Entity { Id = s.Id, Name = s.Name }).ToList();
            if (user.ExpenceId > 0)
            {
                var item = ExpenseItems.First(i => i.Id == user.ExpenceId);
                SelectedExpenseType = item;
            }
        }

        private void DoPaySalary()
        {
             UserDialogs.Confirm(new Acr.UserDialogs.ConfirmConfig() 
            {
                Message = "Выдать зарплату?",
                OnAction = async (bool obj) => 
                {
                    if(obj)
                    {
                         await PaySalary();
                    }                
                }
            });
        }

        private async Task PaySalary()
        {
            if(!user.ExpenceId.HasValue)
            {
                UserDialogs.Alert("Не связана трата с пользователем!");
                return;
            }
            if(user.CurrentEarnedAmount <= 0)
            {
                UserDialogs.Alert("Пустой баланс!");
                return;
            }
            
            var sm = new ShiftManager();
            var shift = await sm.GetCurrentShift();
            if(shift == null)
            {
                UserDialogs.Alert("Запустите новую смену!");
                return;
            }
            await um.PaySalary(UserId, shift.Id);
            user.EntireEarnedAmount += CurrentEarnedAmount;
            user.CurrentEarnedAmount = 0;
            Close(this);
        }
   }
}
