using System.Drawing;

public static class Sampler{



    public static ConstraintsSet CreateSampleOneCoin(string inputFilePath)
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

    public static double GetArea(string filePath)
    {
        Bitmap img = new Bitmap(filePath);
        return Math.Pow((img.Height + img.Width) / 4, 2) * Math.PI; 
    }





    public static string TestSample(string filePath, ConstraintsSet constraints)
    {
        Bitmap img = new Bitmap(filePath);
        var Image = new List<Color>();

        for (int I = 0; I <= img.Width - 1; I++)
        {
            for (int J = 0; J <= img.Height - 1; J++)
                Image.Add(img.GetPixel(I, J));
        }

        return $"File name: {filePath}. Pixels passed: {Image.Where(n => PixelVerifier.VerifyColor(n, constraints, 0))} out of {Image.Count()}";
    }
}
 