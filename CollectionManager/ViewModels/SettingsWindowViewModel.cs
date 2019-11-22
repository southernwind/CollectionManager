using System;
using System.Collections.Generic;
using System.Text;
using CollectionManager.Composition.Base;
using Prism.Services.Dialogs;

namespace CollectionManager.ViewModels {
	internal class SettingsWindowViewModel : ViewModelBase, IDialogAware {
		public bool CanCloseDialog() {
			return true;
		}

		public void OnDialogClosed() {
		}

		public void OnDialogOpened(IDialogParameters parameters) {
		}

		public string Title {
			get;
		} = "設定";

		public event Action<IDialogResult> RequestClose;
	}
}
