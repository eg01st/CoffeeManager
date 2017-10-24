using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeManager.Common;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
namespace CoffeeManagerAdmin.Core
{
    public class TransferSuplyProductsViewModel : ViewModelBase
    {
        readonly IAdminManager manager;

        private List<Entity> coffeeRooms = new List<Entity>();
        private Entity fromCoffeeRoom;
        private Entity toCoffeeRoom;

        public TransferSuplyProductsViewModel(IAdminManager manager)
        {
            this.manager = manager;
            NextCommand = new MvxCommand(DoNextCommand, CanNext);
        }

        private bool CanNext()
        {
            return fromCoffeeRoom != null && toCoffeeRoom != null;
        }

        private void DoNextCommand()
        {
            ShowViewModel<SelectSuplyProductsViewModel>(new { fromCoffeeRoom = fromCoffeeRoom.Id, toCoffeeRoom = toCoffeeRoom.Id});
        }

        public ICommand NextCommand { get; }

        public string FromCoffeeRoomName { get; set; }
        public string ToCoffeeRoomName { get; set; }

        public List<Entity> CoffeeRooms
        {
            get { return coffeeRooms; }
            set
            {
                coffeeRooms = value;
                RaisePropertyChanged(nameof(CoffeeRooms));
            }
        }

        public Entity FromCoffeeRoom
        {
            get { return fromCoffeeRoom; }
            set
            {
                fromCoffeeRoom = value;
                FromCoffeeRoomName = fromCoffeeRoom.Name;
                RaisePropertyChanged(nameof(FromCoffeeRoom));
                RaisePropertyChanged(nameof(FromCoffeeRoomName));
                RaisePropertyChanged(nameof(NextCommand));
            }
        }

        public Entity ToCoffeeRoom
        {
            get { return toCoffeeRoom; }
            set
            {
                toCoffeeRoom = value;
                ToCoffeeRoomName = toCoffeeRoom.Name;
                RaisePropertyChanged(nameof(ToCoffeeRoom));
                RaisePropertyChanged(nameof(ToCoffeeRoomName));
                RaisePropertyChanged(nameof(NextCommand));
            }
        }

        public async Task Init()
        {
            await ExecuteSafe(async () =>
            {
                var items = await manager.GetCoffeeRooms();
                CoffeeRooms = items.ToList();
            });
        }
    }
}
