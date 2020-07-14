using System;
using MvvmCross.Droid.Support.V7.RecyclerView.ItemTemplates;

namespace MobileCore.Droid.Adapters.TemplateSelectors
{
    public interface ITemplateSelector : IMvxTemplateSelector
    {
        Type GetItemViewHolderType(int templateId);
    }
}