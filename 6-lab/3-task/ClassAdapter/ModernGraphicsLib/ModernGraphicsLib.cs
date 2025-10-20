namespace ObjectAdapter.ModernGraphicsLib
{
    public class Point
    {
        public double X { get; }
        public double Y { get; }

        public Point( double x, double y )
        {
            X = x;
            Y = y;
        }
    }

    public class RGBAColor
    {
        public double R { get; }
        public double G { get; }
        public double B { get; }
        public double A { get; }

        public RGBAColor( double r, double g, double b, double a )
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }

    public interface IModernGraphicsRenderer
    {
        void BeginDraw();
        void DrawLine( Point start, Point end, RGBAColor color );
        void EndDraw();
    }

    public class ModernGraphicsRenderer : IModernGraphicsRenderer
    {
        private bool _drawing = false;
        private const int _colorPrecision = 2;

        public void BeginDraw()
        {
            if ( _drawing )
            {
                throw new InvalidOperationException( "Drawing has already begun" );
            }

            Console.WriteLine( "<draw>" );
            _drawing = true;
        }

        public void DrawLine( Point start, Point end, RGBAColor color )
        {
            if ( !_drawing )
            {
                throw new InvalidOperationException( "DrawLine is allowed between BeginDraw()/EndDraw() only" );
            }

            Console.WriteLine(
                $"  <line fromX={start.X} fromY={start.Y} toX={end.X} toY={end.Y}>" +
                $"<color r={color.R.ToString( $"F{_colorPrecision}" )} " +
                $"g={color.G.ToString( $"F{_colorPrecision}" )} " +
                $"b={color.B.ToString( $"F{_colorPrecision}" )} " +
                $"a={color.A.ToString( $"F{_colorPrecision}" )}>" +
                $"</line>"
            );
        }

        public void EndDraw()
        {
            if ( !_drawing )
            {
                throw new InvalidOperationException( "Drawing has not been started" );
            }

            Console.WriteLine( "</draw>" );
            _drawing = false;
        }
    }
}