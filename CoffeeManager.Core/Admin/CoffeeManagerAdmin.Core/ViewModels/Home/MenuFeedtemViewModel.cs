using System;
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Home
{
    public class MenuFeedtemViewModel : FeedItemElementViewModel
    {
        private bool selected;

        protected MenuFeedtemViewModel(string title, Type navigationViewModel)
        {
            Title = title;
            NavigationViewModel = navigationViewModel;
        }

        public static MenuFeedtemViewModel Create<TViewModel>( string title) where TViewModel : PageViewModel
        {
            var menuItem = new MenuFeedtemViewModel(title, typeof(TViewModel));
            return menuItem;
        }

        public string Title { get; }

        public Type NavigationViewModel { get; }

        public bool IsSelected
        {
            get { return selected; }
            set
            {
                selected = value;
                RaisePropertyChanged();
            }
        }

        protected override void Select()
        {
            NavigationService.Navigate(NavigationViewModel);
        }
    }
}