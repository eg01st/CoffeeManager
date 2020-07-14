namespace MobileCore.ViewModels.Items
{
    public class SectionHeaderItemViewModel : FeedItemElementViewModel
    {
        private bool isExpanded = true;

        public SectionHeaderItemViewModel(string title, string righTitle = null, bool isExpandable = false)
        {
            IsExpandable = isExpandable;
            Title = title;
            RighTitle = righTitle;
        }

        public bool IsExpanded
        {
            get => isExpanded;
            set => SetProperty(ref isExpanded, value);
        }

        public bool IsExpandable { get; set; }

        public string Title { get; }
        public string RighTitle { get; }
    }
}