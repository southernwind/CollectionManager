using System.Windows;
using Prism.Ioc;
using CollectionManager.Views;
using CollectionManager.ViewModels;

namespace CollectionManager {
	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	public partial class App {

		protected override Window CreateShell() {
			return this.Container.Resolve<MainWindow>();
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry) {
			containerRegistry.RegisterDialog<SettingsWindow, SettingsWindowViewModel>();
		}
	}
}
