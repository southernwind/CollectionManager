using CollectionManager.Composition.Objects;
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
		/// 対象拡張子
		/// </summary>
		SettingsCollection<Extension> TargetExtensions {
			get;
		}

		/// <summary>
		/// 外部ツール
		/// </summary>
		SettingsCollection<ExternalTool> ExternalTools {
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
