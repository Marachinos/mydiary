using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using mydiary;

namespace mydiary
{
    class Program
    {
        static List<DiaryEntry> entries = new List<DiaryEntry>();
        static Dictionary<DateTime, List<DiaryEntry>> entriesByDate = new Dictionary<DateTime, List<DiaryEntry>>();
        const string filePath = "mydiary.json";

        static void Main() 
        {
            Console.ForegroundColor = ConsoleColor.Cyan; //Welcome message to the diary
            Console.WriteLine("Välkommen till min lilla enkla dagbok :)\n");
            Console.ResetColor();

            SilentLoadFromFile();

            while (true)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                HandleMenuChoice(choice);
                Console.WriteLine();
            }
        }

        static void ShowMenu() //Shows the menychoice
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

        static void HandleMenuChoice(string choice) //6 diffrent cases and
                                                    //a Question when you Exit if you want to save before Exit the app.
        {
            switch (choice)
            {
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
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Vill du spara ändringarna innan du avslutar? (j/n)");
                    Console.ResetColor();
                    if (Console.ReadLine()?.ToLower() == "j")
                    {
                        SaveToFile();
                    }
                    Console.WriteLine("Avslutar programmet.");
                    Environment.Exit(0); 
                    break;
                default:
                    ErrorMessages.ShowInvalidChoice();
                    break;
            }
        }

        static void AddEntry() //Add Entry to the diary
        {
            DateTime date = PromtForDate("Ange datum (lämna tomt för dagens datum): ");
            string text;
            do
            {
                Console.Write("Skriv din dagboksanteckning:\n");
                text = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(text));

            var entry = new DiaryEntry { date = date, text = text };
            entries.Add(entry);

            if (!entriesByDate.ContainsKey(date))
            {
                entriesByDate[date] = new List<DiaryEntry>();
            }
            entriesByDate[date].Add(entry);

            Console.WriteLine("Dagboksanteckning tillagd!");
        }

        static DateTime PromtForDate(string promt) //Promt for date
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
                ErrorMessages.ShowInvalidDateFormat();
            }
        }

        static void ListEntries() //List all the entries
        {
            if (entries.Count == 0)
            {
                ErrorMessages.ShowNoEntriesToList();
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
            if (entriesByDate.TryGetValue(date, out List<DiaryEntry> dayEntries))
            {
                Console.WriteLine($"Anteckningar för {date:yyyy-MM-dd}: ");
                foreach (var entry in dayEntries)
                {
                    Console.WriteLine($"- {entry.text}");
                }
            }
            else
            {
                ErrorMessages.ShowNoEntriesFound(date);
            }
        }

        static void SaveToFile() //Save to file
        {
            try
            {
                string json = JsonSerializer.Serialize(entries);
                File.WriteAllText(filePath, json);
                Console.WriteLine($"Dagboksanteckningar sparade till {filePath}.");
            }
            catch (Exception ex)
            {
                ErrorMessages.ShowFileSaveError(ex.Message);
            }
        }

        static void LoadFromFile() //Load from file
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    ErrorMessages.ShowFileNotFound(filePath);
                    return;
                }
                string json = File.ReadAllText(filePath);
                entries = JsonSerializer.Deserialize<List<DiaryEntry>>(json) ?? new List<DiaryEntry>();

                entriesByDate.Clear();
                foreach (var entry in entries)
                {
                    if (!entriesByDate.ContainsKey(entry.date))
                    {
                        entriesByDate[entry.date] = new List<DiaryEntry>();
                    }
                    entriesByDate[entry.date].Add(entry);
                }

                Console.WriteLine($"Dagboksanteckningar lästa från {filePath}");
            }
            catch (Exception ex)
            {
                ErrorMessages.ShowFileLoadError(ex.Message);
            }
        }

        static void SilentLoadFromFile()
        {
            try
            {
                if (!File.Exists(filePath)) return;
                string json = File.ReadAllText(filePath);
                entries = JsonSerializer.Deserialize<List<DiaryEntry>>(json) ?? new List<DiaryEntry>();

                entriesByDate.Clear();
                foreach (var entry in entries)
                {
                    if (!entriesByDate.ContainsKey(entry.date))
                    {
                        entriesByDate[entry.date] = new List<DiaryEntry>();
                    }
                    entriesByDate[entry.date].Add(entry);
                }
            }
            catch { }
        }
    }
}