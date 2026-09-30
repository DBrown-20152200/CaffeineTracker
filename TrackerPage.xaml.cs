namespace CaffeineTracker;

public partial class TrackerPage : ContentPage
{
	public int totalCaffeineContent = 0;

	public TrackerPage()
	{
		InitializeComponent();
		TrackerList.ItemsSource = DrinkListPage.drinksCollection;
        TotalCaffeine.Text = $"Total Caffeine: {totalCaffeineContent.ToString()}mg";

    }

    private void TrackerList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		DrinkListPage.Drinks itemTapped = (DrinkListPage.Drinks)e.Item;
		totalCaffeineContent += itemTapped.CaffeineContent;
        TotalCaffeine.Text = $"Total Caffeine: {totalCaffeineContent.ToString()}mg";
    }

    private void TrackerPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {

    }
}