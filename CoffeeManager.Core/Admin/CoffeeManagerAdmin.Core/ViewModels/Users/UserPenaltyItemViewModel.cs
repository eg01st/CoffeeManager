using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace CoffeeManagerAdmin.Core.ViewModels.Users
{
    public class UserPenaltyItemViewModel : FeedItemElementViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }

        public ICommand DismisPenaltyCommand { get; set; }

        public UserPenaltyItemViewModel(UserPenalty penalty)
        {
            Id = penalty.Id;
            Amount = penalty.Amount;
            Date = penalty.Date;
            Reason = penalty.Reason;

            DismisPenaltyCommand = new MvxCommand(DoDismissPenalty);
        }

        private void DoDismissPenalty()
        {
            Confirm("Отменить штраф?", async () => await DismissPenalty());
        }

        private async Task DismissPenalty()
        {
            var manager = Mvx.Resolve<IUserManager>();
            await manager.DismissPenalty(Id);
            MvxMessenger.Publish(new UserAmountChangedMessage(this));
        }

        protected override void Select()
        {
            Alert(Reason, "Причина");
        }
    }
}
