using System.Drawing;

public static class PixelVerifier
{
    public static bool VerifyColor(Color p, ConstraintsSet c) =>
    p.R <= c.rMax &&
    p.R >= c.rMin &&
    p.G <= c.gMax &&
    p.G >= c.gMin &&
    p.B <= c.bMax &&
    p.B >= c.bMin &&
    p.R - p.G <= c.rGMaxDiff &&
    p.R - p.G >= c.rGMinDiff &&
    p.R - p.B <= c.rBMaxDiff &&
    p.R - p.B >= c.rBMinDiff &&
    p.G - p.B <= c.gBMaxDiff &&
    p.G - p.B >= c.gBMinDiff;

    public static bool VerifyColor(Color p, ConstraintsSet[] c) => c.Any(n => VerifyColor(p, n));
}
 