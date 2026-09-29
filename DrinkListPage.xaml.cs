using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CaffeineTracker;

public partial class DrinkListPage : ContentPage
{
    public class Drinks
    {
        public static string Name { get; set; }
        public static int CaffeineContent { get; set; }

        public Drinks(string Name, int CaffeineContent)
        {
            Drinks.Name = Name;
            Drinks.CaffeineContent = CaffeineContent;
        }
    }
    public static ObservableCollection<Drinks> drinksCollection = new ObservableCollection<Drinks>();

    public DrinkListPage()
	{
		InitializeComponent();
    }

    private void AddDrinkButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(DrinkEntry.Text) != true && String.IsNullOrEmpty(CaffeineEntry.Text) != true)
        {
            drinksCollection.Add(new Drinks(DrinkEntry.Text, int.Parse(CaffeineEntry.Text)));
        }
    }
}