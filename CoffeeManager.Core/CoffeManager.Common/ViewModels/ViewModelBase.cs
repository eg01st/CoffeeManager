﻿using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using System.Windows.Input;

namespace CoffeManager.Common
{
    public abstract class ViewModelBase : MvxViewModel
    {
        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading;}
            set 
            {
                isLoading = value;
                if(isLoading)
                {
                    this.UserDialogs.ShowLoading("Loading", Acr.UserDialogs.MaskType.Black) ;
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


        private IEmailService EmailService
        {
            get
            {
                if(Mvx.CanResolve<IEmailService>())
                {
                    return Mvx.Resolve<IEmailService>();
                }
                return null;
            }
        }
        #endregion
        public ICommand CloseCommand { get; }



        public ViewModelBase()
        {
            CloseCommand = new MvxCommand(DoClose);
        }

        private void DoClose()
        {
            OnClose();
            DoUnsubscribe();
            Close(this);
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
            catch (Exception e)
            {
                Debug.WriteLine(e.ToDiagnosticString());
                UserDialogs.Alert(e.ToString());
                await EmailService?.SendErrorEmail(e.ToDiagnosticString());
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
                InputType = InputType.DecimalNumber

            });
            if (string.IsNullOrWhiteSpace(result.Value))
            {
                return null;
            }
            return decimal.Parse(result.Value);
        }

        public async Task<string> PromtStringAsync(string message)
        {
            var result = await UserDialogs.PromptAsync(new PromptConfig()
            {
                Message = message,
                InputType = InputType.Default

            });
            return result.Value;
        }
    }
}
