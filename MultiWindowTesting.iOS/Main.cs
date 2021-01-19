using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace MultiWindowTesting.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main(string[] args)
		{
			// Tell the app how our root window is created
			WindowManager.Shared.SetMainWindowFactory(
				() => new MyMainWindow());

			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main(args, null, "AppDelegate");
		}
	}
}
