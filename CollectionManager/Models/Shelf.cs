
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

using CollectionManager.Composition.Base;
using CollectionManager.Composition.Settings;

using Reactive.Bindings;

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
			this.ItemSetList.Clear();
			this.ItemSetList.AddRange(this._settings.ScanDirectories.SelectMany(Directory.EnumerateDirectories).Select(x => {
				var itemSet = new ItemSet();
				itemSet.Title.Value = Path.GetFileName(x);
				itemSet.DirectoryPath.Value = x;
				return itemSet;
			}));
		}
	}
}
