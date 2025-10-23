namespace Slides.Types
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point( double x, double y )
        {
            X = x;
            Y = y;
        }

        public string ToStringFormatted()
        {
            return $"X: {X:F3}, Y: {Y:F3}";
        }

        public override string ToString()
        {
            return $"{X} {Y}";
        }

        public override bool Equals( object? obj )
        {
            return obj is Point other && X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( X, Y );
        }
    }
}
