using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models.Data.DTO.StaffMotivation;
using CoffeManager.Common.Managers;
using MobileCore.Collections;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Motivation
{
    public class MotivationViewModel : FeedViewModel<MotivationItemViewModel>
    {
        private readonly IMotivationManager manager;
        private MotivationDTO motivation;

        public MotivationViewModel(IMotivationManager manager)
        {
            this.manager = manager;
            StartMotivationCommand = new MvxAsyncCommand(DoStartMotivation, () => motivation == null || motivation.EndDate.HasValue);
            FinishMotivationCommand = new MvxAsyncCommand(DoFinishMotivation, () => motivation != null && !motivation.EndDate.HasValue);
        }

        public ICommand StartMotivationCommand { get; set; }
        public ICommand FinishMotivationCommand { get; set; }
        public ICommand ShowDetailedMotivationCommand { get; set; }

        public string MotivationStartDate => motivation?.StartDate.ToString("d");

        protected override async Task<PageContainer<MotivationItemViewModel>> GetPageAsync(int skip)
        {
            return await ExecuteSafe(async () =>
            {
                motivation = await manager.GetCurrentMotivation();
                RaiseAllPropertiesChanged();
                if (motivation != null)
                {
                    var items = await manager.GetUsersMotivation(motivation.Id);
                    return items.Select(s => new MotivationItemViewModel(s)).ToPageContainer();
                }

                return Enumerable.Empty<MotivationItemViewModel>().ToPageContainer();
            });
        }
        
        private async Task DoStartMotivation()
        {
            Confirm("Начать новую мотивацию?", async () =>
            {
                motivation = await ExecuteSafe(manager.StartNewMotivation);
                RaiseAllPropertiesChanged();
            });
        }
        
        private async Task DoFinishMotivation()
        {
            Confirm("Закончить мотивацию?", async () =>
            {
                await ExecuteSafe(manager.FinishMotivation(motivation.Id));
                motivation = null;
                RaiseAllPropertiesChanged();
            });
        }
    }
}