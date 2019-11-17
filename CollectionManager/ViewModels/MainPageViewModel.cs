using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

using CollectionManager.Composition.Base;

using Reactive.Bindings;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Media;

namespace CollectionManager.ViewModels {
	internal class MainPageViewModel : ViewModelBase {
		public ReactiveCollection<BitmapImage> Files {
			get;
			set;
		} = new ReactiveCollection<BitmapImage>();

		public MainPageViewModel() {
		}

		public async Task LoadFiles() {
			var files = await KnownFolders.PicturesLibrary.GetFilesAsync();
			foreach (var file in files) {
				var stream = await file.OpenReadAsync();
				var bi = new BitmapImage();
				await bi.SetSourceAsync(stream);
				this.Files.Add(bi);
			}
		}
	}
}
