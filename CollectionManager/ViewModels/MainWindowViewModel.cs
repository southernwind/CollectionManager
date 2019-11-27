using System;
using System.Linq;
using System.Reactive.Linq;
using CollectionManager.Composition.Base;
using CollectionManager.Models;

using Prism.Services.Dialogs;

using Reactive.Bindings;

namespace CollectionManager.ViewModels {
	internal class MainWindowViewModel : ViewModelBase {
		public ReactiveCollection<ItemSetViewModel> ItemSetList {
			get;
		} = new ReactiveCollection<ItemSetViewModel>();

		public IReactiveProperty<ItemSetViewModel> CurrentItemSet {
			get;
		} = new ReactivePropertySlim<ItemSetViewModel>();

		public ReactiveCommand OpenSettingsWindow {
			get;
		} = new ReactiveCommand();

		public MainWindowViewModel(IDialogService dialogService) {
			var itemset = new ItemSetViewModel();
			itemset.Authors.Value = new[] { "ADA" };
			itemset.DirectoryPath.Value = @"C:\test";
			itemset.Title.Value = "Aqua Journal";
			var item289 = new Item();
			item289.FilePath.Value = "./[289]ネイチャーアクアリウム 陰性水草の印象.pdf";
			item289.Ordinal.Value = new Ordinal() { Number = 289 };
			itemset.AddItem(item289);
			var item288 = new Item();
			item288.FilePath.Value = "./[288]失われた潟／古木と芽吹き.pdf";
			item288.Ordinal.Value = new Ordinal() { Number = 289 };
			itemset.AddItem(item288);

			this.ItemSetList.Add(itemset);
			this.CurrentItemSet.Value = this.ItemSetList.First();

			this.OpenSettingsWindow.Subscribe(x => {
				dialogService.Show(nameof(Views.SettingsWindow), null, _ => { });
			});

			this.CurrentItemSet.Where(x => x != null).Subscribe(x => {
				x.LoadCommand.Execute();
			});
		}
	}
}
