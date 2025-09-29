# mydiary 
En enkel dagboksapp
MyDiary �r en enkel konsolapplikation i C# d�r anv�ndaren kan skriva, spara och l�sa dagboksanteckningar. 
Anteckningar kan listas, s�kas p� datum och sparas i en JSON-fil (`mydiary.json`). 
Applikationen anv�nder en meny d�r anv�ndaren kan v�lja olika funktioner, som att l�gga till nya anteckningar eller l�sa gamla.

# Funktioner
1. Anv�ndaren g�r ett val i menyn
2. V�ljer anv�ndaren att Skriva ny anteckning s� skriver man datum och sen skriver en anteckning
3. Anteckningen sparas, via menyn eller med en fr�ga p� slutet, i en gemensam .jsonfil
4. Anv�ndaren kan v�lja att visa alla sina anteckningar i appen
5. Anv�ndaren kan s�ka efter sina anteckningar via datum
6. N�r man vill avsluta appen f�r man en fr�ga om man vill spara anteckningen man gjort, oavsett om man sparat den via det enskilda valet eller inte.



# Kort reflektion  
I projektet anv�nds en **`List<diaryEntry>`** f�r att lagra alla dagboksanteckningar och
en **`Dictionary<DateTime, List<diaryEntry>>`** f�r att snabbt kunna s�ka efter anteckningar baserat p� datum. 
Detta g�r s�kningen effektiv och enkel att implementera.  

Som I/O-format valdes **JSON** eftersom det �r l�ttl�st. 
L�tt att serialisera i C# och fungerar bra f�r att spara strukturerad data som listor av objekt. 

//Fick l�ra mig att serialisera �r viktigt f�r att kunna spara och l�sa data p� ett strukturerat s�tt.\\
//Och att deserialisering �r lika viktigt f�r att kunna �terst�lla data fr�n det sparade formatet.\\

Felhantering sker fr�mst med `try-catch`-block vid filinl�sning och -skrivning f�r att undvika att programmet 
kraschar vid fel, t.ex. om filen inte finns eller �r korrupt. Dessutom anv�nds `DateTime.TryParseExact` 
f�r att hantera felaktiga datumformat p� ett s�kert s�tt.  

Koden �r t�nkt att vara robust nog f�r nyb�rjare men �nd� enkel att f�rst� och vidareutveckla.

La till menyvalen Delete och Edit f�r att kunna ta bort och redigera anteckningar.

Projektet �r byggt med .NET 8.0 och C# 12.0.

