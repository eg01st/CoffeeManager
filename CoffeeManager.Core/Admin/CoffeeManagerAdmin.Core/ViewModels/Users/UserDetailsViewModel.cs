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
        private readonly IAdminManager adminManager;


        private Entity _currentCoffeeRoom;
        private List<Entity> coffeeRooms;

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
        public decimal DayShiftPersent {get;set;}  
        public decimal NightShiftPercent {get;set;}
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

        public Entity CurrentCoffeeRoom
        {
            get { return _currentCoffeeRoom; }
            set
            {
                _currentCoffeeRoom = value;
                RaisePropertyChanged(nameof(CurrentCoffeeRoom));
                RaisePropertyChanged(nameof(CurrentCoffeeRoomName));
                if(useridParameter == 0)
                {
                    return;
                }
                var strategy = user.PaymentStrategies.FirstOrDefault(s => s.CoffeeRoomId == _currentCoffeeRoom.Id);

                DayShiftPersent = strategy?.DayShiftPersent ?? 0;
                NightShiftPercent = strategy?.NightShiftPercent ?? 0;
                SalaryRate = strategy?.SimplePayment ?? 0;
                MinimumPayment = strategy?.MinimumPayment ?? 0;
                RaiseAllPropertiesChanged();

            }
        }

        public List<Entity> CoffeeRooms
        {
            get { return coffeeRooms; }
            set
            {
                coffeeRooms = value;
                RaisePropertyChanged(nameof(CoffeeRooms));
            }
        }

        public string CurrentCoffeeRoomName
        {
            get { return CurrentCoffeeRoom.Name; }

        }

        public UserDetailsViewModel(IUserManager userManager, IPaymentManager paymentManager, IShiftManager shiftManager, IAdminManager adminManager)
        {
            this.adminManager = adminManager;
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

            user.ExpenceId = SelectedExpenseType?.Id;

            var strategy = user.PaymentStrategies.FirstOrDefault(s => s.CoffeeRoomId == CurrentCoffeeRoom.Id);
            if (strategy == null)
            {
                strategy = new UserPaymentStrategy();
                user.PaymentStrategies = user.PaymentStrategies.Concat(new [] { strategy}).ToArray();
            }

            strategy.SimplePayment = SalaryRate;
            strategy.MinimumPayment = MinimumPayment;
            strategy.DayShiftPersent = DayShiftPersent;
            strategy.NightShiftPercent = NightShiftPercent;
            strategy.CoffeeRoomId = CurrentCoffeeRoom.Id;

            await userManager.UpdateUser(user);
            Close(this);        
        }

        private async void DoCreateUser()
        {
            user.Name = UserName;

            user.ExpenceId = SelectedExpenseType?.Id;
            user.CoffeeRoomNo = Config.CoffeeRoomNo;

            user.IsActive = true;


            var strategy = new UserPaymentStrategy();
            strategy.MinimumPayment = MinimumPayment;
            strategy.SimplePayment = SalaryRate;
            strategy.DayShiftPersent = DayShiftPersent;
            strategy.NightShiftPercent = NightShiftPercent;
            strategy.CoffeeRoomId = CurrentCoffeeRoom.Id;
            user.PaymentStrategies = new[] { strategy };

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
                var strategy = user.PaymentStrategies.FirstOrDefault(s => s.CoffeeRoomId == Config.CoffeeRoomNo);
                if(strategy != null)
                {
                    DayShiftPersent = strategy.DayShiftPersent;
                    NightShiftPercent = strategy.NightShiftPercent;
                    SalaryRate = strategy.SimplePayment;
                    MinimumPayment = strategy.MinimumPayment;
                }

                Penalties = user.Penalties?.Select(s => new UserPenaltyItemViewModel(s)).ToList();

                await InitTypes();

                await InitCoffeeRooms();
                RaiseAllPropertiesChanged();
            });
        }

        public async Task Init()
        {
            if(useridParameter == 0)
            {
                await InitCoffeeRooms();

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
                        Alert("Расход связанный с зарплатой сотрудника удален, выберите новый расход");
                        return;
                    }
                    SelectedExpenseType = item;
                }
            });
        }

        private async Task InitCoffeeRooms()
        {
            await ExecuteSafe(async () =>
            {
                var items = await adminManager.GetCoffeeRooms();
                CoffeeRooms = items.ToList();
                CurrentCoffeeRoom = CoffeeRooms.First(c => c.Id == Config.CoffeeRoomNo);
            });
        }

        private async Task PaySalary()
        {
            await ExecuteSafe(async () =>
            {
                var shift = await shiftManager.GetCurrentShiftForCoffeeRoom(CurrentCoffeeRoom.Id);
                if (shift == null)
                {
                    UserDialogs.Alert("Запустите новую смену!");
                    return;
                }
                await userManager.PaySalary(UserId, CurrentCoffeeRoom.Id);
                user.EntireEarnedAmount += CurrentEarnedAmount;
                user.CurrentEarnedAmount = 0;
                Publish(new UpdateCashAmountMessage(this));
                Close(this);
            });
        }

        private void DoPaySalary()
        {
            if(!user.ExpenceId.HasValue)
            {
                UserDialogs.Alert("Не связан расход с пользователем!");
                return;
            }
            if(user.CurrentEarnedAmount <= 0)
            {
                UserDialogs.Alert("Пустой баланс!");
                return;
            }
            Confirm($"Выдать зарплату баристе {UserName}\nв размере {CurrentEarnedAmount} грн\nс кофейни {CurrentCoffeeRoomName}?", PaySalary);
       
        }

        protected override void DoUnsubscribe()
        {
            Unsubscribe<UserAmountChangedMessage>(token);
        }
   }
}
