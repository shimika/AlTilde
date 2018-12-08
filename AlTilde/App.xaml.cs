using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Shell;

namespace AlTilde {
	/// <summary>
	/// App.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class App : Application, ISingleInstanceApp {

		[STAThread]
		public static void Main() {
			if (SingleInstance<App>.InitializeAsFirstInstance("AlTilde")) {
				App application = new App();

				application.Init();
				application.Run();

				// Allow single instance code to perform cleanup operations
				SingleInstance<App>.Cleanup();
			}
		}

		public void Init() {
			this.InitializeComponent();
		}

		public bool SignalExternalCommandLineArgs(IList<string> args) {
			return false;
		}
	}
}
