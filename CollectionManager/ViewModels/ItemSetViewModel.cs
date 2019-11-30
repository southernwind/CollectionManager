
using CollectionManager.Composition.Base;
using CollectionManager.Models;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace CollectionManager.ViewModels {
	internal class ItemSetViewModel : ViewModelBase {
		public ItemSet Model {
			get;
		}
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

		public ItemSetViewModel(ItemSet model) {
			this.Model = model;
			this.DirectoryPath = this.Model.DirectoryPath.ToReactivePropertyAsSynchronized(x => x.Value).AddTo(this.CompositeDisposable);
			this.ItemList = this.Model.ItemList.ToReadOnlyReactiveCollection().AddTo(this.CompositeDisposable);
			this.Title = this.Model.Title.ToReactivePropertyAsSynchronized(x => x.Value).AddTo(this.CompositeDisposable);
			this.Authors = this.Model.Authors.ToReactivePropertyAsSynchronized(x => x.Value).AddTo(this.CompositeDisposable);
			this.Note = this.Model.Note.ToReactivePropertyAsSynchronized(x => x.Value).AddTo(this.CompositeDisposable);
			this.OrdinalRegex = this.Model.OrdinalRegex.ToReactivePropertyAsSynchronized(x => x.Value).AddTo(this.CompositeDisposable);
			this.Min = this.Model.Min.ToReadOnlyReactivePropertySlim().AddTo(this.CompositeDisposable);
			this.Max = this.Model.Max.ToReadOnlyReactivePropertySlim().AddTo(this.CompositeDisposable);

			this.OpenDirectoryCommand.Subscribe(this.Model.OpenDirectory).AddTo(this.CompositeDisposable);
			this.LoadCommand.Subscribe(this.Model.Load).AddTo(this.CompositeDisposable);
		}
	}
}
