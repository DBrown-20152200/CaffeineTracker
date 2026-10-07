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
        public DateTime Date { get; set; }

        public Drinks(string Description, int CaffeineContent, DateTime date)
        {
            this.Description = Description;
            this.CaffeineContent = CaffeineContent;
            Date = date;
        }
    }

    public static ObservableCollection<Drinks> drinksCollection = new ObservableCollection<Drinks>();

    public DrinkListPage()
	{
		InitializeComponent();
        DrinksList.ItemsSource = drinksCollection;
    }

    public static class FileData
    {
        public static string fileName = "Drinks.json";
        public static void Save(ObservableCollection<Drinks> list)
        {
            var localFolder = FileSystem.Current.AppDataDirectory;
            var filePath = Path.Combine(localFolder, fileName);
            Debug.WriteLine(filePath);

            var content_json = JsonConvert.SerializeObject(list);


            File.WriteAllText(filePath, content_json);
        }
        public static ObservableCollection<Drinks> Load(ObservableCollection<Drinks> list)
        {
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
    }

    private void AddDrinkButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(DrinkEntry.Text) != true && String.IsNullOrEmpty(CaffeineEntry.Text) != true)
        {
            Drinks newDrink = new Drinks(DrinkEntry.Text, (int.Parse(CaffeineEntry.Text)), DateTime.Today);
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

    private void DrinkListPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {

    }
}