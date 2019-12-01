
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

using CollectionManager.Composition.Base;
using CollectionManager.Composition.Enums;
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

		public ReactiveCollection<ItemSet> SortedItemSetList {
			get;
		} = new ReactiveCollection<ItemSet>();

		public IReactiveProperty<ItemSet> CurrentItemSet {
			get;
		} = new ReactivePropertySlim<ItemSet>();

		/// <summary>
		/// 表示する列
		/// </summary>
		public ReadOnlyReactiveCollection<Col> Columns {
			get;
		}

		public Shelf(ISettings settings, CollectionManagerDbContext database) {
			this._settings = settings;
			this._database = database;

			var cols = new ReactiveCollection<Col>();
			cols.AddRange(new[] { new Col(AvailableColumns.Title), new Col(AvailableColumns.Max), new Col(AvailableColumns.NextReleaseDate) });
			this.Columns = cols.ToReadOnlyReactiveCollection();
		}

		public void Load() {
			DataBase.Tables.ItemSet[] rows;
			lock (this._database) {
				rows = this._database.ItemSets.ToArray();
			}

			this.ItemSetList.Clear();
			this.ItemSetList.AddRange(this._settings.ScanDirectories.SelectMany(Directory.EnumerateDirectories)
				.Select(x => {
					var itemSet = new ItemSet(x, this._database, this._settings);
					itemSet.Title.Value = Path.GetFileName(x);
					var row = rows.FirstOrDefault(r => r.DirectoryPath == x);
					if (row == null) {
						itemSet.Create();
					} else {
						itemSet.LoadDatabase(row);
					}

					return itemSet;
				}));
			this.ChangeSortCondition(AvailableColumns.Title);
		}

		public void ChangeSortCondition(AvailableColumns column) {
			var d = new Dictionary<AvailableColumns, Func<ItemSet, object>> {
				{AvailableColumns.Title, x => x.Title.Value},
				{AvailableColumns.Min, x => x.Min.Value},
				{AvailableColumns.Max, x => x.Max.Value},
				{AvailableColumns.Authors, x => x.Authors.Value},
				{AvailableColumns.Note, x => x.Note.Value},
				{AvailableColumns.NextReleaseDate, x => x.NextReleaseDate.Value},
				{AvailableColumns.Completed, x => x.Completed.Value}
			};
			this.SortedItemSetList.Clear();
			this.SortedItemSetList.AddRange(this.ItemSetList.OrderBy(d[column]));
		}
	}

	internal class Col {
		public AvailableColumns AlternateKey {
			get;
		}

		public Col(AvailableColumns alternateKey) {
			this.AlternateKey = alternateKey;
		}
	}
}
