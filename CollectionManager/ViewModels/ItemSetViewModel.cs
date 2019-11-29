
using CollectionManager.Composition.Base;
using CollectionManager.Models;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace CollectionManager.ViewModels {
	internal class ItemSetViewModel : ViewModelBase {
		private readonly ItemSet _model;
		public IReactiveProperty<string> DirectoryPath {
			get;
		}

		public ReadOnlyReactiveCollection<Item> ItemList {
			get;
		}

		public IReactiveProperty<string> Title {
			get;
		}

		public IReactiveProperty<string[]> Authors {
			get;
		}

		public IReactiveProperty<string> Note {
			get;
		}

		public IReactiveProperty<string> OrdinalRegex {
			get;
		}

		public IReadOnlyReactiveProperty<double> Min {
			get;
		}

		public IReadOnlyReactiveProperty<double> Max {
			get;
		}

		public ReactiveCommand OpenDirectoryCommand {
			get;
			set;
		} = new ReactiveCommand();

		public ReactiveCommand LoadCommand {
			get;
		} = new ReactiveCommand();

		public ItemSetViewModel() {
			this._model = new ItemSet().AddTo(this.CompositeDisposable);
			this.DirectoryPath = this._model.DirectoryPath.ToReactivePropertyAsSynchronized(x => x.Value).AddTo(this.CompositeDisposable);
			this.ItemList = this._model.ItemList.ToReadOnlyReactiveCollection().AddTo(this.CompositeDisposable);
			this.Title = this._model.Title.ToReactivePropertyAsSynchronized(x => x.Value).AddTo(this.CompositeDisposable);
			this.Authors = this._model.Authors.ToReactivePropertyAsSynchronized(x => x.Value).AddTo(this.CompositeDisposable);
			this.Note = this._model.Note.ToReactivePropertyAsSynchronized(x => x.Value).AddTo(this.CompositeDisposable);
			this.OrdinalRegex = this._model.OrdinalRegex.ToReactivePropertyAsSynchronized(x => x.Value).AddTo(this.CompositeDisposable);
			this.Min = this._model.Min.ToReadOnlyReactivePropertySlim().AddTo(this.CompositeDisposable);
			this.Max = this._model.Max.ToReadOnlyReactivePropertySlim().AddTo(this.CompositeDisposable);

			this.OpenDirectoryCommand.Subscribe(this._model.OpenDirectory).AddTo(this.CompositeDisposable);
			this.LoadCommand.Subscribe(this._model.Load).AddTo(this.CompositeDisposable);
		}

		public void AddItem(Item item) {
			this._model.ItemList.Add(item);
		}
	}
}
