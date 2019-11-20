using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using CollectionManager.ViewModels;
using Reactive.Bindings;


namespace CollectionManager {
	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	public partial class App {
		protected override void OnStartup(StartupEventArgs e) {
			// 画面起動
			this.MainWindow = new Views.MainWindow {
				DataContext = new MainWindowViewModel()
			};
			this.MainWindow.ShowDialog();
		}
	}
}
