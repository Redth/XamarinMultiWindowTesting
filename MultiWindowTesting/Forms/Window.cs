using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MultiWindowTesting
{
	public class Window : IDisposable
	{
		public Window()
		{
		}

		public Window(ContentPage rootPage)
		{
			MainPage = rootPage;
		}

		public string Id { get; internal set; }

		public ContentPage MainPage { get; internal set; }

		public virtual void OnStopping(IDictionary<string, string> savingState)
		{ }

		public virtual void OnStarting(IReadOnlyDictionary<string, string> restoredState)
		{ }

		public void Dispose()
		{
			MainPage = null;
		}
	}
}
