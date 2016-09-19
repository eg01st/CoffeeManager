using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.Managers;
using CoffeeManager.Core.Messages;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManager.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private PaymentManager _paymentManager = new PaymentManager();
        private readonly MvxSubscriptionToken token;


        private float _currentShiftMoney;
        private float _entireMoney;

        public string CurrentShiftMoney
        {
            get { return _currentShiftMoney.ToString(); }
            set
            {
                _currentShiftMoney = float.Parse(value);
                RaisePropertyChanged(nameof(CurrentShiftMoney));
            }
        }

        public string EntireMoney
        {
            get { return _entireMoney.ToString(); }
            set
            {
                _entireMoney = float.Parse(value);
                RaisePropertyChanged(nameof(EntireMoney));
            }
        }

        public MainViewModel()
        {
            token = Subscribe<AmoutChangedMessage>(OnCallBackMessage);
        }

        public void Init()
        {
            _currentShiftMoney = _paymentManager.GetCurrentShiftMoney();
            _entireMoney = _paymentManager.GetEntireMoney();

           
        }

        private void OnCallBackMessage(AmoutChangedMessage message)
        {
            if (message.Data.Item2)
            {
                _currentShiftMoney += message.Data.Item1;
                _entireMoney += message.Data.Item1;
            }
            else
            {
                _currentShiftMoney -= message.Data.Item1;
                _entireMoney -= message.Data.Item1;

            }
            RaisePropertyChanged(nameof(CurrentShiftMoney));
            RaisePropertyChanged(nameof(EntireMoney));
        }
    }
}
