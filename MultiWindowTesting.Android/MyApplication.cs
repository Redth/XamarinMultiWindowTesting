using System;
using Android.App;

namespace MultiWindowTesting.Droid
{
	[Application]
	public class MyApplication : Android.App.Application
	{
		public MyApplication()
		{
		}

		public override void OnCreate()
		{
			base.OnCreate();

			//global::Xamarin.Forms.Forms.Init(this.ApplicationContext, new Android.OS.Bundle());

			App.Current = new App();
		}
	}
}
