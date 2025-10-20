using ObjectAdapter.GraphicsLib;

namespace ObjectAdapter.ShapeDrawingLib
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

    public interface ICanvasDrawable
    {
        void Draw( ICanvas canvas );
    }

    public class CTriangle : ICanvasDrawable
    {
        private readonly Point _firstVertex;
        private readonly Point _secondVertex;
        private readonly Point _thirdVertex;
        private readonly int _rgbColor;

        public CTriangle( Point p1, Point p2, Point p3, int rgbColor = 0 )
        {
            _firstVertex = p1;
            _secondVertex = p2;
            _thirdVertex = p3;
            _rgbColor = rgbColor;
        }

        public void Draw( ICanvas canvas )
        {
            canvas.SetColor( _rgbColor );
            canvas.MoveTo( _firstVertex.X, _firstVertex.Y );
            canvas.LineTo( _secondVertex.X, _secondVertex.Y );
            canvas.LineTo( _thirdVertex.X, _thirdVertex.Y );
            canvas.LineTo( _firstVertex.X, _firstVertex.Y );
        }
    }

    public class CRectangle : ICanvasDrawable
    {
        private readonly Point _leftTopPoint;
        private readonly double _width;
        private readonly double _height;
        private readonly int _rgbColor;

        public CRectangle( Point leftTopPoint, double width, double height, int rgbColor = 0 )
        {
            if ( width < 0 )
            {
                throw new ArgumentException( "Width can not be negative number" );
            }
            if ( height < 0 )
            {
                throw new ArgumentException( "Height can not be negative number" );
            }

            _leftTopPoint = leftTopPoint;
            _width = width;
            _height = height;
            _rgbColor = rgbColor;
        }

        public void Draw( ICanvas canvas )
        {
            canvas.SetColor( _rgbColor );
            canvas.MoveTo( _leftTopPoint.X, _leftTopPoint.Y );
            canvas.LineTo( _leftTopPoint.X + _width, _leftTopPoint.Y );
            canvas.LineTo( _leftTopPoint.X + _width, _leftTopPoint.Y + _height );
            canvas.LineTo( _leftTopPoint.X, _leftTopPoint.Y + _height );
            canvas.LineTo( _leftTopPoint.X, _leftTopPoint.Y );
        }
    }

    public class CanvasPainter
    {
        private readonly ICanvas _canvas;

        public CanvasPainter( ICanvas canvas )
        {
            _canvas = canvas;
        }

        public void Draw( ICanvasDrawable drawable )
        {
            drawable.Draw( _canvas );
        }
    }
}