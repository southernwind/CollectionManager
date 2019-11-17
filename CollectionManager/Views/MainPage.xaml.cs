using CollectionManager.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CollectionManager.Views {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage {
		internal MainPageViewModel ViewModel {
			get;
		} = new MainPageViewModel();

		public MainPage() {
			this.InitializeComponent();
		}

		private async void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {

			await this.ViewModel.LoadFiles();
		}
	}
}
