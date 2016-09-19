using Fragment = Android.Support.V4.App.Fragment;

namespace CoffeeManager.Droid.Entities
{
    public class TabItem
    {
        public const int DefaultIconId = -1;

        private readonly string title;
        private readonly Fragment fragment;
        private readonly int iconId;
        private readonly string testingIdentifier;

        public TabItem(string title, Fragment fragment, int iconId = DefaultIconId)
        {
            this.title = title;
            this.fragment = fragment;
            this.iconId = iconId;
        }

        public string Title => title;

        public Fragment Fragment => fragment;

        public int IconId => iconId;

        public string TestingIdentifier => testingIdentifier;
    }
}