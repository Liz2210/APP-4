using LibraryApp.Data;
using Newtonsoft.Json;
using System.Diagnostics;

namespace LibraryApp.Services;

/// <summary>
/// Data Service Class
/// </summary>
public static class DataService
{
    #region Static Methods

    /// Method used to read json file
    public static Library Read(string jsonPath)
    {
        try
        {
            if (jsonPath is null)
            {
                throw new ArgumentNullException(nameof(jsonPath));
            }
            string jsonSource = File.ReadAllText(jsonPath);
            var jsonContent = JsonConvert.DeserializeObject<Library>(jsonSource);
            if (jsonContent == null)
            {
                throw new ArgumentNullException(nameof(jsonContent));
            }
            return jsonContent;
        }
        catch
        {
            Console.WriteLine("Data reading was not successful.");
            return null;
        }
    }

    /// Methods used to write json file 
    public static void Write(Library library, string jsonPath)
    {
        try
        {
            if (jsonPath is null) throw new ArgumentNullException(nameof(jsonPath));
            File.WriteAllText(jsonPath, JsonConvert.SerializeObject(library));
        }
        catch
        {
            Console.WriteLine("Data saving was not successful.");
        }
    }

    #endregion // Static Methods
}
