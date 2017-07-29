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
                new TabItem("Кофе", coffee),
                new TabItem("Чай", tea),
                new TabItem("Сладости", sweets),
                new TabItem("Вода", water),
                new TabItem("Добавки", adds),
                new TabItem("Еда", meals),
                new TabItem("Хол напитки", coldDrinks),
                new TabItem("Мороженое", iceCream),
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