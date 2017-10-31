using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeManager.Common.Managers;
using MvvmCross.Core.Navigation;

namespace CoffeManager.Common
{
    public abstract class ViewModelBase : MvxViewModel
    {
        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                if (isLoading)
                {
                    this.UserDialogs.ShowLoading("Loading", Acr.UserDialogs.MaskType.Black);
                }
                else
                {
                    UserDialogs.HideLoading();
                }
                RaisePropertyChanged(nameof(IsLoading));
            }
        }

        #region IOC

        protected IUserDialogs UserDialogs
        {
            get
            {
                return Mvx.Resolve<IUserDialogs>();
            }
        }

        private IMvxMessenger MvxMessenger
        {
            get
            {
                return Mvx.Resolve<IMvxMessenger>();
            }
        }

        private IAccountManager AccountManager
        {
            get
            {
                return Mvx.Resolve<IAccountManager>();
            }
        }

        private ILocalStorage LocalStorage
        {
            get
            {
                return Mvx.Resolve<ILocalStorage>();
            }
        }


        protected IEmailService EmailService
        {
            get
            {
                if (Mvx.CanResolve<IEmailService>())
                {
                    return Mvx.Resolve<IEmailService>();
                }
                return null;
            }
        }

        protected IMvxNavigationService NavigationService
        {
            get
            {
                return Mvx.Resolve<IMvxNavigationService>();
            }
        }

        #endregion
        public ICommand CloseCommand { get; }



        public ViewModelBase()
        {
            CloseCommand = new MvxCommand(DoClose);
        }

        protected virtual void DoClose()
        {
            OnClose();
            DoUnsubscribe();
            Close(this);
        }

        public override void ViewDestroy()
        {
            base.ViewDestroy();
            DoUnsubscribe();
        }


        protected virtual void OnClose()
        {
        }

        protected virtual void DoUnsubscribe()
        {
        }

        protected MvxSubscriptionToken Subscribe<TMessage>(Action<TMessage> action)
            where TMessage : MvxMessage
        {
            return MvxMessenger.Subscribe<TMessage>(action, MvxReference.Weak);
        }

        protected void Unsubscribe<TMessage>(MvxSubscriptionToken id)
            where TMessage : MvxMessage
        {
            MvxMessenger.Unsubscribe<TMessage>(id);
        }

        protected void Publish<T>(T message) where T : MvxMessage
        {
            MvxMessenger.Publish(message);
        }

        protected async Task ExecuteSafe(Task functionToRun, string globalExceptionMessage = null)
        {
            Func<Task<bool>> runDelegate = async () => { await functionToRun; return true; };

            await ExecuteSafe(functionToRun: runDelegate,
                                    globalExceptionMessage: globalExceptionMessage);
        }

        protected async Task ExecuteSafe(Func<Task> functionToRun, string globalExceptionMessage = null)
        {
            Func<Task<bool>> runDelegate = async () => { await functionToRun(); return true; };

            await ExecuteSafe(functionToRun: runDelegate,
                                    globalExceptionMessage: globalExceptionMessage);
            
        }

        protected async Task<T> ExecuteSafe<T>(Func<Task<T>> functionToRun, string globalExceptionMessage = null, T valueToReturnForError = default(T))
        {
            try
            {
                IsLoading = true;

                var result = await functionToRun();
                return result;
            }
            catch (HttpRequestException hrex)
            {
                Debug.WriteLine(hrex.ToDiagnosticString());
                UserDialogs.Alert("Нет подключения к интернету, доступно только добавление продаж");
            }
            catch (TaskCanceledException tcex)
            {
                Debug.WriteLine(tcex.ToDiagnosticString());
                UserDialogs.Alert("Нет подключения к интернету, доступно только добавление продаж");
            }
            catch (UnauthorizedAccessException uaex)
            {
                Debug.WriteLine(uaex.ToDiagnosticString());
                UserDialogs.Alert("Не верный логин или пароль");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToDiagnosticString());
#if DEBUG
                UserDialogs.Alert(e.ToString());
#else
                Alert("Произошла ошибка сервера. Мы работаем над решением проблемы");
                await EmailService?.SendErrorEmail(e.ToDiagnosticString());
#endif

            }
            finally
            {
                IsLoading = false;                
            }
            return valueToReturnForError;
        }

        public void ShowSuccessMessage(string message)
        {
            UserDialogs.ShowSuccess(message, 300);
        }

        public void Alert(string message, string title = null)
        {
            UserDialogs.Alert(message, title);
        }

        public void Alert(string message, Action action, string title = null)
        {
            UserDialogs.Alert(new AlertConfig()
            {
                Message = message,
                Title = title,
                OnAction = async () => await ExecuteSafe(async () => action())
            });
        }

        public void Confirm(string message, Action action)
        {
             UserDialogs.Confirm(new ConfirmConfig()
                {
                    Message = message,
                    OnAction = async
                        (confirm) =>
                        {
                            if (confirm)
                            {
                                await ExecuteSafe(async () => action());
                            }
                        }
                });
        }

        public void Confirm(string message, Func<Task> action)
        {
            UserDialogs.Confirm(new ConfirmConfig()
            {
                Message = message,
                OnAction = async
                    (confirm) =>
                {
                    if (confirm)
                    {
                        await ExecuteSafe(async () => await action());
                    }
                }
            });
        }

        public void Confirm<T>(string message, Func<T, Task> action, T item)
        {
            UserDialogs.Confirm(new ConfirmConfig()
            {
                Message = message,
                OnAction = async
                    (confirm) =>
                {
                    if (confirm)
                    {
                        await ExecuteSafe(async () => await action(item));
                    }
                }
            });
        }

        public async Task<int?> PromtAsync(string message)
        {
           var result = await UserDialogs.PromptAsync(new PromptConfig()
                {
                    Message = message, 
                    InputType = InputType.Number
                    
                });
            if(string.IsNullOrWhiteSpace(result.Value))
            {
                return null;
            }
            return int.Parse(result.Value);
        }

        public async Task<decimal?> PromtDecimalAsync(string message)
        {
            var result = await UserDialogs.PromptAsync(new PromptConfig()
            {
                Message = message,
                InputType = InputType.DecimalNumber,

            });
            if (string.IsNullOrWhiteSpace(result.Value))
            {
                return null;
            }
            return decimal.Parse(result.Value);
        }

        public async Task<string> PromtStringAsync(string message, InputType inputType = InputType.Default)
        {
            var result = await UserDialogs.PromptAsync(new PromptConfig()
            {
                Message = message,
                InputType = inputType

            });
            return result.Value;
        }


        //protected bool ShowViewModelAsRoot<TViewModel>(object parameter = null, MvxRequestedBy requestedBy = null) where TViewModel : IMvxViewModel
        //{
        //    var bundle = MvxNavigationExtensions.ProduceRootViewModelRequest();
        //    return ShowViewModel<TViewModel>(parameter, bundle, requestedBy);
        //}


        protected async Task<bool> PromtLogin()
        {
            var email = await PromtStringAsync("Введите логин");
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            var password = await PromtStringAsync("Введите пароль", InputType.Password);
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }
            await AccountManager.Authorize(email, password);
            LocalStorage.SetUserInfo(new UserInfo() { Login = email, Password = password });
            return true;
        }
    }
}
