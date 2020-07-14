using System;
using System.Collections.Generic;
using MobileCore.Extensions;
using MobileCore.ViewModels;

namespace MobileCore.Droid.Adapters.TemplateSelectors
{
	public class TemplateSelector : ITemplateSelector
	{
		private readonly Dictionary<Type, int> ItemTypeToViewTypeIdMappings = new Dictionary<Type, int>();
		private readonly Dictionary<int, Type> ViewTypeIdToViewHolderTypeMappings = new Dictionary<int, Type>();
		private readonly Dictionary<int, int> ViewTypeIdToResourceIdMappings = new Dictionary<int, int>();

		public TemplateSelector()
		{
		}

		public TemplateSelector(IEnumerable<TemplateSelectorItem> items)
		{
			items.ThrowIfNull(nameof(items));

			if (items.IsNullOrEmpty())
			{
				throw new ArgumentException("items cannot be null or empty");
			}

			foreach (var item in items)
			{
				AddElement(item);
			}
		}

		public TemplateSelector(TemplateSelectorItem item) : this(new TemplateSelectorItem[] { item })
		{
		}

		public TemplateSelector(int resourceId, Type itemType, Type viewHolderType) : this(new TemplateSelectorItem[] { new TemplateSelectorItem(resourceId, itemType, viewHolderType) })
		{
		}

		public TemplateSelector AddElement<TItemType, TViewHolderType>(int resourceId)
			where TItemType : FeedItemElementViewModel
			where TViewHolderType : ViewHolders.CardViewHolder
		{
			var element = TemplateSelectorItem.Produce<TItemType, TViewHolderType>(resourceId);
			AddElement(element);

			return this;
		}

		public void AddElement(TemplateSelectorItem item)
		{
			item.ThrowIfNull(nameof(item));

			item.ItemType.Name.ThrowIfNull(nameof(item.ItemType));
			item.ViewHolderType.Name.ThrowIfNull(nameof(item.ViewHolderType));

			var viewTypeId = $"{item.ItemType.Name}.{item.ViewHolderType}".GetHashCode();

			ItemTypeToViewTypeIdMappings.TryAdd(item.ItemType, viewTypeId);
			ViewTypeIdToViewHolderTypeMappings.TryAdd(viewTypeId, item.ViewHolderType);
			ViewTypeIdToResourceIdMappings.TryAdd(viewTypeId, item.ResourceId);
		}

		public int GetItemLayoutId(int fromViewType)
		{
			int resourceId;

			if (!ViewTypeIdToResourceIdMappings.TryGetValue(fromViewType, out resourceId))
			{
				throw new ArgumentOutOfRangeException($"ViewTypeIdToResourceIdMappings doesn't contain key {fromViewType}");
			}

			return resourceId;
		}

		public int GetItemViewType(object forItemObject)
		{
			int viewTypeId;

			if (!ItemTypeToViewTypeIdMappings.TryGetValue(forItemObject.GetType(), out viewTypeId))
			{
				throw new ArgumentOutOfRangeException($"ItemTypeToViewTypeIdMappings doesn't contain key {forItemObject.GetType()}");
			}

			return viewTypeId;
		}

		public Type GetItemViewHolderType(int fromViewType)
		{
			Type viewHolderType;

			if (!ViewTypeIdToViewHolderTypeMappings.TryGetValue(fromViewType, out viewHolderType))
			{
				throw new ArgumentOutOfRangeException($"ViewTypeIdToViewHolderTypeMappings doesn't contain key {fromViewType}");
			}

			return viewHolderType;
		}
	}
}