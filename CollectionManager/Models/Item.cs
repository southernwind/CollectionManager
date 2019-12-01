using System.IO;
using System.Reactive.Linq;

using CollectionManager.Composition.Base;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace CollectionManager.Models {
	internal class Item : ModelBase {

		public IReadOnlyReactiveProperty<string> FileName {
			get;
		}

		public IReactiveProperty<string> FilePath {
			get;
		} = new ReactivePropertySlim<string>();

		public IReactiveProperty<string> ThumbnailFilePath {
			get;
		} = new ReactivePropertySlim<string>();

		public IReactiveProperty<Ordinal> Ordinal {
			get;
		} = new ReactivePropertySlim<Ordinal>();

		public Item() {
			this.FileName = this.FilePath.Select(Path.GetFileName).ToReadOnlyReactiveProperty().AddTo(this.CompositeDisposable);
		}
	}
}
