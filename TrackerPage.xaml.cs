using System.Collections.ObjectModel;

namespace CaffeineTracker;

public partial class TrackerPage : ContentPage
{
    public class DailyCaffeine
    {
        public DateOnly DateConsumed {  get; set; }
        public int CaffeineConsumed {  get; set; }

        public DailyCaffeine(DateOnly DateConsumed, int CaffeineConsumed)
        {
            this.DateConsumed = DateConsumed;
            this.CaffeineConsumed = CaffeineConsumed;
        }
    }

    public static ObservableCollection<DailyCaffeine> dailyCaffeineIntake = 
        new ObservableCollection<DailyCaffeine>();

	public static int totalCaffeineContent = 0;
    public DateOnly currentDate = DateOnly.Parse(DateTime.Today.ToShortDateString());
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

        if (dailyCaffeineIntake.Count == 0)
        {
            dailyCaffeineIntake.Add(new DailyCaffeine(currentDate, totalCaffeineContent));
        }
        for (int i = 0; i < dailyCaffeineIntake.Count; i++)
        {
            if (dailyCaffeineIntake[i].DateConsumed == currentDate)
            {
                dailyCaffeineIntake[i] = new DailyCaffeine(currentDate, totalCaffeineContent);
            }
        }
    }

    private void TrackerPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {

    }
}