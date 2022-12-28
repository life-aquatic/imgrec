using System.Drawing;

public static class InputSampleCreator{

    
    /// <summary>
    /// Analyzes coins' sample images. Generates "ConstraninsSet" objects, which define the colors  
    /// that are used to match each coin type, based on the sample image's colors.
    /// </summary>
    /// <param name="inputFilePath"></param>
    /// <returns></returns>
    public static ConstraintsSet CreateColorSample(string inputFilePath)
    {
        Bitmap img = new Bitmap(inputFilePath);
        var Image = new List<Color>();

        for (int I = img.Width / 4; I <= (img.Width / 4) * 3; I++)
        {
            for (int J = img.Height / 4; J <= (img.Height / 4) * 3; J++)
                Image.Add(img.GetPixel(I, J));
        }

        return new ConstraintsSet(
            Image.Select(n => n.R).Max(),
            Image.Select(n => n.R).Min(),
            Image.Select(n => n.G).Max(),
            Image.Select(n => n.G).Min(),
            Image.Select(n => n.B).Max(),
            Image.Select(n => n.B).Min(),
            Image.Select(n => n.R - n.G).Max(),
            Image.Select(n => n.R - n.G).Min(),
            Image.Select(n => n.R - n.B).Max(),
            Image.Select(n => n.R - n.B).Min(),
            Image.Select(n => n.G - n.B).Max(),
            Image.Select(n => n.G - n.B).Min());
    }
}
 