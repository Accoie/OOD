using System.Globalization;
using System.Text.RegularExpressions;
using Shapes.ShapesManager.DrawingStrategy;
using Shapes.ShapesParams;
using Shapes.Types;

namespace Shapes.ShapesManager;

public class ShapeParser
{
    private const int _rectangleParamsLength = 4;
    private const int _circleParamsLength = 3;
    private const int _triangleParamsLength = 6;
    private const int _lineParamsLength = 4;
    private const int _textParamsLength = 4;

    private string _shapeColor = "ffffff";

    public RectangleParams ParseRectangleParams( string[] unparsedParams )
    {
        if ( unparsedParams.Length != _rectangleParamsLength )
        {
            throw new Exception( $"Params length for rectangle must be {_rectangleParamsLength}" );
        }

        var leftTop = new Point( ParseDouble( unparsedParams[ 0 ] ), ParseDouble( unparsedParams[ 1 ] ) );
        var width = ParseDouble( unparsedParams[ 2 ] );
        var height = ParseDouble( unparsedParams[ 3 ] );
        var rightBottom = new Point( leftTop.X + width, leftTop.Y + height );

        return new RectangleParams( leftTop, rightBottom, _shapeColor );
    }

    public CircleParams ParseCircleParams( string[] unparsedParams )
    {
        if ( unparsedParams.Length != _circleParamsLength )
        {
            throw new Exception( $"Params length for circle must be {_circleParamsLength}" );
        }

        var center = new Point( ParseInt( unparsedParams[ 0 ] ), ParseInt( unparsedParams[ 1 ] ) );
        var radius = ParseInt( unparsedParams[ 2 ] );

        return new CircleParams( center, radius, _shapeColor );
    }

    public TriangleParams ParseTriangleParams( string[] unparsedParams )
    {
        if ( unparsedParams.Length != _triangleParamsLength )
        {
            throw new Exception( $"Params length for triangle must be {_triangleParamsLength}" );
        }


        var firstVertex = new Point( ParseInt( unparsedParams[ 0 ] ), ParseInt( unparsedParams[ 1 ] ) );
        var secondVertex = new Point( ParseInt( unparsedParams[ 2 ] ), ParseInt( unparsedParams[ 3 ] ) );
        var thirdVertex = new Point( ParseInt( unparsedParams[ 4 ] ), ParseInt( unparsedParams[ 5 ] ) );

        return new TriangleParams( firstVertex, secondVertex, thirdVertex, _shapeColor );
    }

    public LineParams ParseLineParams( string[] unparsedParams )
    {
        if ( unparsedParams.Length != _lineParamsLength )
        {
            throw new Exception( $"Params length for line must be {_lineParamsLength}" );
        }

        var from = new Point( ParseDouble( unparsedParams[ 0 ] ), ParseDouble( unparsedParams[ 1 ] ) );
        var to = new Point( ParseDouble( unparsedParams[ 2 ] ), ParseDouble( unparsedParams[ 3 ] ) );

        return new LineParams( from, to, _shapeColor );
    }

    public TextParams ParseTextParams( string[] unparsedParams )
    {
        if ( unparsedParams.Length < _textParamsLength )
        {
            throw new Exception( $"Params length for text must be at least {_textParamsLength}" );
        }

        var leftTop = new Point( ParseDouble( unparsedParams[ 0 ] ), ParseDouble( unparsedParams[ 1 ] ) );
        var fontSize = ParseDouble( unparsedParams[ 2 ] );
        var text = string.Join( " ", unparsedParams.Skip( 3 ) );

        return new TextParams( leftTop, fontSize, text, _shapeColor );
    }

    public string ValidateIdParam( object possibleId ) 
    {
        if ( possibleId is not string id )
        {
            throw new Exception( "ID must be a string" );
        }

        return id;
    }

    public string ValidateColor( string possibleColor )
    {
        var hexColorRegex = new Regex( "^#?[0-9A-Fa-f]{6}$" );

        if ( !hexColorRegex.IsMatch( possibleColor ) )
        {
            throw new Exception( "Invalid hex color format. It should be in the format #RRGGBB." );
        }

        return possibleColor;
    }

    public int ValidateShiftParam( object possibleShiftParam )
    {
        if ( possibleShiftParam is string shiftParamStr )
        {
            if ( int.TryParse( shiftParamStr, out int parsedShiftParam ) )
            {
                return parsedShiftParam;
            }
            throw new Exception( "DX or DY must be valid number" );
        }

        throw new Exception( "DX or DY must exist" );
    }

    public BaseDrawingStrategy Parse( string[] parseParams, string color )
    {
        if ( parseParams.Length == 0 )
        {
            throw new Exception( "Params cannot be empty" );
        }

        _shapeColor = ValidateColor( color ).Replace( "#", "" );
        var shapeType = parseParams[ 0 ];
        var shapeParams = parseParams.Skip( 1 ).ToArray();

        return shapeType.ToLower() switch
        {
            "rectangle" => new RectangleDrawingStrategy( ParseRectangleParams( shapeParams ) ),
            "circle" => new CircleDrawingStrategy( ParseCircleParams( shapeParams ) ),
            "triangle" => new TriangleDrawingStrategy( ParseTriangleParams( shapeParams ) ),
            "text" => new TextDrawingStrategy( ParseTextParams(shapeParams ) ),
            "line" => new LineDrawingStrategy( ParseLineParams( shapeParams ) ),
            _ => throw new Exception( "Unknown type of shape" )
        };
    }

    private int ParseInt( string value )
    {
        if ( int.TryParse( value, out int result ) )
        {
            return result;
        }

        throw new Exception( $"'{value}' is not a valid number" );
    }

    public double ParseDouble( string value )
    {
        if ( double.TryParse( value, NumberStyles.Any, CultureInfo.InvariantCulture, out double result ) )
        { 
            return result;
        }

        throw new Exception( $"'{value}' is not a valid number" );
    }
}