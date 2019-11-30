
using CollectionManager.Composition.Base;
using CollectionManager.Composition.Settings;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace CollectionManager.Models {
	internal class Shelf : ModelBase {
		private readonly ISettings _settings;

		public ReactiveCollection<ItemSet> ItemSetList {
			get;
		} = new ReactiveCollection<ItemSet>();

		public IReactiveProperty<ItemSet> CurrentItemSet {
			get;
		} = new ReactivePropertySlim<ItemSet>();

		public Shelf(ISettings settings) {
			this._settings = settings;
		}

		public void Load() {
			var itemset = new ItemSet().AddTo(this.CompositeDisposable);
			itemset.Authors.Value = new[] { "ADA" };
			itemset.DirectoryPath.Value = @"C:\test";
			itemset.Title.Value = "Aqua Journal";

			this.ItemSetList.Add(itemset);
		}
	}
}
