
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

		public ReactiveCommand OpenDirectoryCommand {
			get;
			set;
		} = new ReactiveCommand();

		public ReactiveCommand LoadCommand {
			get;
		} = new ReactiveCommand();

		public ItemSetViewModel() {
			this._model = new ItemSet();
			this.DirectoryPath = this._model.DirectoryPath.ToReactivePropertyAsSynchronized(x => x.Value);
			this.ItemList = this._model.ItemList.ToReadOnlyReactiveCollection();
			this.Title = this._model.Title.ToReactivePropertyAsSynchronized(x => x.Value);
			this.Authors = this._model.Authors.ToReactivePropertyAsSynchronized(x => x.Value);
			this.Note = this._model.Note.ToReactivePropertyAsSynchronized(x => x.Value);

			this.OpenDirectoryCommand.Subscribe(this._model.OpenDirectory);
			this.LoadCommand.Subscribe(this._model.Load);
		}

		public void AddItem(Item item) {
			this._model.ItemList.Add(item);
		}
	}
}
