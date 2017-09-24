using System;

using UIKit;
using CoffeManager.Common;
using MvvmCross.Binding.BindingContext;
using System.Drawing;
using Foundation;
using CoreGraphics;

namespace CoffeeManagerAdmin.iOS
{
    public abstract partial class SearchViewController<TView, TViewModel, TItemViewModel> : ViewControllerBase 
        where TView : SearchViewController<TView, TViewModel, TItemViewModel>
        where TItemViewModel : ListItemViewModelBase
        where TViewModel : BaseSearchViewModel<TItemViewModel> 
    {
        protected UITableView TableView {get;set;}
        private UISearchBar _searchBar;


        protected abstract MvxFluentBindingDescriptionSet<TView, TViewModel> CreateBindingSet();

        protected abstract SimpleTableSource TableSource {get;}

        protected abstract UIView TableViewContainer {get;}
        
        protected SearchViewController()
            :base()
        {
        }

        protected SearchViewController(string nibName)
            : base(nibName, null)
        {
        }

        protected SearchViewController(string nibName, NSBundle bundle)
            : base(nibName, bundle)
        {
        }

        protected SearchViewController(IntPtr ptr)
            : base(ptr)
        {
        }


        protected override void InitStylesAndContent()
        {
            TableView = new UITableView();
            TableView.TranslatesAutoresizingMaskIntoConstraints = false;
            TableViewContainer.AddSubview(TableView);

            _searchBar = new UISearchBar(new RectangleF(0, 0, (float)UIScreen.MainScreen.Bounds.Width, 44))
            {
                AutocorrectionType = UITextAutocorrectionType.No
            };
            TableView.TableHeaderView = _searchBar;

            TableViewContainer.AddConstraints(ConstraintExtensions.StickToAllSuperViewEdges(TableViewContainer, TableView));
        }

        protected override void DoBind()
        {
            var tableSource = TableSource;
            TableView.Source = tableSource;
            var set = CreateBindingSet();
            set.Bind(tableSource).To(vm => vm.Items);
            set.Bind(_searchBar).For(v => v.Text).To(vm => vm.SearchString);
            set.Apply();
        }

    }
}

