using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CoffeeManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeManager.Common;
using CoffeeManager.Common;
using System;
using MvvmCross.Plugins.Messenger;
using CoffeeManagerAdmin.Core.Util;

namespace CoffeeManagerAdmin.Core
{
    public class UserDetailsViewModel : ViewModelBase
    {
        private readonly MvxSubscriptionToken token;

        private readonly IShiftManager shiftManager;
        private readonly IUserManager userManager;
        private readonly IPaymentManager paymentManager;

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
        public decimal SalaryRate { get; set; }
        public decimal MinimumPayment { get; set; }

        public ICommand PaySalaryCommand {get;set;}
        public ICommand UpdateCommand {get;set;}
        public ICommand PenaltyCommand { get; set; }
        public ICommand ShowEarningsCommand { get; set; }

        public List<UserPenaltyItemViewModel> Penalties { get; set; } = new List<UserPenaltyItemViewModel>();


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

        public UserDetailsViewModel(IUserManager userManager, IPaymentManager paymentManager, IShiftManager shiftManager)
        {
            this.shiftManager = shiftManager;
            this.paymentManager = paymentManager;
            this.userManager = userManager;
            PaySalaryCommand = new MvxCommand(DoPaySalary);
            UpdateCommand = new MvxCommand(DoUpdateUser);
            PenaltyCommand = new MvxCommand(DoPenalty);
            ShowEarningsCommand = new MvxCommand(DoShowEarnings);

            token = Subscribe<UserAmountChangedMessage>(async (obj) => await Init(useridParameter));
        }

        private void DoShowEarnings()
        {
            var id = ParameterTransmitter.PutParameter(user?.Earnings);
            ShowViewModel<UserEarningsViewModel>(new {id});
        }

        private async void DoPenalty()
        {
            var amount = await PromtDecimalAsync("Введите сумму штрафа");
            if(!amount.HasValue)
            {
                return;
            }
            var reason = await PromtStringAsync("Причина штрафа?");
            if (string.IsNullOrWhiteSpace(reason))
            {
                return;
            }
            await ExecuteSafe(async () =>
            {
                await userManager.PenaltyUser(UserId, amount.Value, reason);
                await Init(useridParameter);
            });

        }

        private void DoUpdateUser()
        {
            Confirm("Сохранить изменения?", UpdateUser);
        }

        private async Task UpdateUser()
        {
            user.DayShiftPersent = DayShiftPersent;
            user.NightShiftPercent = NightShiftPercent;
            user.ExpenceId = SelectedExpenseType?.Id;
            user.SalaryRate = SalaryRate;
            user.MinimumPayment = MinimumPayment;
            await userManager.UpdateUser(user);
            Close(this);        
        }

        private async void DoCreateUser()
        {
            user.Name = UserName;
            user.DayShiftPersent = DayShiftPersent;
            user.NightShiftPercent = NightShiftPercent;
            user.ExpenceId = SelectedExpenseType?.Id;
            user.CoffeeRoomNo = Config.CoffeeRoomNo;
            user.SalaryRate = SalaryRate;
            user.IsActive = true;
            user.MinimumPayment = MinimumPayment;
            await userManager.AddUser(user);
            Publish(new RefreshUserListMessage(this));
            Close(this);        
        }

        public async Task Init(int id)
        {
            useridParameter = id;
            if(useridParameter == 0)
            {
                return;
            }
            await ExecuteSafe(async () => 
            {
                user = await userManager.GetUser(useridParameter);
                UserName = user.Name;
                DayShiftPersent = user.DayShiftPersent;
                NightShiftPercent = user.NightShiftPercent;
                SalaryRate = user.SalaryRate;
                MinimumPayment = user.MinimumPayment;

                Penalties = user.Penalties?.Select(s => new UserPenaltyItemViewModel(s)).ToList();

                await InitTypes();
    
                RaiseAllPropertiesChanged();
            });
        }

        public async Task Init()
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
            await ExecuteSafe(async () => 
            {
                var types = await paymentManager.GetExpenseItems();
                ExpenseItems = types.Select(s => new Entity { Id = s.Id, Name = s.Name }).ToList();
                if (user.ExpenceId > 0)
                {
                    var item = ExpenseItems.FirstOrDefault(i => i.Id == user.ExpenceId);
                    if(item == null)
                    {
                        Alert("Расход связанный с зарплатой сотрудника удалена, выберите новый расход");
                        return;
                    }
                    SelectedExpenseType = item;
                }
            });
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
            await ExecuteSafe(async () => 
            {
                var shift = await shiftManager.GetCurrentShiftAdmin();
                if(shift == null)
                {
                    UserDialogs.Alert("Запустите новую смену!");
                    return;
                }
                await userManager.PaySalary(UserId, shift.Id);
                user.EntireEarnedAmount += CurrentEarnedAmount;
                user.CurrentEarnedAmount = 0;
                Publish(new UpdateCashAmountMessage(this));
                Close(this);
            });
        }

        protected override void DoUnsubscribe()
        {
            Unsubscribe<UserAmountChangedMessage>(token);
        }
   }
}
