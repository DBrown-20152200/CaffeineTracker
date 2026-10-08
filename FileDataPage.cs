using Newtonsoft.Json;
using System.Diagnostics;
using System.Collections.ObjectModel;
using static CaffeineTracker.DrinkListPage;

namespace CaffeineTracker;

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