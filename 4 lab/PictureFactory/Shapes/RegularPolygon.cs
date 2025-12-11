using PictureFactory.Canvases;
using PictureFactory.Shapes.Params;
using PictureFactory.Types;

namespace PictureFactory.Shapes
{
    public class RegularPolygon : Shape
    {
        public int VertexCount { get; private set; }
        public Point Center { get; private set; }
        public double Radius { get; private set; }

        public RegularPolygon( RegularPolygonParams @params ) : base( @params.Color )
        {
            VertexCount = @params.VertexCount;
            Center = @params.Center;
            Radius = @params.Radius;
        }

        public override void Draw( ICanvas canvas )
        {
            if ( VertexCount < 3 )
                throw new InvalidOperationException( "Regular polygon must have at least 3 vertices" );

            canvas.SetColor( Color );

            Point[] vertices = CalculateVertices();

            for ( int i = 0; i < VertexCount; i++ )
            {
                Point from = vertices[ i ];
                Point to = vertices[ ( i + 1 ) % VertexCount ];
                canvas.DrawLine( from, to );
            }
        }

        private Point[] CalculateVertices()
        {
            Point[] vertices = new Point[ VertexCount ];
            double angleStep = 2 * Math.PI / VertexCount;

            for ( int i = 0; i < VertexCount; i++ )
            {
                double angle = i * angleStep;
                double x = Center.X + Radius * Math.Cos( angle );
                double y = Center.Y + Radius * Math.Sin( angle );
                vertices[ i ] = new Point( x, y );
            }

            return vertices;
        }

        public override ShapeType GetShapeType()
        {
            return ShapeType.RegularPolygon;
        }
    }
}