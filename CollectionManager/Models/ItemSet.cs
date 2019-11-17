using CollectionManager.Composition.Base;

using Reactive.Bindings;

namespace CollectionManager.Models {
	internal class ItemSet : ModelBase {
		public ReactiveCollection<Item> ItemList {
			get;
		} = new ReactiveCollection<Item>();

		public IReactiveProperty<string> Title {
			get;
		} = new ReactivePropertySlim<string>();

		public IReactiveProperty<string[]> Authors {
			get;
		} = new ReactivePropertySlim<string[]>();

		public IReactiveProperty<string> Note {
			get;
		} = new ReactivePropertySlim<string>();

		public ItemSet() {

		}
	}
}
