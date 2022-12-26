using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;







Bitmap imgbla = new Bitmap(@"c:\temp\cbla.png");

//ConstraintsSet BlackConstraints1 = new ConstraintsSet (109, 0,  125, 0,  148, 0,      4,  -28, 3,  -56, 8,  -31);
//ConstraintsSet FiverConstraints1 = new ConstraintsSet (255, 47, 255, 28, 255, 21,    30, -2,  46, -19, 19, -21);
//ConstraintsSet DimeConstraints2 = new ConstraintsSet  (252, 33, 254, 35, 233, 14,    6, -12, 41, 3, 42, 11);
//ConstraintsSet DimeConstraints1 = new ConstraintsSet  (255, 129,255, 134, 241,102,   0, -15, 35, -9, 38, -4);

//Console.WriteLine(Sampler.CreateSample(@"c:\temp\cbla.png"));
//Console.WriteLine(Sampler.CreateSample(@"c:\temp\c5.png"));
//Console.WriteLine(Sampler.CreateSample(@"c:\temp\c10b.png"));
//Console.WriteLine(Sampler.CreateSample(@"c:\temp\c10a.png"));

ConstraintsSet[] Fivers = new ConstraintsSet[]
{
    Sampler.CreateSample(@"c:\temp\fi\c5a.png"),
    Sampler.CreateSample(@"c:\temp\fi\c5b.png"),
    Sampler.CreateSample(@"c:\temp\fi\c5c.png")
};

ConstraintsSet[] Dimes = new ConstraintsSet[]
{
    Sampler.CreateSample(@"c:\temp\fi\c10a.png"),
    Sampler.CreateSample(@"c:\temp\fi\c10b.png"),
    Sampler.CreateSample(@"c:\temp\fi\c10c.png")
};


//WriteMaskToFile(@"c:\temp\table.png", @"c:\temp\fi\a_fiver.png", Fivers);
//WriteMaskToFile(@"c:\temp\table.png", @"c:\temp\fi\a_dimes.png", Dimes );
Console.WriteLine(CalcSum(@"c:\temp\table.png", Fivers, Dimes));




void WriteMaskToFile(string inputFilePath, string outputFilePath, ConstraintsSet[] constraints)
{
    Bitmap imgSource = new Bitmap(inputFilePath);
    Bitmap imgTarget = new Bitmap(imgSource.Width, imgSource.Height);
    for (int I = 0; I <= imgSource.Width - 1; I++)
        for (int J = 0; J <= imgSource.Height - 1; J++)
            if (PixelVerifier.VerifyColor(imgSource.GetPixel(I, J), constraints))
                imgTarget.SetPixel(I, J, Color.Red);
    imgTarget.Save(outputFilePath);
}

(int, int) CalcSum(string inputFilePath, ConstraintsSet[] fiverConstraints, ConstraintsSet[] dimeConstraints)
{
    int fivers = 0;
    int dimes = 0;
    Bitmap imgSource = new Bitmap(inputFilePath);
    for (int I = 0; I <= imgSource.Width - 1; I++)
        for (int J = 0; J <= imgSource.Height - 1; J++)
            if (PixelVerifier.VerifyColor(imgSource.GetPixel(I, J), fiverConstraints))
                fivers += 1;
            else if (PixelVerifier.VerifyColor(imgSource.GetPixel(I, J), dimeConstraints))
                dimes += 1;
    return (fivers, dimes);
}


//public class Pixel
//{
//    public int x, y, r, g, b, o;
//    public Pixel(int X, int Y, int R, int G, int B, int O) => (x, y, r, g, b, o) = (X, Y, R, G, B, O);
//}

public struct ConstraintsSet
{
    public int rMax, rMin, gMax, gMin, bMax, bMin, rGMaxDiff, rGMinDiff, rBMaxDiff, rBMinDiff, gBMaxDiff, gBMinDiff;
    public ConstraintsSet(int RMax, int RMin, int GMax, int GMin, int BMax, int BMin, 
        int RGMaxDiff, int RGMinDiff, int RBMaxDiff, int RBMinDiff, int GBMaxDiff, int GBMinDiff) =>
        (rMax, rMin, gMax, gMin, bMax, bMin, rGMaxDiff, rGMinDiff, rBMaxDiff, rBMinDiff, gBMaxDiff, gBMinDiff) = 
        (RMax, RMin, GMax, GMin, BMax, BMin, RGMaxDiff, RGMinDiff, RBMaxDiff, RBMinDiff, GBMaxDiff, GBMinDiff);

    public override string ToString() => "rmax\trmin\tgmax\tgmin\tbmax\tbmin\tr-g_max\tr-g_min\tr-b_max\tr-b_min\tg-b_max\tg-b_min\n" +
            rMax + "\t" +
            rMin + "\t" +
            gMax + "\t" +
            gMin + "\t" +
            bMax + "\t" +
            bMin + "\t" +
            rGMaxDiff + "\t" +
            rGMinDiff + "\t" +
            rBMaxDiff + "\t" +
            rBMinDiff + "\t" +
            gBMaxDiff + "\t" +
            gBMinDiff;
}
 