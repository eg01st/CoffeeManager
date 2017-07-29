using System;

using UIKit;
using CoffeManager.Common;
using MvvmCross.Binding.BindingContext;
using System.Drawing;

namespace CoffeeManagerAdmin.iOS
{
    public abstract partial class SearchViewController<TView, TViewModel, TItemViewModel> : ViewControllerBase 
        where TView : SearchViewController<TView, TViewModel, TItemViewModel>
        where TItemViewModel : ListItemViewModelBase
        where TViewModel : BaseSearchViewModel<TItemViewModel> 
    {
        public SearchViewController() : base("SearchViewController", null)
        {
        }

        protected UITableView TableView => SearchTableView;
        private UISearchBar _searchBar;


        protected abstract MvxFluentBindingDescriptionSet<TView, TViewModel> CreateBindingSet();

        protected abstract SimpleTableSource TableSource {get;}
        
        public override void ViewDidLoad()
        {
  
  
          base.ViewDidLoad();
        }

        protected override void DoBind()
        {
            _searchBar = new UISearchBar(new RectangleF(0, 0, (float)View.Frame.Width, 44))
            {
                AutocorrectionType = UITextAutocorrectionType.No
            };
            SearchTableView.TableHeaderView = _searchBar;
            var tableSource = TableSource;
            SearchTableView.Source = tableSource;

            var set = CreateBindingSet();
            set.Bind(tableSource).To(vm => vm.Items);
            set.Bind(_searchBar).For(v => v.Text).To(vm => vm.SearchString);
            set.Apply();
        }

    }
}

