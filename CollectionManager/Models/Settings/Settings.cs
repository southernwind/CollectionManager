using System;
using System.Collections.Generic;
using System.IO;
using System.Xaml;
using System.Xml;

using CollectionManager.Composition.Settings;
using CollectionManager.Composition.Settings.Objects;

namespace CollectionManager.Models.Settings {
	public class Settings : SettingsBase, ISettings {
		private readonly string _settingsFilePath;
		/// <summary>
		/// スキャン設定
		/// </summary>
		public SettingsCollection<string> ScanDirectories {
			get;
		} = new SettingsCollection<string>(Array.Empty<string>());

		[Obsolete("for serialize")]
		public Settings() {
		}

		public Settings(string path) {
			this._settingsFilePath = path;
		}

		/// <summary>
		/// 保存
		/// </summary>
		public void Save() {
			using var ms = new MemoryStream();
			XamlServices.Save(ms, this.Export());
			try {
				using var fs = File.Create(this._settingsFilePath);
				ms.WriteTo(fs);
			} catch (IOException ex) {
				Console.WriteLine($"設定保存失敗{ex}");
			}
		}

		/// <summary>
		/// 設定ロード
		/// </summary>
		public void Load() {
			this.LoadDefault();
			if (!File.Exists(this._settingsFilePath)) {
				Console.WriteLine("設定ファイルなし");
				return;
			}

			try {
				if (!(XamlServices.Load(this._settingsFilePath) is Dictionary<string, dynamic> settings)) {
					Console.WriteLine("設定ファイル読み込み失敗");
					return;
				}

				this.Import(settings);
			} catch (XmlException ex) {
				Console.WriteLine($"設定ファイル読み込み失敗{ex}");
			}
		}
	}
}
