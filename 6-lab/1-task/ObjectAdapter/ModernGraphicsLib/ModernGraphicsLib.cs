namespace ObjectAdapter.ModernGraphicsLib
{
    public class Point
    {
        public Point( double x, double y )
        {
            X = x;
            Y = y;
        }

        public double X { get; }
        public double Y { get; }
    }

    public interface IModernGraphicsRenderer
    {
        void BeginDraw();
        void DrawLine( Point start, Point end );
        void EndDraw();
    }

    public class ModernGraphicsRenderer : IModernGraphicsRenderer
    {
        private bool _drawing = false;

        public void BeginDraw()
        {
            if ( _drawing )
            {
                throw new InvalidOperationException( "Drawing has already begun" );
            }

            Console.WriteLine( "<draw>" );
            _drawing = true;
        }

        public void DrawLine( Point start, Point end )
        {
            if ( !_drawing )
            {
                throw new InvalidOperationException( "DrawLine is allowed between BeginDraw()/EndDraw() only" );
            }

            Console.WriteLine( $"  <line fromX={start.X} fromY={start.Y} toX={end.X} toY={end.Y} />" );
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