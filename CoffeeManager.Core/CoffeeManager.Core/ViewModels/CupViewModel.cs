using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public class CupViewModel : ViewModelBase
    {
        private CupType _cupType;
        public CupViewModel(CupType cupType)
        {
            _cupType = cupType;
        }

        public int Id => _cupType.Id;
        public string Name => _cupType.Name;
    }
}
