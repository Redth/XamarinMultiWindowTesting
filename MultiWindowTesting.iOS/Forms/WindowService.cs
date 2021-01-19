using System;
using Foundation;
using MultiWindowTesting.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(WindowService))]

namespace MultiWindowTesting.iOS
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

			var userActivity = new NSUserActivity(windowInstance.GetType().FullName);
			userActivity.AddUserInfoEntries(
				NSDictionary.FromObjectAndKey(
					new NSString(windowId),
					new NSString(WindowIdKey)));

			UIApplication.SharedApplication.RequestSceneSessionActivation(
				null,
				userActivity,
				null,
				err =>
				{
					Console.WriteLine(err.Description);
				});
		}
	}
}
