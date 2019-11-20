using System;
using System.IO;
using System.Threading.Tasks;

using CollectionManager.Composition.Base;

using Reactive.Bindings;

namespace CollectionManager.ViewModels {
	internal class MainWindowViewModel : ViewModelBase {
		public ReactiveCollection<string> Files {
			get;
			set;
		} = new ReactiveCollection<string>();

		public MainWindowViewModel() {
		}

		public async Task LoadFiles() {
		}
	}
}
