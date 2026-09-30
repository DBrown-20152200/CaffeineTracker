using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CaffeineTracker;

public partial class DrinkListPage : ContentPage
{
    public class Drinks
    {
        public string Name { get; set; }
        public string CaffeineContent { get; set; }

        public Drinks(string Name, string CaffeineContent)
        {
            this.Name = Name;
            this.CaffeineContent = CaffeineContent;
        }
    }

    public class DataModel
    {
        public static ObservableCollection<Drinks> drinksCollection = new ObservableCollection<Drinks>();
    }

    public DrinkListPage()
	{
		InitializeComponent();
        DrinksList.ItemsSource = DataModel.drinksCollection;
    }

    private void AddDrinkButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(DrinkEntry.Text) != true && String.IsNullOrEmpty(CaffeineEntry.Text) != true)
        {
            DataModel.drinksCollection.Add(new Drinks(DrinkEntry.Text, (CaffeineEntry.Text + "mg")));
            
        }

        for (int i = 0; i < DataModel.drinksCollection.Count; i++)
        {
            Debug.WriteLine($"{DataModel.drinksCollection[i].Name} " +
                $"{DataModel.drinksCollection[i].CaffeineContent}");
        }
    }
    private void DrinksList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Drinks itemTapped = (Drinks) e.Item;
        DataModel.drinksCollection.Remove(itemTapped);
    }
}