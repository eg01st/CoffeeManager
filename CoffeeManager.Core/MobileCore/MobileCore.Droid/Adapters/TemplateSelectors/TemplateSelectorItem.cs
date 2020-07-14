using System;
using Java.Lang;
using MobileCore.ViewModels;

namespace MobileCore.Droid.Adapters.TemplateSelectors
{
    public class TemplateSelectorItem
    {
        public TemplateSelectorItem(int resourceId, Type itemType, Type viewHolderType)
        {
            if (!itemType.IsSubclassOf(typeof(FeedItemElementViewModel)))
            {
                throw new IllegalStateException("itemType must derived from FeedItemElementViewModel");
            }

            if (!viewHolderType.IsSubclassOf(typeof(ViewHolders.CardViewHolder)) && viewHolderType != typeof(ViewHolders.CardViewHolder))
            {
                throw new IllegalStateException("viewHolderType must derived from CardViewHolder");
            }

            ResourceId = resourceId;
            ItemType = itemType;
            ViewHolderType = viewHolderType;
        }

        public static TemplateSelectorItem Produce<TItemType, TViewHolderType>(int resourceId)
            where TItemType : FeedItemElementViewModel
            where TViewHolderType : ViewHolders.CardViewHolder
        {
            var templateSelectorItem = new TemplateSelectorItem(resourceId, typeof(TItemType), typeof(TViewHolderType));

            return templateSelectorItem;
        }

        public int ResourceId { get; }
        public Type ItemType { get; }
        public Type ViewHolderType { get; }
    }
}