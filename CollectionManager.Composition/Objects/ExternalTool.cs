using Prism.Mvvm;

using Reactive.Bindings;

namespace CollectionManager.Composition.Objects {
	public class ExternalTool : BindableBase {
		public IReactiveProperty<string> DisplayName {
			get;
			set;
		} = new ReactiveProperty<string>();
		public IReactiveProperty<string> Command {
			get;
			set;
		} = new ReactiveProperty<string>();

		public IReactiveProperty<string> Arguments {
			get;
			set;
		} = new ReactiveProperty<string>();
	}
}
