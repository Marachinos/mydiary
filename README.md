# mydiary 
En enkel dagboksapp
MyDiary är en enkel konsolapplikation i C# där användaren kan skriva, spara och läsa dagboksanteckningar. 
Anteckningar kan listas, sökas på datum och sparas i en JSON-fil (`mydiary.json`). 
Applikationen använder en meny där användaren kan välja olika funktioner, som att lägga till nya anteckningar eller läsa gamla.

# Funktioner
1. Användaren gör ett val i menyn
2. Väljer användaren att Skriva ny anteckning så skriver man datum och sen skriver en anteckning
3. Anteckningen sparas, via menyn eller med en fråga på slutet, i en gemensam .jsonfil
4. Användaren kan välja att visa alla sina anteckningar i appen
5. Användaren kan söka efter sina anteckningar via datum
6. När man vill avsluta appen får man en fråga om man vill spara anteckningen man gjort, oavsett om man sparat den via det enskilda valet eller inte.



# Kort reflektion  
I projektet används en **`List<diaryEntry>`** för att lagra alla dagboksanteckningar och
en **`Dictionary<DateTime, List<diaryEntry>>`** för att snabbt kunna söka efter anteckningar baserat på datum. 
Detta gör sökningen effektiv och enkel att implementera.  

Som I/O-format valdes **JSON** eftersom det är lättläst. 
Lätt att serialisera i C# och fungerar bra för att spara strukturerad data som listor av objekt. 

//Fick lära mig att serialisera är viktigt för att kunna spara och läsa data på ett strukturerat sätt.\\
//Och att deserialisering är lika viktigt för att kunna återställa data från det sparade formatet.\\

Felhantering sker främst med `try-catch`-block vid filinläsning och -skrivning för att undvika att programmet 
kraschar vid fel, t.ex. om filen inte finns eller är korrupt. Dessutom används `DateTime.TryParseExact` 
för att hantera felaktiga datumformat på ett säkert sätt.  

Koden är tänkt att vara robust nog för nybörjare men ändå enkel att förstå och vidareutveckla.

La till menyvalen Delete och Edit för att kunna ta bort och redigera anteckningar.

Projektet är byggt med .NET 8.0 och C# 12.0.

