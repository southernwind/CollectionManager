using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;

using CollectionManager.Composition.Base;
using CollectionManager.Composition.Settings;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace CollectionManager.Models {
	internal class Item : ModelBase {
		private readonly ISettings _settings;

		public IReadOnlyReactiveProperty<string> FileName {
			get;
		}

		public IReactiveProperty<string> FilePath {
			get;
		} = new ReactivePropertySlim<string>();

		public IReactiveProperty<string> ThumbnailFilePath {
			get;
		} = new ReactivePropertySlim<string>();

		public IReactiveProperty<Ordinal> Ordinal {
			get;
		} = new ReactivePropertySlim<Ordinal>();

		public ReactiveCommand LaunchCommand {
			get;
		} = new ReactiveCommand();

		public Item(ISettings settings) {
			this._settings = settings;
			this.FileName = this.FilePath.Select(Path.GetFileName).ToReadOnlyReactiveProperty().AddTo(this.CompositeDisposable);
			this.LaunchCommand.Subscribe(this.Launch);
		}

		public void Launch() {
			var tool = this._settings.TargetExtensions.First(x => x.ExtensionText.Value == Path.GetExtension(this.FilePath.Value)).SupportedExternalTool.Value;
			try {
				Process.Start(tool.Command.Value, $"{this.FilePath.Value} {tool.Arguments.Value}");
			} catch {
				Console.WriteLine("起動失敗");
			}
		}
	}
}
