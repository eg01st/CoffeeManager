using System;
using MvvmCross.iOS.Views;
using MvvmCross.Core.ViewModels;
using UIKit;
using CoffeeManagerAdmin.Core;
using MvvmCross.Platform;
using Foundation;

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

            var moneyViewModel = ProduceViewModel(typeof(MoneyViewModel));
            var moneyView = ProduceView(moneyViewModel);

            var selectedMoneyImage = UIImage.FromBundle("ic_attach_money.png");
            selectedMoneyImage = selectedMoneyImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            var moneyImage = UIImage.FromBundle("ic_attach_money_white.png");
            moneyImage = moneyImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            var moneytabBarItem = new UITabBarItem("Финансы", moneyImage, selectedMoneyImage);



            var storageViewModel = ProduceViewModel(typeof(StorageViewModel));
            var storageView = ProduceView(storageViewModel);

            var selectedStorageImage = UIImage.FromBundle("ic_shopping_basket.png");
            selectedStorageImage = selectedStorageImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            var storageImage = UIImage.FromBundle("ic_shopping_basket_white.png");
            storageImage = storageImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            var storagetabBarItem = new UITabBarItem("Склад", storageImage, selectedStorageImage);


            var expensesViewModel = ProduceViewModel(typeof(ManageExpensesViewModel));
            var expensesView = ProduceView(expensesViewModel);

            var selectedExpensesImage = UIImage.FromBundle("ic_arrow_upward.png");
            selectedExpensesImage = selectedExpensesImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            var expensesImage = UIImage.FromBundle("ic_arrow_upward_white.png");
            expensesImage = expensesImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            var expensesTabBarItem = new UITabBarItem("Расходы", expensesImage, selectedExpensesImage);



            var productsViewModel = ProduceViewModel(typeof(ProductsViewModel));
            var productsView = ProduceView(productsViewModel);

            var selectedProductsImage = UIImage.FromBundle("ic_local_cafe.png");
            selectedProductsImage = selectedProductsImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            var productsImage = UIImage.FromBundle("ic_local_cafe_white.png");
            productsImage = productsImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            var productsTabBarItem = new UITabBarItem("Товары", productsImage, selectedProductsImage);

        


            var statisticViewModel = ProduceViewModel(typeof(StatisticViewModel));
            var statisticView = ProduceView(statisticViewModel);

            var selectedstatisticImage = UIImage.FromBundle("ic_trending_up.png");
            selectedstatisticImage = selectedstatisticImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            var statisticImage = UIImage.FromBundle("ic_trending_up_white.png");
            statisticImage = statisticImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            var statistictabBarItem = new UITabBarItem("Статистика", statisticImage, selectedstatisticImage);


            moneyView.TabBarItem = moneytabBarItem;

            storageView.TabBarItem = storagetabBarItem;

            productsView.TabBarItem = productsTabBarItem;
            statisticView.TabBarItem = statistictabBarItem;
            expensesView.TabBarItem = expensesTabBarItem;

            var controllers = new[] {moneyView, storageView, productsView, expensesView, statisticView };


            ViewControllers = controllers;

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
