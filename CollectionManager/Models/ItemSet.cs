using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text.RegularExpressions;

using CollectionManager.Composition.Base;
using CollectionManager.Composition.Settings;
using CollectionManager.DataBase;
using CollectionManager.DataBase.Tables;

using Microsoft.EntityFrameworkCore;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace CollectionManager.Models {
	internal class ItemSet : ModelBase {
		private readonly CollectionManagerDbContext _database;
		private readonly ISettings _settings;

		/// <summary>
		/// アイテムセットID
		/// </summary>
		private int _itemSetId;

		private bool _fileLoadedFlag;

		public string DirectoryPath {
			get;
		}

		public ReactiveCollection<Item> ItemList {
			get;
		} = new ReactiveCollection<Item>();

		public IReactiveProperty<string> Title {
			get;
		} = new ReactivePropertySlim<string>();

		public IReactiveProperty<string[]> Authors {
			get;
		} = new ReactivePropertySlim<string[]>();

		public IReactiveProperty<string> Note {
			get;
		} = new ReactivePropertySlim<string>();

		public ReactiveProperty<string> OrdinalRegex {
			get;
		} = new ReactiveProperty<string>(@"^.*? (?<number>\d+)\..*?$");

		public IReactiveProperty<double?> Min {
			get;
		} = new ReactivePropertySlim<double?>();

		public IReactiveProperty<double?> Max {
			get;
		} = new ReactivePropertySlim<double?>();

		public IReactiveProperty<bool> Completed {
			get;
		} = new ReactivePropertySlim<bool>();

		public IReactiveProperty<DateTime?> NextReleaseDate {
			get;
		} = new ReactivePropertySlim<DateTime?>();

		public ItemSet(string directoryPath, CollectionManagerDbContext database, ISettings settings) {
			this.DirectoryPath = directoryPath;
			this._database = database;
			this._settings = settings;

			this.ItemList.CollectionChangedAsObservable().Subscribe(_ => this.CalculateMinMax()).AddTo(this.CompositeDisposable);

			this.Min.ToUnit()
				.Merge(this.Max.ToUnit())
				.Merge(this.OrdinalRegex.ToUnit())
				.Merge(this.Note.ToUnit())
				.Merge(this.Title.ToUnit())
				.Merge(this.Authors.ToUnit())
				.Merge(this.Completed.ToUnit())
				.Merge(this.NextReleaseDate.ToUnit())
				.Where(_ => this._itemSetId != default)
				.Throttle(TimeSpan.FromSeconds(1))
				.Subscribe(_ => this.Update())
				.AddTo(this.CompositeDisposable);

			this.OrdinalRegex.SetValidateNotifyError(x => {
				try {
					_ = new Regex(x);
					return null;
				} catch {
					return "正規表現検証エラー";
				}
			});
			this.OrdinalRegex.Where(x => !this.OrdinalRegex.HasErrors && this._fileLoadedFlag).Subscribe(x => {
				var regex = new Regex(this.OrdinalRegex.Value);
				foreach (var item in this.ItemList) {
					var match = regex.Match(Path.GetFileName(item.FilePath.Value));
					if (match.Success) {
						item.Ordinal.Value = new Ordinal { Number = int.Parse(match.Groups["number"].Value) };
					}
				}

				this.CalculateMinMax();
			});
		}

		public void OpenDirectory() {
			try {
				Process.Start("explorer.exe", $"\"{this.DirectoryPath}\"");
			} catch (Exception ex) {
				Console.WriteLine(ex);
			}
		}

		/// <summary>
		/// データベースレコードの作成
		/// </summary>
		public void Create() {
			var row = new DataBase.Tables.ItemSet {
				DirectoryPath = this.DirectoryPath,
				OrdinalRegex = this.OrdinalRegex.Value
			};
			lock (this._database) {
				this._database.ItemSets.Add(row);
				this._database.SaveChanges();
			}

			this._itemSetId = row.ItemSetId;
		}

		public void LoadDatabase(DataBase.Tables.ItemSet row) {
			this.Authors.Value = row.Authors?.Select(x => x.Name).ToArray();
			this.Note.Value = row.Note;
			this.Min.Value = row.Min;
			this.Max.Value = row.Max;
			this.OrdinalRegex.Value = row.OrdinalRegex;
			this.Completed.Value = row.Completed;
			this.NextReleaseDate.Value = row.NextReleaseDate;

			this._itemSetId = row.ItemSetId;
		}

		/// <summary>
		/// ファイル読み込み
		/// </summary>
		public void LoadActualFiles() {
			this.ItemList.Clear();
			Regex regex = null;
			try {
				regex = new Regex(this.OrdinalRegex.Value);
			} catch {
				Console.WriteLine("正規表現検証エラー");
			}
			foreach (var file in Directory.EnumerateFiles(this.DirectoryPath).Where(x => this._settings.TargetExtensions.Select(x => x.ExtensionText.Value).Contains(Path.GetExtension(x)))) {
				var match = regex?.Match(Path.GetFileName(file));
				var item = new Item(this._settings);
				item.FilePath.Value = file;
				if (match?.Success ?? false) {
					item.Ordinal.Value = new Ordinal { Number = int.Parse(match.Groups["number"].Value) };
				}

				this.ItemList.Add(item);
			}

			this._fileLoadedFlag = true;
		}

		/// <summary>
		/// データベース情報最新化
		/// </summary>
		private void Update() {
			lock (this._database) {
				var row = this._database.ItemSets.Include(x => x.Authors).First(x => x.ItemSetId == this._itemSetId);
				this._database.ItemSetAuthors.RemoveRange(row.Authors);
				row.Authors = this.Authors.Value?.Select(x => new ItemSetAuthor { Name = x }).ToArray() ??
							  Array.Empty<ItemSetAuthor>();
				row.Note = this.Note.Value;
				row.Min = this.Min.Value;
				row.Max = this.Max.Value;
				row.OrdinalRegex = this.OrdinalRegex.Value;
				row.Completed = this.Completed.Value;
				row.NextReleaseDate = this.NextReleaseDate.Value;
				this._database.SaveChanges();
			}
		}

		private void CalculateMinMax() {
			if (!this.ItemList.Any()) {
				this.Min.Value = null;
				this.Max.Value = null;
				return;
			}
			this.Min.Value = this.ItemList.Select(x => x.Ordinal.Value?.Number).Min();
			this.Max.Value = this.ItemList.Select(x => x.Ordinal.Value?.Number).Max();
		}
	}
}
