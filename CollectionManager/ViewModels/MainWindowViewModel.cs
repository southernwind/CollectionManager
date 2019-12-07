using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

using CollectionManager.Composition.Base;
using CollectionManager.Composition.Enums;
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

		public ReadOnlyReactiveCollection<Col> Columns {
			get;
		}

		public IReactiveProperty<string> FilterWord {
			get;
		}

		public ReactiveCollection<AvailableColumns> SortConditions {
			get;
		} = new ReactiveCollection<AvailableColumns>();

		public ReactiveCommand<AvailableColumns> ChangeSortConditionCommand {
			get;
		} = new ReactiveCommand<AvailableColumns>();

		public ReactiveCommand ReloadCommand {
			get;
		} = new ReactiveCommand();

		public MainWindowViewModel(IDialogService dialogService, Shelf shelf) {
			this.ItemSetList = shelf.SortedItemSetList.ToReadOnlyReactiveCollection(x => new ItemSetViewModel(x)).AddTo(this.CompositeDisposable);
			this.SortConditions.AddRange(Enum.GetValues(typeof(AvailableColumns)).Cast<AvailableColumns>());
			this.FilterWord = shelf.FilterWord.ToReactivePropertyAsSynchronized(x => x.Value).AddTo(this.CompositeDisposable);
			this.CurrentItemSet = shelf.CurrentItemSet.ToReactivePropertyAsSynchronized(
				x => x.Value,
				x => x == null ? null : new ItemSetViewModel(x),
				x => x?.Model);
			this.Columns = shelf.Columns.ToReadOnlyReactiveCollection().AddTo(this.CompositeDisposable);

			this.OpenSettingsWindow.Subscribe(x => {
				dialogService.Show(nameof(Views.SettingsWindow), null, _ => { });
			}).AddTo(this.CompositeDisposable);

			this.CurrentItemSet.Where(x => x != null).Subscribe(x => {
				x.LoadActualFilesCommand.Execute();
			}).AddTo(this.CompositeDisposable);
			shelf.Load();

			this.ChangeSortConditionCommand.Subscribe(shelf.ChangeSortCondition);

			this.ReloadCommand.Subscribe(shelf.Load).AddTo(this.CompositeDisposable);
		}
	}
}
