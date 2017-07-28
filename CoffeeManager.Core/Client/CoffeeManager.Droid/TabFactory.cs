using Android.Support.V4.App;
using CoffeeManager.Core.ViewModels;
using CoffeeManager.Droid.Entities;
using CoffeeManager.Droid.Views.Fragments;

namespace CoffeeManager.Droid
{
    public class TabFactory
    {
        public TabItem[] Produce()
        {
            var coffee = ProduceFragment<CoffeeFragment, CoffeeViewModel>();
            var tea = ProduceFragment<TeaFragment, TeaViewModel>();
            var sweets = ProduceFragment<SweetsFragment, SweetsViewModel>();
            var water = ProduceFragment<WaterFragment, WaterViewModel>();
            var adds = ProduceFragment<AddsFragment, AddsViewModel>();
            var meals = ProduceFragment<MealsFragment, MealsViewModel>();
            var coldDrinks = ProduceFragment<ColdDrinksFragment, ColdDrinksViewModel>();
            var iceCream = ProduceFragment<IceCreamFragment, IceCreamViewModel>();

            return new TabItem[]
            {
                new TabItem("����", coffee),
                new TabItem("���", tea),
                new TabItem("��������", sweets),
                new TabItem("����", water),
                new TabItem("�������", adds),
                new TabItem("���", meals),
                new TabItem("��� �������", coldDrinks),
                new TabItem("���������", iceCream),
            };

        }

        private Fragment ProduceFragment<TFragment, TViewModel>()
    where TFragment : BaseFragment<TViewModel>, new()
    where TViewModel : ViewModelBase
        {
            var fragment = new TFragment();
            fragment.LoadVm();
            return fragment;
        }
    }
}