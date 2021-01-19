using System.Collections.Generic;
using System.Windows.Input;

namespace MultiWindowTesting
{
	public class MyMainWindow : Window
	{
		public MyMainWindow()
		{
			myMainPage = new MyMainPage();

			MainPage = myMainPage;
		}

		MyMainPage myMainPage;

		public override void OnStarting(IReadOnlyDictionary<string, string> restoredState)
			=> myMainPage?.OnStarting(restoredState);

		public override void OnStopping(IDictionary<string, string> savingState)
			=> myMainPage?.OnStopping(savingState);
	}
}
