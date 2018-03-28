using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace CoffeeManagerAdmin.Core.ViewModels.Users
{
    public class UserPenaltyItemViewModel : ListItemViewModelBase
    {
        private IUserManager manager;

        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }

        public ICommand ShowReasonCommand { get; set; }
        public ICommand DismisPenaltyCommand { get; set; }

        public UserPenaltyItemViewModel(UserPenalty penalty)
        {
            Id = penalty.Id;
            Amount = penalty.Amount;
            Date = penalty.Date;
            Reason = penalty.Reason;

            DismisPenaltyCommand = new MvxCommand(DoDismissPenalty);

            manager = Mvx.Resolve<IUserManager>();
        }

        private void DoDismissPenalty()
        {
            Confirm("Отменить штраф?", () => DismissPenalty());
        }

        private async Task DismissPenalty()
        {
            await manager.DismissPenalty(Id);
            Publish(new UserAmountChangedMessage(this));
        }

        protected override void DoGoToDetails()
        {
            Alert(Reason, "Причина");
        }
    }
}
