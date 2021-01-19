using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MultiWindowTesting
{
	public partial class MyMainPage : ContentPage
	{
		public MyMainPage()
		{
			InitializeComponent();

			viewModel = new MyMainViewModel();
			BindingContext = viewModel;
		}

		MyMainViewModel viewModel;

		public void OnStarting(IReadOnlyDictionary<string, string> restoredState)
			=> viewModel?.OnStarting(restoredState);

		public void OnStopping(IDictionary<string, string> savingState)
			=> viewModel?.OnStopping(savingState);
	}

}
