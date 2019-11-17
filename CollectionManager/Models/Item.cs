using CollectionManager.Composition.Base;

using Reactive.Bindings;

namespace CollectionManager.Models {
	internal class Item : ModelBase {
		public IReactiveProperty<string> FilePath {
			get;
		} = new ReactivePropertySlim<string>();

		public IReactiveProperty<string> ThumbnailFilePath {
			get;
		} = new ReactivePropertySlim<string>();

		public IReactiveProperty<Ordinal> Ordinal {
			get;
		} = new ReactivePropertySlim<Ordinal>();
	}
}
