using Newtonsoft.Json;
using System.Drawing;



List<Coin> coinSamples;

using (StreamReader reader = new StreamReader(args[0]))
{
    string json = reader.ReadToEnd();
    coinSamples = JsonConvert.DeserializeObject<List<Coin>>(json);
}


var presetColors = new Color[] { Color.DeepPink, Color.DeepSkyBlue, Color.Aquamarine, Color.BlueViolet, Color.DarkGoldenrod, Color.DarkOrange, Color.Red, Color.Blue, Color.Yellow, Color.Green, Color.Indigo, Color.Orange, Color.Orchid, Color.Brown, Color.Pink };

for (int i = 0; i < coinSamples.Count(); i++)
{
    var coin = coinSamples[i];
    coin.DisplayColor = presetColors[i % presetColors.Length];
    Console.WriteLine("Aznalyzing colors and size for coin type: " + coin.CoinName);
    var coinAreas = new List<double>();
    foreach (var j in coin.CoinSampleImgs)
    {
        coin.ColorConstraints.Add(Sampler.CreateSampleOneCoin(j));
        coinAreas.Add(Sampler.GetArea(j));
    }
    coin.Area = coinAreas.Average();
}






void WriteMaskToFile(string inputFilePath, string outputFilePath, List<Coin> CoinKinds)
{

    Bitmap imgSource = new Bitmap(inputFilePath);
    Bitmap imgTarget = new Bitmap(inputFilePath);
    for (int I = 0; I <= imgSource.Width - 1; I++)
        for (int J = 0; J <= imgSource.Height - 1; J++)
            foreach (var coin in CoinKinds)
            {
                if (PixelVerifier.VerifyColor(imgSource.GetPixel(I, J), coin.ColorConstraints, coin.ColorMatchTolerance))
                {
                    imgTarget.SetPixel(I, J, coin.DisplayColor);
                    coin.TotalArea += 1;
                    break;
                }
            }
    imgTarget.Save(outputFilePath);
}

WriteMaskToFile(args[1], args[2], coinSamples);

foreach(Coin coin in coinSamples)
    Console.WriteLine(coin);










public struct ConstraintsSet
{
    public int rMax, rMin, gMax, gMin, bMax, bMin, rGMaxDiff, rGMinDiff, rBMaxDiff, rBMinDiff, gBMaxDiff, gBMinDiff;
    public ConstraintsSet(int RMax, int RMin, int GMax, int GMin, int BMax, int BMin, 
        int RGMaxDiff, int RGMinDiff, int RBMaxDiff, int RBMinDiff, int GBMaxDiff, int GBMinDiff) =>
        (rMax, rMin, gMax, gMin, bMax, bMin, rGMaxDiff, rGMinDiff, rBMaxDiff, rBMinDiff, gBMaxDiff, gBMinDiff) = 
        (RMax, RMin, GMax, GMin, BMax, BMin, RGMaxDiff, RGMinDiff, RBMaxDiff, RBMinDiff, GBMaxDiff, GBMinDiff);

    public override string ToString() => "rmax\trmin\tgmax\tgmin\tbmax\tbmin\tr-g_max\tr-g_min\tr-b_max\tr-b_min\tg-b_max\tg-b_min\n" +
            rMax + "\t" + rMin + "\t" + gMax + "\t" + gMin + "\t" + bMax + "\t" + bMin + "\t" + rGMaxDiff + "\t" + rGMinDiff + "\t" + rBMaxDiff + "\t" + rBMinDiff + "\t" + gBMaxDiff + "\t" + gBMinDiff;
}
 