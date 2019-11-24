using System;
using System.Diagnostics;
using System.IO;

using CollectionManager.Composition.Base;

using Reactive.Bindings;

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

		public ItemSet() {

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
			foreach (var file in Directory.EnumerateFiles(this.DirectoryPath.Value)) {
				var item = new Item();
				item.FilePath.Value = file;

				this.ItemList.Add(item);
			}
		}
	}
}
