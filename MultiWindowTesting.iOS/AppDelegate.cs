using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace MultiWindowTesting.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
				return true;

			LoadApplication(new App());
			return base.FinishedLaunching(app, options);
		}

		public override UISceneConfiguration GetConfiguration(UIApplication application, UISceneSession connectingSceneSession, UISceneConnectionOptions options)
			=> new UISceneConfiguration("Default Configuration", connectingSceneSession.Role);
	}
}
