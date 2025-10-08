using System.Globalization;
using PictureFactory.Shapes.Params;
using PictureFactory.Types;

namespace PictureFactory.Factories
{
    public class ShapeFactoryParser
    {
        private const int _rectangleParamsCount = 4;
        private const int _ellipseParamsCount = 4;
        private const int _triangleParamsCount = 6;
        private const int _regularPolygonParamsCount = 3;
        private const int _minParamsCount = 4;

        private Dictionary<string, ShapeType> _shapesStrMap = new(){
            {"rect", ShapeType.Rectangle},
            {"triangle", ShapeType.Triangle},
            {"reg-polygon", ShapeType.RegularPolygon},
            {"ellipse", ShapeType.Ellipse},
        };

        public ShapeParams ParseShapeParams( string descr )
        {
            if ( string.IsNullOrEmpty( descr ) )
            {
                throw new ArgumentNullException( "Shape description cannot be empty" );
            }

            string[] options = descr.Split( " " );
            if ( options.Length < _minParamsCount )
            {
                throw new ArgumentException( $"Shape options cannot be less than {_minParamsCount}" );
            }

            string shapeTypeStr = options[ 0 ];
            ShapeType shapeType = ParseShapeType( shapeTypeStr );
            Color color = ParseColor( options[ 1 ] );
            string[] shapeOptions = options.Skip( 2 ).ToArray();

            return shapeType switch
            {
                ShapeType.RegularPolygon => ParseRegularPolygonParams( shapeOptions, color ),
                ShapeType.Ellipse => ParseEllipseParams( shapeOptions, color ),
                ShapeType.Triangle => ParseTriangleParams( shapeOptions, color ),
                ShapeType.Rectangle => ParseRectangleParams( shapeOptions, color ),

                _ => throw new ArgumentException( $"Unknown shape type: {shapeType}" ),
            };
        }

        private RectangleParams ParseRectangleParams( string[] descr, Color color )
        {
            if ( descr.Length < _rectangleParamsCount )
            {
                throw new ArgumentException( $"Rectangle params count cannot be less than {_rectangleParamsCount}" );
            }

            Point leftTop = ParsePoint( descr[ 0 ], descr[ 1 ] );
            Point rightBottom = ParsePoint( descr[ 2 ], descr[ 3 ] );

            if ( leftTop.X >= rightBottom.X || leftTop.Y >= rightBottom.Y )
            {
                throw new ArgumentException( $"Invalid rectangle coordinates" );
            }

            return new RectangleParams( leftTop, rightBottom, color );
        }

        private TriangleParams ParseTriangleParams( string[] descr, Color color )
        {
            if ( descr.Length < _triangleParamsCount )
            {
                throw new ArgumentException( $"Triangle params count cannot be less than {_triangleParamsCount}" );
            }

            Point firstVertex = ParsePoint( descr[ 0 ], descr[ 1 ] );
            Point secondVertex = ParsePoint( descr[ 2 ], descr[ 3 ] );
            Point thirdVertex = ParsePoint( descr[ 4 ], descr[ 5 ] );

            return new TriangleParams( firstVertex, secondVertex, thirdVertex, color );
        }

        private RegularPolygonParams ParseRegularPolygonParams( string[] descr, Color color )
        {
            if ( descr.Length < _regularPolygonParamsCount )
            {
                throw new ArgumentException( $"Regular polygon params count cannot be less than {_regularPolygonParamsCount}" );
            }

            if ( !int.TryParse( descr[ 0 ], out int vertexCount ) || vertexCount < 3 )
            {
                throw new ArgumentException( "Invalid count of vertex!" );
            }

            Point center = ParsePoint( descr[ 1 ], descr[ 2 ] );
            double radius = ParseDouble( descr[ 3 ] );

            if ( radius < 0 )
            {
                throw new ArgumentException( "Radius cannot be less than 0" );
            }

            return new RegularPolygonParams( vertexCount, center, radius, color );
        }

        private EllipseParams ParseEllipseParams( string[] descr, Color color )
        {
            if ( descr.Length < _ellipseParamsCount )
            {
                throw new ArgumentException( $"Ellipse params count cannot be less than {_ellipseParamsCount}" );
            }

            Point center = ParsePoint( descr[ 0 ], descr[ 1 ] );
            double radiusX = ParseDouble( descr[ 2 ] );
            double radiusY = ParseDouble( descr[ 3 ] );

            if ( radiusX <= 0 || radiusY <= 0 )
            {
                throw new ArgumentException( "Radius cannot be less than 0" );
            }

            return new EllipseParams( center, radiusX, radiusY, color );
        }

        private ShapeType ParseShapeType( string typeStr )
        {
            bool isParsed = _shapesStrMap.TryGetValue( typeStr.Trim().ToLower(), out ShapeType type );

            if ( !isParsed )
            {
                throw new ArgumentException( $"Unknown shape type: {typeStr}" );
            }

            return type;
        }

        private Color ParseColor( string colorStr )
        {
            switch ( colorStr.ToLower().Trim() )
            {
                case "green":
                    return Color.Green;
                case "red":
                    return Color.Red;
                case "blue":
                    return Color.Blue;
                case "black":
                    return Color.Black;
                case "pink":
                    return Color.Pink;
                case "yellow":
                    return Color.Yellow;
                default:
                    throw new ArgumentException( $"Unknown color: {colorStr}" );
            }
        }

        private static Point ParsePoint( string x, string y )
        {
            return new Point( ParseDouble( x ), ParseDouble( y ) );
        }

        private static double ParseDouble( string value )
        {
            if ( double.TryParse( value, NumberStyles.Any, CultureInfo.InvariantCulture, out double result ) )
            {
                return result;
            }

            throw new Exception( $"{value} is not a valid number" );
        }
    }
}