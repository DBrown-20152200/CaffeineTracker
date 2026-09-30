using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CaffeineTracker;

public partial class DrinkListPage : ContentPage
{
    public class Drinks
    {
        public string Description { get; set; }
        public int CaffeineContent { get; set; }

        public Drinks(string Description, int CaffeineContent)
        {
            this.Description = Description;
            this.CaffeineContent = CaffeineContent;
        }
    }

    public static ObservableCollection<Drinks> drinksCollection = new ObservableCollection<Drinks>();

    public DrinkListPage()
	{
		InitializeComponent();
        DrinksList.ItemsSource = drinksCollection;
    }

    private void AddDrinkButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(DrinkEntry.Text) != true && String.IsNullOrEmpty(CaffeineEntry.Text) != true)
        {
            Drinks newDrink = new Drinks(DrinkEntry.Text, (int.Parse(CaffeineEntry.Text)));
            if (drinksCollection.Contains(newDrink) == false)
            {
                drinksCollection.Add(newDrink);
            }
        }
    }
    private void DrinksList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Drinks itemTapped = (Drinks) e.Item;
        drinksCollection.Remove(itemTapped);
    }
}