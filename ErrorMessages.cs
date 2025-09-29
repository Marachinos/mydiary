using System;

namespace mydiary
{
    public static class ErrorMessages //ErrorMessages Class for displaying error messages
    {
        public static void ShowInvalidChoice()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ogiltigt val. Ange 1-6 och försök igen.");
            Console.ResetColor();
        }

        public static void ShowInvalidDateFormat()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ogiltigt datumformat. Använd ÅÅÅÅ-MM-DD");
            Console.ResetColor();
        }

        public static void ShowFileNotFound(string filePath)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Filen {filePath} finns inte.");
            Console.ResetColor();
        }

        public static void ShowFileSaveError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Fel vid sparande av fil: {message}");
            Console.ResetColor();
        }

        public static void ShowFileLoadError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Fel vid läsning från fil: {message}");
            Console.ResetColor();
        }

        public static void ShowNoEntriesFound(DateTime date)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ingen anteckning hittades för {date:yyyy-MM-dd}.");
            Console.ResetColor();
        }

        public static void ShowNoEntriesToList()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Inga dagboksanteckningar att visa.");
            Console.ResetColor();
        }
    }
}