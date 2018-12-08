using System;
using System.Windows;

namespace AlTilde {
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
			InitTray();
		}

		private void Window_Loaded(Object sender, RoutedEventArgs e) {
			this.Hide();
			GlobalHotKey.registerHotKey(this);
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if (TrayNotify != null) {
				TrayNotify.Dispose();
			}
		}
	}
}
