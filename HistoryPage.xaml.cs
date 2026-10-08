using System.Collections.ObjectModel;

namespace CaffeineTracker;

public partial class HistoryPage : ContentPage
{
	public HistoryPage()
	{
		InitializeComponent();
		CaffeineHistoryList.ItemsSource = DrinkListPage.dailyCaffeineIntake;
	}
}