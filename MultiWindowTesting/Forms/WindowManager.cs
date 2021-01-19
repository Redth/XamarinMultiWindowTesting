using System;
using System.Collections.Generic;

namespace MultiWindowTesting
{
	// This whole type can be internal when multitargeted
	public class WindowManager
	{
		static WindowManager shared;
		public static WindowManager Shared
			=> shared ??= new WindowManager();


		internal Func<Window> MainWindowFactory { get; set; }

		public void SetMainWindowFactory(Func<Window> createMainWindow)
			=> MainWindowFactory = createMainWindow;

		Dictionary<string, Window> windows = new Dictionary<string, Window>();

		public string ManageWindow(Window windowInstance = null)
		{
			if (windowInstance == null)
			{
				if (MainWindowFactory == null)
					throw new Exception("You must set WindowManager.MainWindowFactory in your application startup");

				windowInstance = MainWindowFactory();
			}

			windowInstance.Id = Guid.NewGuid().ToString();
			windows.Add(windowInstance.Id, windowInstance);

			return windowInstance.Id;
		}

		public Window GetMainWindow()
			=> GetWindow(ManageWindow(null));

		public Window GetWindow(string id)
			=> windows?[id];

		public void UnmanageWindow(string id)
		{
			if (windows.ContainsKey(id))
				windows.Remove(id);
		}
	}
}
