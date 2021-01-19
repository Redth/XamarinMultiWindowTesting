using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace MultiWindowTesting.iOS
{
	public abstract class FormsSceneDelegate : UIWindowSceneDelegate
	{
		public string Uid = Guid.NewGuid().ToString().Substring(0, 6);

		public override UIWindow Window { get; set; }

		public Window MainWindow { get; internal set; }

		UIViewController rootViewController;

		public override void WillConnect(UIScene scene, UISceneSession session, UISceneConnectionOptions connectionOptions)
		{
			var restoredWindowState = new Dictionary<string, string>();

			var restoreUserActivity = session?.StateRestorationActivity;

			if (restoreUserActivity?.UserInfo != null)
			{
				Console.WriteLine($"Restoring StateRestorationActivity (type: {restoreUserActivity.ActivityType})...");

				foreach (var k in restoreUserActivity.UserInfo.Keys)
				{
					var key = k.ToString();
					var val = restoreUserActivity?.UserInfo?[k]?.ToString();

					Console.WriteLine($"   -> {key}={val}");

					restoredWindowState[key] = val;
				}
			}
			else
				Console.WriteLine($"StateRestorationActivity null, nothing to restore...");


			if (App.Current == null)
				App.SetCurrentApplication(new App());

			var windowScene = scene as UIWindowScene;

			// Wire up our forms window
			if (MainWindow == null)
			{
				var windowId =
					connectionOptions?.UserActivities?.AnyObject?.UserInfo
						?.ValueForKey(new NSString(WindowService.WindowIdKey))?.ToString();

				// Try and get the managed window
				if (!string.IsNullOrEmpty(windowId))
					MainWindow = WindowManager.Shared.GetWindow(windowId);

				// If we don't have a window tracking, assume the default one
				if (MainWindow == null)
					MainWindow = WindowManager.Shared.GetMainWindow();
			}

			MainWindow.OnStarting(restoredWindowState);

			if (Window == null)
			{
				Console.WriteLine($"{MainWindow.Id}: UIWindow null, creating...");
				Window = new UIWindow(windowScene);
			}

			if (rootViewController == null)
			{
				Console.WriteLine($"{MainWindow.Id}: UIViewController Root null, creating...");
				rootViewController = MainWindow.MainPage.CreateViewController();
			}

			if (Window.RootViewController == null)
			{
				Console.WriteLine($"{MainWindow.Id}: UIWindow's RootViewController null, assigning...");
				Window.RootViewController = rootViewController;
			}

			
			Window.MakeKeyAndVisible();
		}

		public override NSUserActivity GetStateRestorationActivity(UIScene scene)
		{
			if (MainWindow == null)
			{
				Console.WriteLine("MainWindow null, no state to save...");
				return null;
			}

			var userActivity = new NSUserActivity(MainWindow.GetType().FullName);

			var d = new Dictionary<string, string>();
			var nsd = new NSMutableDictionary();

			MainWindow.OnStopping(d);

			Console.WriteLine($"{MainWindow.Id}: Saving StateRestorationActivity (type: {userActivity.ActivityType})...");

			foreach (var k in d)
			{
				Console.WriteLine($"{MainWindow.Id}:   -> {k.Key}={k.Value}");
				nsd.SetValueForKey(new NSString(k.Value), new NSString(k.Key));
			}

			userActivity.AddUserInfoEntries(nsd);

			MainWindow.Dispose();
			MainWindow = null;

			return userActivity;
		}
	}
}
