namespace Slides.Types
{
    public class RGBAColor
    {
        public readonly double R;
        public readonly double G;
        public readonly double B;
        public readonly double A;

        public RGBAColor( double r, double g, double b, double a )
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public override bool Equals( object? obj )
        {
            return obj is RGBAColor color &&
                     R == color.R &&
                     G == color.G &&
                     B == color.B &&
                     A == color.A;
        }

        public override string ToString()
        {
            return $"rgba({R}, {G}, {B}, {A})";
        }

        public override int GetHashCode()
        {
            return R.GetHashCode() ^ G.GetHashCode() ^ B.GetHashCode() ^ A.GetHashCode();
        }
    }
}
