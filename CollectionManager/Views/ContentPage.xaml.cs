using CollectionManager.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CollectionManager.Views {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class ContentPage : Page {
		internal MainPageViewModel ViewModel {
			get;
		} = new MainPageViewModel();

		public ContentPage() {
			this.InitializeComponent();
		}

		private async void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {

			await this.ViewModel.LoadFiles();
		}
	}
}
