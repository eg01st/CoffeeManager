using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using CoffeeManager.Core;
using CoffeeManager.Core.ViewModels;
using CoffeeManager.Droid.Entities;
using CoffeeManager.Droid.Views.Abstract;
using CoffeeManager.Droid.Views.Fragments;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace CoffeeManager.Droid
{
    public class TabFactory
    {
        private readonly IMvxViewModelLoader mvxViewModelLoader = Mvx.Resolve<IMvxViewModelLoader>();
        public TabItem[] Produce()
        {
            var coffee = ProduceFragment<CoffeeFragment, CoffeeViewModel>();
            var tea = ProduceFragment<TeaFragment, TeaViewModel>();
            var coldDrinks = ProduceFragment<ColdDrinksFragment, ColdDrinksViewModel>();
            var water = ProduceFragment<WaterFragment, WaterViewModel>();
            var iceCream = ProduceFragment<IceCreamFragment, IceCreamViewModel>();
            var meals = ProduceFragment<MealsFragment, MealsViewModel>();

            return new TabItem[]
            {
                new TabItem("Кофе", coffee),
                new TabItem("Чай", tea),
                new TabItem("Хол. напитки", coldDrinks),
                new TabItem("Вода", water),
                new TabItem("Мороженое", iceCream),
                new TabItem("Еда", meals),
            };

        }

        private Fragment ProduceFragment<TFragment, TViewModel>()
    where TFragment : LazyViewModelLoadingFragment<TViewModel>, new()
    where TViewModel : ViewModelBase
        {
            var fragment = new TFragment
            {
                ViewModelLoadingFactory = () =>
                {
                    var request = new MvxViewModelRequest<TViewModel>(null, null, MvxRequestedBy.UserAction);
                    var viewModel = (TViewModel)mvxViewModelLoader.LoadViewModel(request, null);
                    return viewModel;
                }
            };

            return fragment;
        }
    }
}