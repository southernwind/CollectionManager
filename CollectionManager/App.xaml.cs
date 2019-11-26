using System.Windows;
using CollectionManager.Composition.Settings;
using CollectionManager.DataBase;
using CollectionManager.Models.Settings;
using CollectionManager.ViewModels;
using CollectionManager.Views;

using Microsoft.Data.Sqlite;

using Prism.Ioc;

namespace CollectionManager {
	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	public partial class App {

		protected override Window CreateShell() {
			// DataBase
			var sb = new SqliteConnectionStringBuilder {
				DataSource = "./database.db"
			};
			var dbContext = new CollectionManagerDbContext(new SqliteConnection(sb.ConnectionString));
			dbContext.Database.EnsureCreated();

			return this.Container.Resolve<MainWindow>();
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry) {
			containerRegistry.Register<ISettings, Settings>();
			containerRegistry.RegisterDialog<SettingsWindow, SettingsWindowViewModel>();
		}
	}
}
