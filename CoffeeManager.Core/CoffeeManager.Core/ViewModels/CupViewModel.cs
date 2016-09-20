using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public class CupViewModel : ViewModelBase
    {
        private Cup _cup;
        public CupViewModel(Cup cup)
        {
            _cup = cup;
        }

        public int Id => _cup.Id;
        public string Name => _cup.Name;
        public int Capacity => _cup.Capacity;
    }
}
