using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MultiWindowTesting
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}

		static int stateCounter = 0;
		public static int StateCounter
			=> ++stateCounter;

		public static void NewWindow(Window windowInstance)
		{
			var s = DependencyService.Get<IWindowService>();

			s.NewWindow(windowInstance);
		}
	}
}
