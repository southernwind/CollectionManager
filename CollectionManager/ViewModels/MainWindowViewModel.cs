using System;
using System.Linq;
using System.Reactive.Linq;

using CollectionManager.Composition.Base;
using CollectionManager.Models;

using Prism.Services.Dialogs;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace CollectionManager.ViewModels {
	internal class MainWindowViewModel : ViewModelBase {
		public ReadOnlyReactiveCollection<ItemSetViewModel> ItemSetList {
			get;
		}

		public IReactiveProperty<ItemSetViewModel> CurrentItemSet {
			get;
		}

		public ReactiveCommand OpenSettingsWindow {
			get;
		} = new ReactiveCommand();

		public MainWindowViewModel(IDialogService dialogService, Shelf shelf) {
			this.ItemSetList = shelf.ItemSetList.ToReadOnlyReactiveCollection(x => new ItemSetViewModel(x)).AddTo(this.CompositeDisposable);
			this.CurrentItemSet = shelf.CurrentItemSet.ToReactivePropertyAsSynchronized(
				x => x.Value,
				x => x == null ? null : new ItemSetViewModel(x),
				x => x?.Model);

			this.OpenSettingsWindow.Subscribe(x => {
				dialogService.Show(nameof(Views.SettingsWindow), null, _ => { });
			}).AddTo(this.CompositeDisposable);

			this.CurrentItemSet.Where(x => x != null).Subscribe(x => {
				x.LoadActualFilesCommand.Execute();
			}).AddTo(this.CompositeDisposable);
			shelf.Load();
		}
	}
}
