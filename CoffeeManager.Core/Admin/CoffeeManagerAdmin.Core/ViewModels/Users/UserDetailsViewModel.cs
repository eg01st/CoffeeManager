using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Common;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.User;
using CoffeeManagerAdmin.Core.ViewModels.Abstract;
using CoffeManager.Common.Managers;
using MobileCore.Collections;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.Users
{
    public class UserDetailsViewModel : AdminCoffeeRoomFeedViewModel<UserPenaltyItemViewModel>, IMvxViewModel<int>
    {
        private readonly MvxSubscriptionToken token;

        private readonly IShiftManager shiftManager;
        private readonly IUserManager userManager;
        private readonly IPaymentManager paymentManager;

        private List<Entity> expenseItems = new List<Entity>();
        private Entity selectedExpenseType;
        private int? expenseTypeId;
        private string expenseTypeName;

        private UserDTO user;
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
        public ICommand SelectExpenseCommand { get; } 
        
        public List<Entity> ExpenseItems
        {
            get { return expenseItems; }
            set
            {
                expenseItems = value;
                RaisePropertyChanged(nameof(ExpenseItems));
            }
        }

        public Entity SelectedExpenseType
        {
            get { return selectedExpenseType; }
            set
            {
                if (selectedExpenseType != value)
                {
                    selectedExpenseType = value;
                    RaisePropertyChanged(nameof(SelectedExpenseType));
                    ExpenseTypeId = selectedExpenseType.Id;
                    ExpenseTypeName = selectedExpenseType.Name;
                }
            }
        }

        public int? ExpenseTypeId
        {
            get { return expenseTypeId; }
            set
            {
                expenseTypeId = value;
                RaisePropertyChanged(nameof(ExpenseTypeId));
            }
        }

        public string ExpenseTypeName
        {
            get { return expenseTypeName; }
            set
            {
                expenseTypeName = value;
                RaisePropertyChanged(nameof(ExpenseTypeName));
            }
        }

        public override Entity CurrentCoffeeRoom
        {
            get => base.CurrentCoffeeRoom;
            set
            {
                base.CurrentCoffeeRoom = value;
                if(useridParameter == 0)
                {
                    return;
                }
                var strategy = user.PaymentStrategies?.FirstOrDefault(s => s.CoffeeRoomId == CurrentCoffeeRoom.Id);

                DayShiftPersent = strategy?.DayShiftPersent ?? 0;
                NightShiftPercent = strategy?.NightShiftPercent ?? 0;
                SalaryRate = strategy?.SimplePayment ?? 0;
                MinimumPayment = strategy?.MinimumPayment ?? 0;
                RaiseAllPropertiesChanged();
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
            ShowEarningsCommand = new MvxAsyncCommand(DoShowEarnings);
            SelectExpenseCommand = new MvxCommand(DoSelectExpense);

            token = MvxMessenger.Subscribe<UserAmountChangedMessage>(async (obj) => await Initialize());
        }
        
        
        
        private void DoSelectExpense()
        {
            if (ExpenseItems.Count <= 1)
            {
                return;
            }
            var optionList = new List<ActionSheetOption>();
            foreach (var cr in ExpenseItems)
            {
                optionList.Add(new ActionSheetOption(cr.Name, () => { SelectedExpenseType = ExpenseItems.First(c => c.Id == cr.Id); }));
            }

            UserDialogs.ActionSheet(new ActionSheetConfig
            {
                Options = optionList,
                Title = "Выбор расхода",
            });
        }

        private async Task DoShowEarnings()
        {
            await NavigationService.Navigate<UserEarningsViewModel, UserEarningsHistory[]>(user?.Earnings);
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
                await Initialize();
            });

        }

        private void DoUpdateUser()
        {
            Confirm("Сохранить изменения?", UpdateUser);
        }

        private async Task UpdateUser()
        {

            user.ExpenceId = SelectedExpenseType?.Id;

            var strategy = user.PaymentStrategies?.FirstOrDefault(s => s.CoffeeRoomId == CurrentCoffeeRoom.Id);
            if (strategy == null)
            {
                strategy = new UserPaymentStrategy();
                if (user.PaymentStrategies != null)
                {
                    user.PaymentStrategies = user.PaymentStrategies.Concat(new [] { strategy}).ToArray();
                }
                else
                {
                    user.PaymentStrategies = new[] {strategy};
                }
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
            MvxMessenger.Publish(new RefreshUserListMessage(this));
            Close(this);        
        }

        protected override async Task<PageContainer<UserPenaltyItemViewModel>> GetPageAsync(int skip)
        {   
            if(useridParameter == 0)
            {
                user = new UserDTO();
                UpdateCommand = new MvxCommand(DoCreateUser);
            }
            else
            {
                await ExecuteSafe(async () =>
                {
                    user = await userManager.GetUser(useridParameter);
                    UserName = user.Name;
                    var strategy = user.PaymentStrategies?.FirstOrDefault(s => s.CoffeeRoomId == Config.CoffeeRoomNo);
                    if (strategy != null)
                    {
                        DayShiftPersent = strategy.DayShiftPersent;
                        NightShiftPercent = strategy.NightShiftPercent;
                        SalaryRate = strategy.SimplePayment;
                        MinimumPayment = strategy.MinimumPayment;
                    }
                });
            }
            await InitTypes();
            return user.Penalties?.Select(s => new UserPenaltyItemViewModel(s)).ToPageContainer();
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
                MvxMessenger.Publish(new UpdateCashAmountMessage(this));
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
            Confirm($"Выдать зарплату баристе {UserName}\nв размере {CurrentEarnedAmount} грн\nс заведения {CurrentCoffeeRoomName}?", PaySalary);
       
        }

        protected override void DoUnsubscribe()
        {
            base.DoUnsubscribe();
            MvxMessenger.Unsubscribe<UserAmountChangedMessage>(token);
        }

        public void Prepare(int parameter)
        {
            useridParameter = parameter;
        }
    }
}
