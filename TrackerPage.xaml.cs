using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using static CaffeineTracker.DrinkListPage;

namespace CaffeineTracker;

public partial class TrackerPage : ContentPage
{
	public static int totalCaffeineContent = 0;
    public static DateOnly currentDate = DateOnly.Parse(DateTime.Today.ToString("yyyy-MM-dd"));
    public static List<int> caffeineIntakeList = new List<int>();

    public static void UpdateTotalCaffeine()
    {
        totalCaffeineContent = caffeineIntakeList.Sum();

        foreach (DailyCaffeine Entry in dailyCaffeineIntake)
        {
            if (Entry.DateConsumed == currentDate)
            {
                Entry.CaffeineConsumed = totalCaffeineContent;
            }
        }
    }

    public TrackerPage()
	{        
        try
        {
            drinksCollection = FileData.LoadDrinks(drinksCollection);
            dailyCaffeineIntake = FileData.LoadHistory(dailyCaffeineIntake);
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Exception caught: {e}");

            drinksCollection.Add(new Drinks("Coke (375mL)", 35));
            drinksCollection.Add(new Drinks("Espresso", 63));

            FileData.SaveDrinks(drinksCollection);
            FileData.SaveHistory(dailyCaffeineIntake);
        }
        finally
        {
            drinksCollection = FileData.LoadDrinks(drinksCollection);
            dailyCaffeineIntake = FileData.LoadHistory(dailyCaffeineIntake);
        }

        InitializeComponent();
		TrackerList.ItemsSource = drinksCollection;

        if (dailyCaffeineIntake.Count == 0)
        {
            dailyCaffeineIntake.Add(new DailyCaffeine(currentDate, totalCaffeineContent));
        }
        else
        {
            for (int i = 0; i < dailyCaffeineIntake.Count; i++)
            {
                if (dailyCaffeineIntake[i].DateConsumed == currentDate)
                {
                    totalCaffeineContent = dailyCaffeineIntake[i].CaffeineConsumed;
                    caffeineIntakeList.Add(totalCaffeineContent);
                }
            }
        }

        TotalCaffeine.Text = $"Total Caffeine: {totalCaffeineContent}mg";
    }

    private void TrackerList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		DrinkListPage.Drinks itemTapped = (DrinkListPage.Drinks)e.Item;

        caffeineIntakeList.Add(itemTapped.CaffeineContent);

        UpdateTotalCaffeine();

        TotalCaffeine.Text = $"Total Caffeine: {totalCaffeineContent.ToString()}mg";
        FileData.SaveHistory(dailyCaffeineIntake);
    }
    private void UndoButton_Clicked(object sender, EventArgs e)
    {
        if (caffeineIntakeList.Count > 0)
        {
            caffeineIntakeList.RemoveAt(caffeineIntakeList.Count - 1);
        }
        UpdateTotalCaffeine();
        TotalCaffeine.Text = $"Total Caffeine: {totalCaffeineContent.ToString()}mg";
        FileData.SaveHistory(dailyCaffeineIntake);
    }
}