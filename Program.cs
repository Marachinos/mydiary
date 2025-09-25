using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;

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
        }
    }
}
