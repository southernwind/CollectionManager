using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CollectionManager.Composition.Base;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace CollectionManager.Models {
	internal class ItemSet : ModelBase {

		public IReactiveProperty<string> DirectoryPath {
			get;
		} = new ReactivePropertySlim<string>();

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

		public IReactiveProperty<string> OrdinalRegex {
			get;
		} = new ReactivePropertySlim<string>(@"^\[(?<number>\d+)\].*$");

		public IReactiveProperty<double> Min {
			get;
		} = new ReactivePropertySlim<double>();

		public IReactiveProperty<double> Max {
			get;
		} = new ReactivePropertySlim<double>();
		
		public ItemSet() {
			this.ItemList.CollectionChangedAsObservable().Subscribe(x => {
				if (!this.ItemList.Any()) {
					this.Min.Value = double.NaN;
					this.Max.Value = double.NaN;
					return;
				}
				this.Min.Value = this.ItemList.Select(x => x.Ordinal.Value.Number).Min();
				this.Max.Value = this.ItemList.Select(x => x.Ordinal.Value.Number).Max();
			});
			
		}

		public void OpenDirectory() {
			try {
				Process.Start("explorer.exe", $"\"{this.DirectoryPath.Value}\"");
			} catch (Exception ex) {
				Console.WriteLine(ex);
			}
		}

		public void Load() {
			this.ItemList.Clear();
			var regex = new Regex(this.OrdinalRegex.Value);
			foreach (var file in Directory.EnumerateFiles(this.DirectoryPath.Value)) {
				var item = new Item();
				item.FilePath.Value = file;
				var match = regex.Match(Path.GetFileName(file));
				item.Ordinal.Value = new Ordinal() { Number = int.Parse(match.Groups["number"].Value)};
				this.ItemList.Add(item);
			}
		}
	}
}
