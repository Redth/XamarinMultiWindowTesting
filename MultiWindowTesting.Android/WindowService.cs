using System;
using Android.App;
using Android.Content;
using MultiWindowTesting.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(WindowService))]

namespace MultiWindowTesting.Droid
{
	public class WindowService : IWindowService
	{
		internal const string WindowIdKey = "xamarin_forms_window_instance_id";

		public WindowService()
		{
		}

		public void NewWindow(Window windowInstance)
		{
			var windowId = WindowManager.Shared
				.ManageWindow(windowInstance);

			var intent = new Intent(Application.Context, typeof(FormsActivity));

			intent.Extras.PutString(WindowService.WindowIdKey, windowId);

			Application.Context.StartActivity(intent);
		}
	}
}
