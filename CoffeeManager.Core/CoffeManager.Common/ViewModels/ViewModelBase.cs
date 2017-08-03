﻿using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;

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
        
        protected IUserDialogs UserDialogs
        {
            get
            {
                return Mvx.Resolve<IUserDialogs>();
            }
        }

        protected IMvxMessenger MvxMessenger
        {
            get
            {
                return Mvx.Resolve<IMvxMessenger>();
            }
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
            catch (Exception e)
            {
                UserDialogs.Alert(e.ToString());
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

        public void Alert(string message)
        {
            UserDialogs.Alert(message);
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
    }
}
