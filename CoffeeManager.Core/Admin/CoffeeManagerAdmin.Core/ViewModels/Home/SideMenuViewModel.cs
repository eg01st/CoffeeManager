﻿using System.Threading.Tasks;
using CoffeeManagerAdmin.Core.ViewModels.Categories;
using CoffeeManagerAdmin.Core.ViewModels.CoffeeCounter;
using CoffeeManagerAdmin.Core.ViewModels.CreditCard;
using CoffeeManagerAdmin.Core.ViewModels.Settings;
using CoffeManager.Common.Common;
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Home
{
    public class SideMenuViewModel : FeedViewModel<FeedItemElementViewModel>
    {
    
        private MenuFeedtemViewModel selectedViewModel;

        public SideMenuViewModel()
        {
        }

        public async override Task Initialize()
        {
            ItemsCollection.AddRange(new FeedItemElementViewModel[]
            {
                MenuFeedtemViewModel.Create<SettingsViewModel>(Strings.CoffeeRooms),
                MenuFeedtemViewModel.Create<UsersViewModel>( Strings.Users),
                MenuFeedtemViewModel.Create<CreditCardViewModel>( Strings.CreditCard),
                MenuFeedtemViewModel.Create<CoffeeCountersViewModel>( Strings.Counters),
                MenuFeedtemViewModel.Create<CategoriesViewModel>( Strings.Categories),
                MenuFeedtemViewModel.Create<CoffeeCountersViewModel>( Strings.Quit),
            });
        }

        public MenuFeedtemViewModel SelectedViewModel 
        { 
            get
            {
                return selectedViewModel;
            }

            private set
            {
                selectedViewModel = value;
                RaisePropertyChanged();
            }
        }


        protected override async Task OnItemSelectedAsync(FeedItemElementViewModel item)
        {
            if (!(item is MenuFeedtemViewModel))
            {
                return;
            }

            await base.OnItemSelectedAsync(item);

            if (SelectedViewModel != null)
            {
                SelectedViewModel.IsSelected = false;
            }

            SelectedViewModel = (MenuFeedtemViewModel)item;
            SelectedViewModel.IsSelected = true;
        }
    }
}