using System;

using Prism.Mvvm;

using Reactive.Bindings;

namespace CollectionManager.Composition.Objects {
	public class Extension : BindableBase {
		/// <summary>
		/// 拡張子テキスト
		/// </summary>
		public IReactiveProperty<string> ExtensionText {
			get;
			set;
		} = new ReactiveProperty<string>();

		/// <summary>
		/// 対応する外部ツール
		/// </summary>
		public IReactiveProperty<ExternalTool> SupportedExternalTool {
			get;
			set;
		} = new ReactiveProperty<ExternalTool>();

		[Obsolete("for serialize")]
		public Extension() {

		}

		public Extension(string extensionText) {
			this.ExtensionText.Value = extensionText;
		}
		public bool Equals(Extension other) {
			return
				other != null &&
					this.ExtensionText.Value == other.ExtensionText.Value &&
					this.SupportedExternalTool.Value == other.SupportedExternalTool.Value;
		}

	}
}
