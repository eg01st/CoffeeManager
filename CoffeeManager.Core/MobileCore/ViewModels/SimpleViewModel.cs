using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MobileCore.Connection;
using MobileCore.Email;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;

namespace MobileCore.ViewModels
{
    public class SimpleViewModel : MvxViewModel
    {
        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
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

        protected IUserDialogs UserDialogs => Mvx.Resolve<IUserDialogs>();

        protected IMvxMessenger MvxMessenger => Mvx.Resolve<IMvxMessenger>();

        protected IEmailService EmailService => Mvx.CanResolve<IEmailService>() ? Mvx.Resolve<IEmailService>() : null;

        protected IMvxNavigationService NavigationService => Mvx.Resolve<IMvxNavigationService>();

        protected IConnectivity Connectivity => Mvx.Resolve<IConnectivity>();

        #endregion

        #region dialogs

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

        #endregion

        
        protected async Task ExecuteSafe(Task functionToRun, string globalExceptionMessage = null, bool checkInternetConnection = true)
        {
            Func<Task<bool>> runDelegate = async () => { await functionToRun; return true; };

            await ExecuteSafe(functionToRun: runDelegate,
                              globalExceptionMessage: globalExceptionMessage, checkInternetConnection: checkInternetConnection);
        }

        protected async Task ExecuteSafe(Func<Task> functionToRun, string globalExceptionMessage = null, bool checkInternetConnection = true)
        {
            Func<Task<bool>> runDelegate = async () => { await functionToRun(); return true; };

            await ExecuteSafe(functionToRun: runDelegate,
                              globalExceptionMessage: globalExceptionMessage, checkInternetConnection : checkInternetConnection);
            
        }

        protected async Task<T> ExecuteSafe<T>(Func<Task<T>> functionToRun, string globalExceptionMessage = null, T valueToReturnForError = default(T), bool checkInternetConnection = true)
        {
            try
            {
                if(checkInternetConnection)
                {
                    var hasConnection = await Connectivity.HasInternetConnectionAsync;
                    if(!hasConnection)
                    {
                        throw new NoInternetConnectionException();
                    }
                }

                IsLoading = true;

                var result = await functionToRun();
                return result;
            }
            catch (NoInternetConnectionException nice)
            {
                UserDialogs.Alert("Нет подключения к интернету, доступно только добавление продаж");
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
                Alert("Произошла ошибка сервера.");
                await EmailService.SendErrorEmail("CoffeeRoomId: 0" ,e.ToDiagnosticString());
#endif

            }
            finally
            {
                IsLoading = false;                
            }
            return valueToReturnForError;
        }
    }
}