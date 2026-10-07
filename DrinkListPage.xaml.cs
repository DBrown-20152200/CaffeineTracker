using System.Collections.ObjectModel;
using System.Diagnostics;
using Newtonsoft.Json;

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
    public class DailyCaffeine
    {
        public DateOnly DateConsumed { get; set; }
        public int CaffeineConsumed { get; set; }

        public DailyCaffeine(DateOnly DateConsumed, int CaffeineConsumed)
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

    public static class FileData
    {
        public static void SaveDrinks(ObservableCollection<Drinks> list)
        {
            string fileName = "Drinks.json";

            var localFolder = FileSystem.Current.AppDataDirectory;
            var filePath = Path.Combine(localFolder, fileName);
            Debug.WriteLine(filePath);

            var content_json = JsonConvert.SerializeObject(list);


            File.WriteAllText(filePath, content_json);
        }

        public static ObservableCollection<Drinks> LoadDrinks(ObservableCollection<Drinks> list)
        {
            string fileName = "Drinks.json";
            var localFolder = FileSystem.Current.AppDataDirectory;
            var filePath = Path.Combine(localFolder, fileName);

            Debug.WriteLine(filePath);

            string content_json = File.ReadAllText(filePath);
            list = JsonConvert.DeserializeObject<ObservableCollection<Drinks>>(content_json);

            if (list == null)
            {
                list = new ObservableCollection<Drinks>();
                Debug.WriteLine("New list created");
            }
            return list;
        }
        public static void SaveHistory(ObservableCollection<DailyCaffeine> list)
        {
            string fileName = "DrinksHistory.json";

            var localFolder = FileSystem.Current.AppDataDirectory;
            var filePath = Path.Combine(localFolder, fileName);
            Debug.WriteLine(filePath);

            var content_json = JsonConvert.SerializeObject(list);


            File.WriteAllText(filePath, content_json);
        }
        public static ObservableCollection<DailyCaffeine> LoadHistory(ObservableCollection<DailyCaffeine> list)
        {
            string fileName = "DrinksHistory.json";
            var localFolder = FileSystem.Current.AppDataDirectory;
            var filePath = Path.Combine(localFolder, fileName);

            Debug.WriteLine(filePath);

            string content_json = File.ReadAllText(filePath);
            list = JsonConvert.DeserializeObject<ObservableCollection<DailyCaffeine>>(content_json);

            if (list == null)
            {
                list = new ObservableCollection<DailyCaffeine>();
                Debug.WriteLine("New list created");
            }
            return list;
        }
    }

    private void AddDrinkButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(DrinkEntry.Text) != true && String.IsNullOrEmpty(CaffeineEntry.Text) != true)
        {
            bool drinkAlreadyAdded = false;

            Drinks newDrink = new Drinks(DrinkEntry.Text, (int.Parse(CaffeineEntry.Text)));

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

    private void DrinkListPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {

    }
}