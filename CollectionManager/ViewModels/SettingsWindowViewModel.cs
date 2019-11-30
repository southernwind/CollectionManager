using System;
using System.Windows.Forms;

using CollectionManager.Composition.Base;
using CollectionManager.Composition.Settings;

using Prism.Services.Dialogs;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

using DialogResult = System.Windows.Forms.DialogResult;

namespace CollectionManager.ViewModels {
	internal class SettingsWindowViewModel : ViewModelBase, IDialogAware {
		/// <summary>
		/// スキャン設定
		/// </summary>
		public ReadOnlyReactiveCollection<string> ScanDirectories {
			get;
		}

		public IReactiveProperty<string> SelectedScanDirectory {
			get;
		} = new ReactiveProperty<string>();

		/// <summary>
		/// スキャン設定追加
		/// </summary>
		public ReactiveCommand AddScanDirectoryCommand {
			get;
		} = new ReactiveCommand();

		/// <summary>
		/// スキャン設定削除
		/// </summary>
		public ReactiveCommand RemoveScanDirectoryCommand {
			get;
		} = new ReactiveCommand();

		public SettingsWindowViewModel(ISettings settings) {
			this.ScanDirectories = settings.ScanDirectories.ToReadOnlyReactiveCollection().AddTo(this.CompositeDisposable);

			this.AddScanDirectoryCommand.Subscribe(() => {
				var fbd = new FolderBrowserDialog();
				if (fbd.ShowDialog() == DialogResult.OK) {
					settings.ScanDirectories.Add(fbd.SelectedPath);
				}
			});
			this.RemoveScanDirectoryCommand.Subscribe(() => {
				settings.ScanDirectories.Remove(this.SelectedScanDirectory.Value);
			});
		}

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
