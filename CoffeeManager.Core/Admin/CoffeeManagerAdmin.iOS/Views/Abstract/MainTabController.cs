using System;
using MvvmCross.iOS.Views;
using MvvmCross.Core.ViewModels;
using UIKit;
using CoffeeManagerAdmin.Core;
using MvvmCross.Platform;
using Foundation;
using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels.Home;
using CoffeeManagerAdmin.Core.ViewModels.ManageExpenses;

namespace CoffeeManagerAdmin.iOS
{
    public abstract class MainTabController : MvxTabBarViewController, IUITabBarControllerDelegate
    {
        private bool isConstructed;
        private bool inConstruction;
        private IMvxViewModelLoader mvxViewModelLoader;
        private IMvxIosViewCreator mvxIosViewCreator;

        public MainTabController()
        {
            Initialize();
            Delegate = this;
        }

        public override bool ShowChildView(UIViewController viewController)
        {
            viewController.HidesBottomBarWhenPushed = true;
            return base.ShowChildView(viewController);
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

        public override void ViewDidLoad()
        {
            if (!isConstructed)
            {
                return;
            }

            base.ViewDidLoad();

            var vcs = new List<UIViewController>();

            foreach (var item in new []
            { 
                new { Type = typeof(MoneyViewModel), Icon = "ic_attach_money.png", Title = "Финансы" },
                new { Type = typeof(StorageViewModel), Icon = "ic_shopping_basket.png", Title = "Склад" },
                new { Type = typeof(ManageExpensesViewModel), Icon = "ic_arrow_upward.png", Title = "Расходы" },
                new { Type = typeof(ProductsViewModel), Icon = "ic_local_cafe.png", Title = "Товары" },
                new { Type = typeof(StatisticViewModel), Icon = "ic_trending_up.png", Title = "Статистика" },
            })
            {
                var vm = ProduceViewModel(item.Type);
                vm.Initialize();
                var view = ProduceView(vm);

                var icon = UIImage.FromBundle(item.Icon);
                icon = icon.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);

                var tabBarItem = new UITabBarItem(item.Title, icon, icon);
                view.TabBarItem = tabBarItem;
                vcs.Add(view);
            }

            ViewControllers = vcs.ToArray();

            DoViewDidLoad();
        }

        protected virtual void DoViewDidLoad()
        {
            
        }

        private MvxViewModel ProduceViewModel(Type viewModelType)
        {
            var request = new MvxViewModelRequest(viewModelType);
            var loadedViewModel = mvxViewModelLoader.LoadViewModel(request, null);
            var viewModel = loadedViewModel as MvxViewModel;

            return viewModel;
        }

        private UIViewController ProduceView(IMvxViewModel viewModel)
        {
            var controller = mvxIosViewCreator.CreateView(viewModel) as UIViewController;
            return new CoffeeManagerAdminNavigationContoller(controller);
        }
    }
}
