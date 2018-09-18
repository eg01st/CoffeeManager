using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models.Data.DTO.StaffMotivation;
using CoffeManager.Common.Managers;
using MobileCore.Collections;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels.Motivation
{
    public class MotivationViewModel : FeedViewModel<MotivationItemViewModel>
    {
        private readonly IMotivationManager manager;
        private MotivationDTO motivation;

        public MotivationViewModel(IMotivationManager manager)
        {
            this.manager = manager;
        }

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
    }
}