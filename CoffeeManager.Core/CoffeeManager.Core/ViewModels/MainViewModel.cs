using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.Managers;

namespace CoffeeManager.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private PaymentManager _paymentManager = new PaymentManager();

        private string _currentShiftMoney;
        private string _entireMoney;

        public string CurrentShiftMoney
        {
            get { return _currentShiftMoney; }
            set
            {
                _currentShiftMoney = value;
                RaisePropertyChanged(nameof(CurrentShiftMoney));
            }
        }

        public string EntireMoney
        {
            get { return _entireMoney; }
            set
            {
                _entireMoney = value;
                RaisePropertyChanged(nameof(EntireMoney));
            }
        }

        public void Init()
        {
            _currentShiftMoney = _paymentManager.GetCurrentShiftMoney().ToString();
            _entireMoney = _paymentManager.GetEntireMoney().ToString();
        }
    }
}
