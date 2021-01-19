using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using System.Collections.Generic;

namespace MultiWindowTesting.Droid
{
	[Activity(Label = "MultiWindowTesting", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
	public class MyActivity : FormsActivity
	{

	}

	public abstract class FormsActivity : AndroidX.AppCompat.App.AppCompatActivity
	{
		public Window MainWindow { get; internal set; }

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.formslayout);

			var restoredWindowState = new Dictionary<string, string>();

			foreach (var k in savedInstanceState.KeySet())
			{
				var v = savedInstanceState.GetString(k);
				restoredWindowState[k] = v;
			}
			//global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			Xamarin.Essentials.Platform.Init(this, savedInstanceState);

			// Wire up our forms window
			if (MainWindow == null)
			{
				var windowId = savedInstanceState.GetString(WindowService.WindowIdKey, null);

				// Try and get the managed window
				if (!string.IsNullOrEmpty(windowId))
					MainWindow = MultiWindowTesting.WindowManager.Shared.GetWindow(windowId);

				// If we don't have a window tracking, assume the default one
				if (MainWindow == null)
					MainWindow = MultiWindowTesting.WindowManager.Shared.GetMainWindow();
			}

			var f = MainWindow.MainPage.CreateSupportFragment(this);

			SupportFragmentManager.BeginTransaction()
				.Add(Resource.Id.container, f).Commit();

			MainWindow.OnStarting(restoredWindowState);

		}

		protected override void OnSaveInstanceState(Bundle outState)
		{
			var state = new Dictionary<string, string>();

			MainWindow?.OnStopping(state);

			foreach (var k in state)
				outState.PutString(k.Key, k.Value);

			base.OnSaveInstanceState(outState);
		}
	}
}