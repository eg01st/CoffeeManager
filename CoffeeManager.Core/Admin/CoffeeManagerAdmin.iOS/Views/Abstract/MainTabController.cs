using System;
using MvvmCross.iOS.Views;
using CoffeeManagerAdmin.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using UIKit;
using CoffeeManagerAdmin.Core;
using System.Collections.Generic;
using MvvmCross.Platform;

namespace CoffeeManagerAdmin.iOS
{
    public abstract class MainTabController : MvxTabBarViewController
    {
        private bool isConstructed;
        private bool inConstruction;
        private IMvxViewModelLoader mvxViewModelLoader;
        private IMvxIosViewCreator mvxIosViewCreator;

        public MainTabController()
        {
            Initialize();
        }

        private void Initialize()
        {
            mvxViewModelLoader = Mvx.Resolve<IMvxViewModelLoader>();
            mvxIosViewCreator = Mvx.Resolve<IMvxIosViewCreator>();

            inConstruction = true;
            isConstructed = true;
            ViewDidLoad();
            inConstruction = false;
        }


        protected int StartTabIndex
        {
            get; set;
        }
        public override nint SelectedIndex
        {
            get
            {
                return base.SelectedIndex;
            }
            set
            {
                if (inConstruction && value != StartTabIndex) // prevent setting first view by default 
                {
                    return;
                }

                base.SelectedIndex = value;
            }
        }


        protected UIViewController LastTopController { get; set; }

        public override void ViewDidLoad()
        {
            if (!isConstructed)
            {
                return;
            }

            base.ViewDidLoad();

            var moneyViewModel = ProduceViewModel(typeof(MoneyViewModel));
            var moneyView = ProduceView(moneyViewModel);

            var selectedMoneyImage = UIImage.FromBundle("ic_attach_money.png");
            selectedMoneyImage = selectedMoneyImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            var moneyImage = UIImage.FromBundle("ic_attach_money_white.png");
            moneyImage = moneyImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            var moneytabBarItem = new UITabBarItem("Финансы", moneyImage, selectedMoneyImage);

            moneyView.TabBarItem = moneytabBarItem;


            var storageViewModel = ProduceViewModel(typeof(StorageViewModel));
            var storageView = ProduceView(storageViewModel);

            var selectedStorageImage = UIImage.FromBundle("ic_shopping_basket.png");
            selectedStorageImage = selectedStorageImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            var storageImage = UIImage.FromBundle("ic_shopping_basket_white.png");
            storageImage = storageImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            var storagetabBarItem = new UITabBarItem("Склад", storageImage, selectedStorageImage);

            storageView.TabBarItem = storagetabBarItem;


            var usersViewModel = ProduceViewModel(typeof(UsersViewModel));
            var usersView = ProduceView(usersViewModel);

            var selectedUsersImage = UIImage.FromBundle("ic_account_circle.png");
            selectedUsersImage = selectedUsersImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            var usersImage = UIImage.FromBundle("ic_account_circle_white.png");
            usersImage = usersImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            var userstabBarItem = new UITabBarItem("Персонал", usersImage, selectedUsersImage);

            usersView.TabBarItem = userstabBarItem;


            var statisticViewModel = ProduceViewModel(typeof(StatisticViewModel));
            var statisticView = ProduceView(statisticViewModel);

            var selectedstatisticImage = UIImage.FromBundle("ic_trending_up.png");
            selectedstatisticImage = selectedstatisticImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            var statisticImage = UIImage.FromBundle("ic_trending_up_white.png");
            statisticImage = statisticImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            var statistictabBarItem = new UITabBarItem("Статистика", statisticImage, selectedstatisticImage);

            statisticView.TabBarItem = statistictabBarItem;


            var controllers = new[] {moneyView, storageView, usersView, statisticView };


            ViewControllers = controllers;

            LastTopController = controllers[0];

            OnViewControllerChanged(null, LastTopController);


            DoViewDidLoad();
        }

        private MvxViewModel ProduceViewModel(Type viewModelType)
        {
            var request = new MvxViewModelRequest(viewModelType, null, null, MvxRequestedBy.UserAction);
            var loadedViewModel = mvxViewModelLoader.LoadViewModel(request, null);
            var viewModel = loadedViewModel as MvxViewModel;

            return viewModel;
        }

        private UIViewController ProduceView(IMvxViewModel viewModel)
        {
            var controller = mvxIosViewCreator.CreateView(viewModel) as UIViewController;
            return new CoffeeManagerAdminNavigationContoller(controller);
        }

        protected virtual void DoViewDidLoad()
        {
        }

        protected virtual void SubscribeToEvents()
        {
            ViewControllerSelected += OnViewControllerSelected;
        }

        protected virtual void UnsubscribeFromEvents()
        {
            ViewControllerSelected -= OnViewControllerSelected;
        }

        private void OnViewControllerSelected(object sender, UITabBarSelectionEventArgs e)
        {
            var oldController = LastTopController;
            var newController = SelectedViewController;
            LastTopController = newController;

            OnViewControllerChanged(oldController, newController);
        }

        protected virtual void OnViewControllerChanged(UIViewController oldController, UIViewController newController)
        {
        }
    }
}
