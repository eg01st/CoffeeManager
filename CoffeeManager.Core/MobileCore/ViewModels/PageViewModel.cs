using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace MobileCore.ViewModels
{
    public class PageViewModel : SimpleViewModel
    {
        public event EventHandler DataLoaded;

        private bool dataLoaded;
        private bool isLoading;
        private bool isInit = true;

        public ICommand CloseCommand { get; }

        public bool IsInit
        {
            get => isInit;

            set => SetProperty(ref isInit, value);
        }

        public bool DataAlreadyLoaded
        {
            get => dataLoaded;

            set
            {
                SetProperty(ref dataLoaded, value);
                RaisePropertyChanged(nameof(DataLoaded));
            }
        }
        
        public async Task<bool> HasInternetConnectionAsync() => await Connectivity.HasInternetConnectionAsync;

        protected PageViewModel()
        {
            CloseCommand = new MvxAsyncCommand(DoClose);
            Subscribe();
        }

        public override async Task Initialize() => await DoLoadDataAsync();

        public void Subscribe() => DoSubscribe();

        public void Unsubscribe() => DoUnsubscribe();

        private async Task DoLoadDataAsync()
        {
            DataAlreadyLoaded = false;

            var loadDataFunc = new Func<Task>(async () =>
            {
                await DoPreLoadDataImplAsync();
                await DoLoadDataImplAsync();
                FireDataLoaded();
                await DataLoadedAsync();

                RaiseAllPropertiesChanged();
            });

            IsInit = true;

            await ExecuteSafe(loadDataFunc);

            IsInit = false;
        }
        
        protected virtual async Task RefreshDataAsync()
        {
            await DoLoadDataAsync();
        }

        protected virtual Task DoPreLoadDataImplAsync() => Task.FromResult(0);
        
        protected virtual Task DoLoadDataImplAsync() => Task.FromResult(0);

        protected virtual Task DataLoadedAsync() => Task.FromResult(0);

        protected virtual void DoSubscribe()
        {
            
        }

        protected virtual void DoUnsubscribe()
        {
           
        }

        protected void FireDataLoaded()
        {
            DataAlreadyLoaded = true;
            DataLoaded?.Invoke(this, new EventArgs());
        }

        protected virtual async Task DoClose()
        {
            OnClose();
            await PerformClose();
        }

        private void OnClose()
        {
            DoUnsubscribe();
            DoOnClose();
        }

        protected virtual void DoOnClose()
        {
        }

        protected virtual async Task PerformClose()
        {
            await NavigationService.Close(this);
        }
    }
}