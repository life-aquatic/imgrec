using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;


// Validating command-line arguments in 3 steps:
// 1) Trying to deserialize argument 0 into a List<Coin>
// 2) Trying to open argument 1 as a Bitmap
// 3) Trying to save a dummy 1x1 bitmap into the path provided by argument 2
try
{
    JsonConvert.DeserializeObject<List<Coin>>(File.ReadAllText(args[0]));
    new Bitmap(args[1]);
    new Bitmap(1, 1).Save(args[2]);
}
catch (Exception _)
{
    Console.WriteLine("Virheelliset parametrit. \n" +
        @"Esimerkki käytöstä: imgrec.exe c:\temp\config.json c:\temp\inputimage.png c:\temp\outputimage.png");
    Environment.Exit(1);
}

// Using the provided json file and a set of sample coin images to create a data set of coin types, colors, values and sizes
List<Coin> coinSamples = JsonConvert.DeserializeObject<List<Coin>>(File.ReadAllText(args[0]));


Console.WriteLine("Kuvaa analysoidaan, odota hetki.");

// Filtering the input image, saving the filtered result into the output image, counting total area (in pixels) for each
// coin type, and writing it into coinSamples collection, as a property for each coin type.
ImageAnalyzer.AnalyzeImage(args[1], args[2], coinSamples);

Console.WriteLine($"Analyysin tulokset:\n" +
    $"{String.Join('\n', coinSamples)}\n" +
    $"Yhteisarvo: {coinSamples.Select(n => n.TotalValue).Sum():N2} euroa.\n" + 
    $"Suodatettu kuva on tallennettu {args[2]} -tiedostoon.\n" +
    $"Jos suodatustulos ei näkyy oikealta, säädä \"ColorMatchTolerance\"-parametrin {args[0]} -tiedostossa ja yritä uudelleen.");
 