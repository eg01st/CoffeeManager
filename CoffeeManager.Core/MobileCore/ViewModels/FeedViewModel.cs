using System.Collections.Specialized;
using System.Threading.Tasks;
using MobileCore.Collections;
using MobileCore.Extensions;
using MobileCore.Lists;
using MvvmCross.Core.ViewModels;

namespace MobileCore.ViewModels
{
    public class FeedViewModel<TFeedItemViewModel> : PageViewModel where TFeedItemViewModel : FeedItemElementViewModel
    {
        private int totalCount;
        private bool nextPageIsEmpty;

        protected FeedViewModel()
        {
            ItemsCollection = new CustomObservableCollection<TFeedItemViewModel>();
            ItemsCollection.CollectionChanged += OnCollectionChanged;

            LoadNextPageCommand = new MvxAsyncCommand(LoadNextPageAsync, CanLoadNextPage);

            ItemSelectedCommand = new MvxAsyncCommand<TFeedItemViewModel>(OnItemSelectedAsync);
        }
        
        public MvxAsyncCommand<TFeedItemViewModel> ItemSelectedCommand { get; }

        public CustomObservableCollection<TFeedItemViewModel> ItemsCollection { get; }

        public bool IsEmpty => ItemsCollection.IsNullOrEmpty();

        public int ItemsCount => ItemsCollection.Count;
        
        public int TotalCount
        {
            get { return totalCount; }
            protected set
            {
                totalCount = value;
                RaisePropertyChanged();
            }
        }

        public MvxAsyncCommand LoadNextPageCommand { get; }
        
        protected virtual async Task OnItemSelectedAsync(TFeedItemViewModel item) 
        {
            item.ThrowIfNull(nameof(item));

            item.SelectCommand.Execute();

            await Task.Yield();
        }
        
        protected virtual void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(IsEmpty));
            RaisePropertyChanged(nameof(ItemsCount));
        }
        
        protected override async Task DoLoadDataImplAsync()
        {
            var collection = await GetPageAsync(0);
            if(collection == null)
            {
                return;
            }
            TotalCount = collection.TotalCount;

            ItemsCollection.ReplaceWith(collection.Items);
        }

        protected override async Task RefreshDataAsync()
        {
            LoadNextPageCommand.Cancel();

            await DoLoadDataImplAsync();
        }
	    
        protected virtual async Task LoadNextPageAsync()
        {
            IsLoading = true;
            var skip = ItemsToSkipCount;
            var nextPage = await GetPageAsync(skip);
            nextPageIsEmpty = nextPage.TotalCount == 0;
            TotalCount = nextPage.TotalCount; 
            ItemsCollection.AddRange(nextPage.Items);
            IsLoading = false;
        }
        
        protected virtual bool CanLoadNextPage()
        {
            if (IsEmpty)
            {
                return false;
            }

            if (IsLoading)
            {
                return false;
            }

            if(nextPageIsEmpty)
            {
                return false;
            }

            //if (ItemsToSkipCount >= TotalCount)
            //{
            //    return false;
            //}
            
//            if (!(await HasInternetConnectionAsync()))
//            {
//                return false;
//            }

            return true;
        } 

        protected virtual int ItemsToSkipCount => ItemsCount;

        protected virtual Task<PageContainer<TFeedItemViewModel>> GetPageAsync(int skip)
        {
            var page = ProduceEmptyPage();
            return Task.FromResult(page);
        }

        protected static PageContainer<TFeedItemViewModel> ProduceEmptyPage(int totalCount = 0) 
            => new PageContainer<TFeedItemViewModel>(null, totalCount);
    }
}