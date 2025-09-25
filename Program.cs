using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;

namespace mydiary
{
    public class diaryEntry //Diary Entry Class
    { 
    public DateTime date { get; set; }
        public string name { get; set; }
    }
    class Program
    {
        static List<diaryEntry> entries = new List<diaryEntry>();
        static Dictionary<DateTime, List<diaryEntre>> entriesByDate = new Dictionary<DateTime, List<diaryEntre>>();
        const string filePath = "mydiary.json";
        static void Main() //Hälsningsfras
        {
            Console.WriteLine("Välkommen till min lilla enkla dagbok :)\n");

            LoadFromFile();

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
                        if (Console.ReadLine()?.ToLower() == j)
                        {
                            SaveToFile();
                        }
                        Console.WriteLine("Avslutar programmet.");
                        return;

                    default:
                        Console.WriteLine("Ogiltigt val, ange 1-6 och försök igen.");
                        break;
                }
                Console.WriteLine();
                }
            }

    }
}
