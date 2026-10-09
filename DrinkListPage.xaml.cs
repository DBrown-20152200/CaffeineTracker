using System.Collections.ObjectModel;

namespace CaffeineTracker;

public partial class DrinkListPage : ContentPage
{
    public class Drinks
    {
        public string Description { get; set; }
        public float CaffeineContent { get; set; }
        public Drinks(string Description, float CaffeineContent)
        {
            this.Description = Description;
            this.CaffeineContent = CaffeineContent;
        }
    }
    public class DailyCaffeine
    {
        public DateOnly DateConsumed { get; set; }
        public float CaffeineConsumed { get; set; }

        public DailyCaffeine(DateOnly DateConsumed, float CaffeineConsumed)
        {
            this.DateConsumed = DateConsumed;
            this.CaffeineConsumed = CaffeineConsumed;
        }
    }

    public static ObservableCollection<Drinks> drinksCollection = new ObservableCollection<Drinks>();
    public static ObservableCollection<DailyCaffeine> dailyCaffeineIntake =
    new ObservableCollection<DailyCaffeine>();

    public DrinkListPage()
	{
		InitializeComponent();
        DrinksList.ItemsSource = drinksCollection;
    }



    private void AddDrinkButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(DrinkEntry.Text) != true && String.IsNullOrEmpty(CaffeineEntry.Text) != true)
        {
            bool drinkAlreadyAdded = false;

            Drinks newDrink = new Drinks(DrinkEntry.Text, (float.Parse(CaffeineEntry.Text)));

            foreach(Drinks drink in drinksCollection)
            {
                if(drink.Description == newDrink.Description)
                {
                    drinkAlreadyAdded = true;
                }
            }

            if (drinkAlreadyAdded == false)
            {
                drinksCollection.Add(newDrink);
                FileData.SaveDrinks(drinksCollection);
            }
        }
    }
    private void DrinksList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Drinks itemTapped = (Drinks) e.Item;
        drinksCollection.Remove(itemTapped);
        FileData.SaveDrinks(drinksCollection);
    }
}