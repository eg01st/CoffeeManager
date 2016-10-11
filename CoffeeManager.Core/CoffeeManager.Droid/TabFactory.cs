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
            var sweets = ProduceFragment<SweetsFragment, SweetsViewModel>();
            var water = ProduceFragment<WaterFragment, WaterViewModel>();
            var adds = ProduceFragment<AddsFragment, AddsViewModel>();
            var meals = ProduceFragment<MealsFragment, MealsViewModel>();

            return new TabItem[]
            {
                new TabItem("����", coffee),
                new TabItem("���", tea),
                new TabItem("��������", sweets),
                new TabItem("����", water),
                new TabItem("�������", adds),
                new TabItem("���", meals),
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