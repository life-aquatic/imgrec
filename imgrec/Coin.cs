using Newtonsoft.Json;
using System.Drawing;

/// <summary>
/// Describes each individual coin type.
/// CoinName, CoinValue, ColorMatchTolerance and CoinSampleImgs are populated at the program startup, based on the configuration json file.
/// ColorConstrains and Area are calculated during the object instantiation, based on the coin sample images.
/// DisplayColor and TotalArea are assigned by ImageAnalyzer.AnalyzeImage() method.
/// </summary>
public class Coin
{
    /// <summary>
    /// A list of file names pinting to sample images for each coin type. The more sample images, the more precise it will be matched.
    /// Borders of each coin sample image should be exactly equal to the coin size, because the coin area will be calculated
    /// based on the dimensions of each sample image.
    /// </summary>
    public List<string> CoinSampleImgs;
    public string CoinName;
    public double CoinValue;
    public int ColorMatchTolerance;


    public List<ConstraintsSet> ColorConstraints;
    public double Area;

    public Color DisplayColor;
    public int TotalArea = 0;
    public double TotalValue { get => TotalArea / Area * CoinValue; }

    [JsonConstructor]
    public Coin(string coinName, double coinValue, int colorMatchTolerance, List<string> coinSampleImgs)
    {
        (CoinName, CoinValue, ColorMatchTolerance, CoinSampleImgs) = (coinName, coinValue, colorMatchTolerance, coinSampleImgs);
        ColorConstraints = CoinSampleImgs.Select(n => InputSampleCreator.CreateColorSample(n)).ToList();
        Area = CoinSampleImgs.Select(n => new Bitmap(n))
            .Select(n => (Math.Pow((n.Height + n.Width) / 4, 2) * Math.PI))
            .Average();
    }
    
    public override string ToString() =>  $"{CoinName}: {TotalArea / Area:N} kpl, {TotalValue:n2} euroa.";
}
 