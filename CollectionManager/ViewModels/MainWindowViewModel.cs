using System;
using System.IO;
using System.Threading.Tasks;

using CollectionManager.Composition.Base;
using CollectionManager.Models;
using Reactive.Bindings;

namespace CollectionManager.ViewModels {
	internal class MainWindowViewModel : ViewModelBase {
		public ReactiveCollection<ItemSet> ItemSetList {
			get;
		} = new ReactiveCollection<ItemSet>();

		public MainWindowViewModel() {
			var itemset = new ItemSet();
			itemset.Authors.Value = new[] { "ADA" };
			itemset.Title.Value = "Aqua Journal";
			var item289 = new Item();
			item289.FilePath.Value = "./[289]ネイチャーアクアリウム 陰性水草の印象.pdf";
			item289.Ordinal.Value = new Ordinal() { Number = 289 };
			itemset.ItemList.Add(item289);
			var item288 = new Item();
			item288.FilePath.Value = "./[288]失われた潟／古木と芽吹き.pdf";
			item288.Ordinal.Value = new Ordinal() { Number = 289 };
			itemset.ItemList.Add(item288);

			this.ItemSetList.Add(itemset);
		}
	}
}
