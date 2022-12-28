using System.Drawing;

public static class ImageAnalyzer {
    
    /// <summary>
    /// The main part of the image analysis logic. Here is what it does:
    /// 1) Iterates over each pixel in the input image and gets the pixel's color
    /// 2) Uses "CoinKinds" collection to match the pixel's color and color of specific coin type
    /// 3) Depending on the matched coin type, increments its TotalArea by 1 pixel and writes that pixel
    /// to the output image. The output image is used to visually represent filtering results. 
    /// If the output image does not look like nice solid circles, matching sensivity for each coin type
    /// can be tweaked using ColorMatchTolerance parameter in the configuration json file.
    /// </summary>
    /// <param name="inputFilePath"></param>
    /// <param name="outputFilePath"></param>
    /// <param name="CoinKinds"></param>
    public static void AnalyzeImage(string inputFilePath, string outputFilePath, List<Coin> CoinKinds)
    {

        Bitmap imgSource = new Bitmap(inputFilePath);
        Bitmap imgTarget = new Bitmap(inputFilePath);
        for (int i = 0; i <= imgSource.Width - 1; i++)
            for (int j = 0; j <= imgSource.Height - 1; j++)
                for (int k = 0; k < CoinKinds.Count(); k++)
                {
                    Coin coin = CoinKinds[k];
                    coin.DisplayColor = presetOutputColors[k % presetOutputColors.Length];
                    if (VerifyColor(imgSource.GetPixel(i, j), coin.ColorConstraints, coin.ColorMatchTolerance))
                    {
                        imgTarget.SetPixel(i, j, coin.DisplayColor);
                        CoinKinds[k].TotalArea += 1;
                        break;
                    }
                }
        imgTarget.Save(outputFilePath);
    }

    private static bool VerifyColor(Color p, ConstraintsSet c, int tolerance) =>
        p.R <= c.rMax + tolerance &&
        p.R >= c.rMin - tolerance &&
        p.G <= c.gMax + tolerance &&
        p.G >= c.gMin - tolerance &&
        p.B <= c.bMax + tolerance &&
        p.B >= c.bMin - tolerance &&
        p.R - p.G <= c.rGMaxDiff + tolerance &&
        p.R - p.G >= c.rGMinDiff - tolerance &&
        p.R - p.B <= c.rBMaxDiff + tolerance &&
        p.R - p.B >= c.rBMinDiff - tolerance &&
        p.G - p.B <= c.gBMaxDiff + tolerance &&
        p.G - p.B >= c.gBMinDiff - tolerance;

    private static bool VerifyColor(Color p, List<ConstraintsSet> c, int tolerance) => c.Any(n => VerifyColor(p, n, tolerance));


    // Hardcoded output colors for output image generation. If there are more than 14 coin types, same color may be used for two different coin types.
    private static Color[] presetOutputColors = new Color[] {
        Color.DeepPink,
        Color.DeepSkyBlue,
        Color.Aquamarine,
        Color.BlueViolet,
        Color.DarkGoldenrod,
        Color.DarkOrange,
        Color.Red,
        Color.Blue,
        Color.Yellow,
        Color.Green,
        Color.Indigo,
        Color.Orange,
        Color.Orchid,
        Color.Brown,
        Color.Pink
        };
}
 