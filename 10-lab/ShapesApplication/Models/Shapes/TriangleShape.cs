using System.Windows;
using System.Windows.Media;
using ShapesApplication.Models.Shapes.Styles;

namespace ShapesApplication.Models
{
    public class TriangleShape : Shape
    {
        public TriangleShape( Rect frame, ShapeStyle style ) : base( ShapeType.Triangle, frame, style )
        {
        }

        public override Geometry GetGeometry()
        {
            Point[] points = GetTrianglePoints();
            StreamGeometry geometry = new StreamGeometry();

            using ( StreamGeometryContext context = geometry.Open() )
            {
                context.BeginFigure( points[ 0 ], true, true );
                context.LineTo( points[ 1 ], true, false );
                context.LineTo( points[ 2 ], true, false );
            }

            return geometry;
        }

        public override bool ContainsPoint( Point point )
        {
            Point[] points = GetTrianglePoints();
            return IsPointInTriangle( point, points[ 0 ], points[ 1 ], points[ 2 ] );
        }

        private Point[] GetTrianglePoints()
        {
            return new Point[]
            {
                new Point(Bounds.X + Bounds.Width / 2, Bounds.Y),
                new Point(Bounds.X, Bounds.Y + Bounds.Height),
                new Point(Bounds.X + Bounds.Width, Bounds.Y + Bounds.Height)
            };
        }

        private static bool IsPointInTriangle( Point p, Point a, Point b, Point c )
        {
            double area = 0.5 * ( -b.Y * c.X + a.Y * ( -b.X + c.X ) + a.X * ( b.Y - c.Y ) + b.X * c.Y );
            double s = 1 / ( 2 * area ) * ( a.Y * c.X - a.X * c.Y + ( c.Y - a.Y ) * p.X + ( a.X - c.X ) * p.Y );
            double t = 1 / ( 2 * area ) * ( a.X * b.Y - a.Y * b.X + ( a.Y - b.Y ) * p.X + ( b.X - a.X ) * p.Y );

            return s > 0 && t > 0 && ( 1 - s - t ) > 0;
        }
    }
}