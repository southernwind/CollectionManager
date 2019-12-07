
using System;

using CollectionManager.Composition.Base;
using CollectionManager.Models;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace CollectionManager.ViewModels {
	internal class ItemSetViewModel : ViewModelBase {
		public ItemSet Model {
			get;
		}
		public string DirectoryPath {
			get {
				return this.Model.DirectoryPath;
			}
		}

		public ReadOnlyReactiveCollection<Item> ItemList {
			get;
		}

		public IReactiveProperty<string> Title {
			get;
		}

		public IReactiveProperty<string> TitleYomi {
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

		public IReactiveProperty<bool> Completed {
			get;
		}

		public IReactiveProperty<DateTime?> NextReleaseDate {
			get;
		}

		public IReadOnlyReactiveProperty<double?> Min {
			get;
		}

		public IReadOnlyReactiveProperty<double?> Max {
			get;
		}


		public ReactiveCommand OpenDirectoryCommand {
			get;
			set;
		} = new ReactiveCommand();

		public ReactiveCommand LoadActualFilesCommand {
			get;
		} = new ReactiveCommand();

		public ItemSetViewModel(ItemSet model) {
			this.Model = model;
			this.ItemList = this.Model.ItemList.ToReadOnlyReactiveCollection().AddTo(this.CompositeDisposable);
			this.Title = this.Model.Title.ToReactivePropertyAsSynchronized(x => x.Value).AddTo(this.CompositeDisposable);
			this.TitleYomi = this.Model.TitleYomi.ToReactivePropertyAsSynchronized(x => x.Value).AddTo(this.CompositeDisposable);
			this.Authors = this.Model.Authors.ToReactivePropertyAsSynchronized(x => x.Value).AddTo(this.CompositeDisposable);
			this.Note = this.Model.Note.ToReactivePropertyAsSynchronized(x => x.Value).AddTo(this.CompositeDisposable);
			this.OrdinalRegex = this.Model.OrdinalRegex.ToReactivePropertyAsSynchronized(x => x.Value).AddTo(this.CompositeDisposable);
			this.Min = this.Model.Min.ToReadOnlyReactivePropertySlim().AddTo(this.CompositeDisposable);
			this.Max = this.Model.Max.ToReadOnlyReactivePropertySlim().AddTo(this.CompositeDisposable);
			this.Completed = this.Model.Completed.ToReactivePropertyAsSynchronized(x => x.Value).AddTo(this.CompositeDisposable);
			this.NextReleaseDate = this.Model.NextReleaseDate.ToReactivePropertyAsSynchronized(x => x.Value);
			this.OpenDirectoryCommand.Subscribe(this.Model.OpenDirectory).AddTo(this.CompositeDisposable);
			this.LoadActualFilesCommand.Subscribe(this.Model.LoadActualFiles).AddTo(this.CompositeDisposable);
		}
	}
}
