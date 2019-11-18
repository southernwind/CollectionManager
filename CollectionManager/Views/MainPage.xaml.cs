using System;
using CollectionManager.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CollectionManager.Views {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage {

		public MainPage() {
			this.InitializeComponent();
		}

		private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args) {
			Type page;
			if (args.IsSettingsInvoked == true) {
				page = typeof(SettingsPage);
			} else {
				page = typeof(ContentPage);
			}

			if (this.ContentFrame.CurrentSourcePageType == page) {
				return;
			}

			this.ContentFrame.Navigate(page, null, args.RecommendedNavigationTransitionInfo);
		}
	}
}
