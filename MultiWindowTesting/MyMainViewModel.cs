using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MultiWindowTesting
{
	public class MyMainViewModel : INotifyPropertyChanged
	{
		public void OnStarting(IReadOnlyDictionary<string, string> restoredState)
		{
			if (restoredState.TryGetValue("counter_state", out var s)
				&& int.TryParse(s, out var i))
				State = i;
			else
				State = App.StateCounter;

			if (restoredState.TryGetValue("list_items", out var itemstr))
			{
				foreach (var item in itemstr.Split(';'))
					Items.Add(item);
			}
		}

		public void OnStopping(IDictionary<string, string> savingState)
		{
			savingState["counter_state"] = State.ToString();

			if (Items.Count > 0)
				savingState["list_items"] = string.Join(";", Items);
		}

		int state = 0;
		public int State
		{
			get => state;
			set
			{
				state = value;
				NotifyPropertyChanged(nameof(State));
			}
		}

		string itemText;
		public string ItemText
		{
			get => itemText;
			set
			{
				itemText = value;
				NotifyPropertyChanged(nameof(ItemText));
			}
		}

		public ObservableCollection<string> Items { get; set; } = new ObservableCollection<string>();

		public ICommand NewWindowCommand
			=> new Command(() => App.NewWindow(new MyMainWindow()));

		public ICommand AddItemCommand
			=> new Command(() =>
			{
				if (!string.IsNullOrEmpty(ItemText))
				{
					Items.Add(ItemText);
					ItemText = string.Empty;
				}
			});

		void NotifyPropertyChanged(string propertyName)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		public event PropertyChangedEventHandler PropertyChanged;
	}

}
