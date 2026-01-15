namespace ObjectAdapter.GraphicsLib
{
    public interface ICanvas
    {
        void MoveTo( double x, double y );
        void LineTo( double x, double y );
    }

    public class CCanvas : ICanvas
    {
        public void MoveTo( double x, double y )
        {
            Console.WriteLine( $"Move to ({x}, {y})" );
        }

        public void LineTo( double x, double y )
        {
            Console.WriteLine( $"Line to ({x}, {y})" );
        }
    }
}
