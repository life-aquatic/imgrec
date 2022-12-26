using System.Drawing;

public class Coin
{
    public string CoinName;
    public int CoinValue;
    public List<string> CoinSampleImgs;
    public List<ConstraintsSet> ColorConstraints = new List<ConstraintsSet>();
    public double Area;
    public int ColorMatchTolerance;
    public Color DisplayColor;
    public int TotalArea = 0;

    public override string ToString()
    {
        return $"{CoinName}: {TotalArea / Area}";
    }
}
 