using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Xml.Linq;

namespace mydiary
{
    public class diaryEntry //Diary Entry Class
    { 
    public DateTime date { get; set; }
        public string text { get; set; }
    }
    class Program
    {
        static List<diaryEntry> entries = new List<diaryEntry>();
        static Dictionary<DateTime, List<diaryEntry>> entriesByDate = new Dictionary<DateTime, List<diaryEntry>>();
        const string filePath = "mydiary.json";
        static void Main() //Hälsningsfras
        {
            Console.WriteLine("Välkommen till min lilla enkla dagbok :)\n"); //Byt färg

            SilentLoadFromFile(); //laddar dagboksfilen utan att skriva ut något, loads the diary file without printing anything


            while (true)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                switch (choice)
                {   //6 diffrent cases and a question if you want
                    //to save changes before closing the app
                    case "1":
                        AddEntry();
                        break;
                    case "2":
                        ListEntries();
                        break;
                    case "3":
                        SearchByDate();
                        break;
                    case "4":
                        SaveToFile();
                        break;
                    case "5":
                        LoadFromFile();
                        break;
                    case "6":
                        Console.WriteLine("Vill du spara ändringarna innan du avslutar? (j/n)");
                        if (Console.ReadLine()?.ToLower() == "j")
                        {
                            SaveToFile();
                        }
                        Console.WriteLine("Avslutar programmet.");
                        return;

                    default:
                        Console.WriteLine("Ogiltigt val, ange 1-6 och försök igen."); //byt färg
                        break;
                }
                Console.WriteLine();
            }
        }
        static void ShowMenu() //Menuchoice
        {
            Console.WriteLine("Gör ett val:");
            Console.WriteLine("1. Lägg till en Ny dagboksanteckning");
            Console.WriteLine("2. Lista alla dagboksanteckningar");
            Console.WriteLine("3. Sök en dagboksanteckning via datum");
            Console.WriteLine("4. Spara dagboksanteckning till fil");
            Console.WriteLine("5. Läs dagboksanteckning från fil");
            Console.WriteLine("6. Avsluta");
            Console.Write("Gör ditt val: ");
        }
        static void AddEntry() //Börja skriva i dagboken, Start to write in diary
        {
            DateTime date = PromtForDate("Ange datum: ");
            string text;
            do
            {
                Console.Write("Skriv din dagboksanteckning:\n");
                text = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(text));

            var entry = new diaryEntry { date = date, text = text };
            entries.Add(entry);

            if (!entriesByDate.ContainsKey(date))
            {
                entriesByDate[date] = new List<diaryEntry>();
            }
            entriesByDate[date].Add(entry);

            Console.WriteLine("Dagboksanteckning tillagd!");
        }
        static DateTime PromtForDate(string promt) //promt for date
        {
            while (true)
            {
            Console.Write(promt);
            string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    return DateTime.Today;
                }
                if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    return date;
                }
                Console.WriteLine("Ogiltigt datumformat. Använd ÅÅÅÅ-MM-DD"); //byt färg
            }
        }
        static void ListEntries() //List Entries
        {
            if (entries.Count == 0)
            {
                Console.WriteLine("Inga dagboksanteckningar att visa.");
                return;
            }
            var sortedEntries = entries.OrderBy(e => e.date).ToList();
            foreach (var entry in sortedEntries)
            { 
                Console.WriteLine($"{entry.date:yyyy-MM-dd}: {entry.text}");
            }
        }
        static void SearchByDate() //Search by date
        {
            DateTime date = PromtForDate("Ange datum att söka efter (ÅÅÅÅ-MM-DD): ");
            if (entriesByDate.TryGetValue(date, out List<diaryEntry> dayEntries))
            {
                Console.WriteLine($"Anteckningar för {date:yyyy-MM-dd}: ");
                foreach (var entry in dayEntries)
                {
                    Console.WriteLine($"- {entry.text}");
                }
            }
            else
            {
                Console.WriteLine($"Ingen anteckning hittades för {date:yyyy-MM-dd}."); //Byt färg
            }
        }
        static void SaveToFile() //Save to the file
        {
            try
            {
                string json = JsonSerializer.Serialize(entries);
                File.WriteAllText(filePath, json);
                Console.WriteLine($"Dagboksanteckningar sparade till {filePath}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid sparande av fil: {ex.Message}"); //Byt färg
            }
        }
        static void LoadFromFile() //Load from file
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Filen {filePath} finns inte."); //Byt färg
                    return;
                }
                string json = File.ReadAllText(filePath);
                entries = JsonSerializer.Deserialize<List<diaryEntry>>(json) ?? new List<diaryEntry>();

                entriesByDate.Clear();
                foreach (var entry in entries)
                {
                    if (!entriesByDate.ContainsKey(entry.date))
                    {
                        entriesByDate[entry.date] = new List<diaryEntry>();
                    }
                    entriesByDate[entry.date].Add(entry);
                }

                Console.WriteLine($"Dagboksanteckningar lästa från {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid läsning från fil: {ex.Message}"); //Byt färg
            }
        }
        static void SilentLoadFromFile() //Tyst laddning av filen. Silent load from file
        {
            try
            {
                if (!File.Exists(filePath)) return;
                string json = File.ReadAllText(filePath);
                entries = JsonSerializer.Deserialize<List<diaryEntry>>(json) ?? new List<diaryEntry>();

                entriesByDate.Clear();
                foreach (var entry in entries)
                {
                    if (!entriesByDate.ContainsKey(entry.date))
                    {
                        entriesByDate[entry.date] = new List<diaryEntry>();
                    }
                    entriesByDate[entry.date].Add(entry);
                }
            }
            catch { /* Ignorera fel vid tyst laddning */ }
        }

    }
}
