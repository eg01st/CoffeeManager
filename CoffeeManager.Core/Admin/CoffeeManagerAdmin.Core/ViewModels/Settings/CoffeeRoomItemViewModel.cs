using System;
using CoffeManager.Common;
using MvvmCross.Platform;
using System.Threading.Tasks;

namespace CoffeeManagerAdmin.Core
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
            Confirm($"Удалить кофейню {Name}?", DeleteCoffeeRoom);
        }

        private async Task DeleteCoffeeRoom()
        {
            var adminManager = Mvx.Resolve<IAdminManager>();
            await adminManager.DeleteCoffeeRoom(id);
            Publish(new RefreshCoffeeRoomsMessage(this));
        }

    }
}
