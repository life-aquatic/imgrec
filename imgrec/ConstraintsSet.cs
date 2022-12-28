/// <summary>
/// This data type defines the colors that we use to match coins, in RGB:
/// for example, rMax is a maximum value for red channel, rGMaxDiff is maximum allowed difference between
/// red and green channels, gBMinDiff is minimum allowed difference between green and blue channels
/// </summary>
public struct ConstraintsSet
{
    public int rMax, rMin, gMax, gMin, bMax, bMin, rGMaxDiff, rGMinDiff, rBMaxDiff, rBMinDiff, gBMaxDiff, gBMinDiff;
    public ConstraintsSet(int RMax, int RMin, int GMax, int GMin, int BMax, int BMin, 
        int RGMaxDiff, int RGMinDiff, int RBMaxDiff, int RBMinDiff, int GBMaxDiff, int GBMinDiff) =>
        (rMax, rMin, gMax, gMin, bMax, bMin, rGMaxDiff, rGMinDiff, rBMaxDiff, rBMinDiff, gBMaxDiff, gBMinDiff) = 
        (RMax, RMin, GMax, GMin, BMax, BMin, RGMaxDiff, RGMinDiff, RBMaxDiff, RBMinDiff, GBMaxDiff, GBMinDiff);
}
 