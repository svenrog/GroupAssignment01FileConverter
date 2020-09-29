# Filkonverteraren (Gruppuppgift)

Andra inlämningsuppgiften för kursen Programmeringsteknik C#

## Inlämning

Inlämning av denna uppgift får ej ske via pull request. Lösningens innehåll skall packas i en .zip-fil och skickas in via PingPong [här]().

## Bakgrund

Företaget EBOARDSTORE behöver hjälp med mjukvara som kan konvertera butikens inventarie till olika format som skall skickas till olika parter.

## Kravspecifikation

Mjukvaran skall ta emot två parametrar:

* Källfil
* Målfil

Målfilen skall skapas som ett resultat av konvertering av indatat.

Följande indata- och utdataformat skall stödjas:

* Xml (.xml)
* JSON (.json)
* Binärdata (.bin)
* CSV (Excel) (.csv)

Läsning av andra filformat skall generera felmeddelanden.
Skrivning till andra filformat skall generera felmeddelanden.

_Alla medföljande tester måste testa grönt_
_Vissa tester använder sig av djupjämförelser för att spåra att all data är identisk, var noga med att datat konverteras förlustfritt_

### Ytterligare

Projektet är redan uppsatt med NuGet-paketet [CommandLineParser](https://github.com/commandlineparser/commandline) som gör att ni kan köra ert program med inparametrar.

För att färdigställa denna uppgift behöver man implementera metoden `FileConverterProgram.Run`.
Till er hjälp har ni den statiska klassen `ConverterRegistrationScanner` som har två egenskaper, `SupportedWriters` och `SupportedReaders`.

Dessa populeras vid körning av programmet och hittar automatiskt klasser som implementerar `IFileWriter` respektive `IFileReader`.
Man kan således hämta ut en `IFileWriter` från `ConverterRegistrationScanner.SupportedWriters[".xml"]`.
Vidare behöver man bistå med funktionaliteten för alla `IFileWriter` och `IFileReader` som krävs för att lösa uppgiften.