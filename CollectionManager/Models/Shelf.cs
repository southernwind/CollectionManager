
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

using CollectionManager.Composition.Base;
using CollectionManager.Composition.Settings;
using CollectionManager.DataBase;

using Reactive.Bindings;

namespace CollectionManager.Models {
	internal class Shelf : ModelBase {
		private readonly ISettings _settings;
		private readonly CollectionManagerDbContext _database;

		public ReactiveCollection<ItemSet> ItemSetList {
			get;
		} = new ReactiveCollection<ItemSet>();

		public IReactiveProperty<ItemSet> CurrentItemSet {
			get;
		} = new ReactivePropertySlim<ItemSet>();

		public Shelf(ISettings settings, CollectionManagerDbContext database) {
			this._settings = settings;
			this._database = database;
		}

		public void Load() {
			var rows = this._database.ItemSets.ToArray();
			this.ItemSetList.Clear();
			this.ItemSetList.AddRange(this._settings.ScanDirectories.SelectMany(Directory.EnumerateDirectories).Select(x => {
				var itemSet = new ItemSet(x, this._database);
				itemSet.Title.Value = Path.GetFileName(x);
				var row = rows.FirstOrDefault(r => r.DirectoryPath == x);
				if (row == null) {
					itemSet.Create();
				} else {
					itemSet.LoadDatabase(row);
				}

				return itemSet;
			}));
		}
	}
}
