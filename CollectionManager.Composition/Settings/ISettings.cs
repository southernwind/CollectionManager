using CollectionManager.Composition.Settings.Objects;

namespace CollectionManager.Composition.Settings {
	public interface ISettings {
		/// <summary>
		/// スキャン設定
		/// </summary>
		SettingsCollection<string> ScanDirectories {
			get;
		}

		/// <summary>
		/// 保存
		/// </summary>
		void Save();

		/// <summary>
		/// 設定ロード
		/// </summary>
		void Load();
	}
}
