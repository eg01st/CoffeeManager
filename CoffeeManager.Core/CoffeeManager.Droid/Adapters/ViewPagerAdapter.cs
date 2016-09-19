using System;
using System.Collections.Generic;
using Android.Support.V4.App;
using Java.Lang;



namespace CoffeeManager.Droid.Adapters
{
    public class ViewPagerAdapter : FragmentPagerAdapter
    {
        private readonly List<Tuple<string, Fragment>> fragments = new List<Tuple<string, Fragment>>();

        public ViewPagerAdapter(FragmentManager manager)
            : base(manager)
        {
        }

        public override int Count => fragments.Count;

        public override Fragment GetItem(int position)
        {
            var element = GetElementAt(position);

            return element.Item2;
        }

        public void AddFragment(Fragment fragment, string title)
        {
            var keyValuePair = new Tuple<string, Fragment>(title, fragment);
            fragments.Add(keyValuePair);
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            var element = GetElementAt(position);
            var titleStr = element.Item1;
            var javaStr = new Java.Lang.String(titleStr);

            return javaStr;
        }

        private Tuple<string, Fragment> GetElementAt(int position)
        {
            var element = fragments[position];

            return element;
        }
    }
}