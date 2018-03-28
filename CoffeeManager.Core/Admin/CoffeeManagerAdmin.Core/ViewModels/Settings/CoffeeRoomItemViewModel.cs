using System.Threading.Tasks;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Platform;

namespace CoffeeManagerAdmin.Core.ViewModels.Settings
{
    public class CoffeeRoomItemViewModel : ListItemViewModelBase
    {
        private int id;

        public CoffeeRoomItemViewModel(int id, string name)
        {
            this.id = id;
            Name = name;
        }

        protected override void DoGoToDetails()
        {
            Confirm($"Удалить заведение {Name}?", DeleteCoffeeRoom);
        }

        private async Task DeleteCoffeeRoom()
        {
            var adminManager = Mvx.Resolve<IAdminManager>();
            await adminManager.DeleteCoffeeRoom(id);
            Publish(new RefreshCoffeeRoomsMessage(this));
        }

    }
}
