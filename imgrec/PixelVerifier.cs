using System.Drawing;

public static class PixelVerifier
{
    public static bool VerifyColor(Color p, ConstraintsSet c, int tolerance) =>
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

    public static bool VerifyColor(Color p, List<ConstraintsSet> c, int tolerance) => c.Any(n => VerifyColor(p, n, tolerance));
}
 