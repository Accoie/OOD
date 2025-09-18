using System.Text;
using Shapes.Canvas;
using Shapes.ShapesManager.DrawingStrategy;

namespace Shapes.ShapesManager;

public class Picture
{
    private List<ShapeData> _shapes = new List<ShapeData>();
    private readonly ICanvas _canvas;

    public Picture( ICanvas canvas )
    {
        _canvas = canvas;
    }

    public string GetShapesInfo()
    {
        var stringBuilder = new StringBuilder();
        var i = 1;

        foreach ( var shapeData in _shapes )
        {
            stringBuilder.AppendLine( $"{i}. {shapeData.Id} {shapeData.Shape.GetInfo()} " );
            i++;
        }

        return stringBuilder.ToString();
    }

    public string GetShapeColorById( string id )
    {
        var shapeData = FindShapeDataByIdThrow( id );

        return shapeData.Shape.GetColor();
    }

    public void AddShape( string id, BaseDrawingStrategy drawingStrategy )
    {
        if ( FindShapeDataById( id ) is not null )
        {
            throw new Exception( "Shape with this id already exists" );
        }

        ShapeData shapeData = new ShapeData( id, new Shape( drawingStrategy ), false );

        _shapes.Add( shapeData );
    }

    public void DeleteShape( string id )
    {
        var shape = FindShapeDataByIdThrow( id );

        _shapes.Remove( shape );
    }

    public void DrawPicture()
    {
        foreach ( var shapeData in _shapes )
        {
            shapeData.Draw = true;
            DrawShapeCanvas( shapeData.Shape );
        }
    }

    public void DrawShape( string id )
    {
        var shapeData = FindShapeDataByIdThrow( id );

        if ( shapeData.Id == id )
        {
            shapeData.Draw = true;
        }

        RenderPicture();
    }

    public void ChangeShape( string id, BaseDrawingStrategy newDrawingStrategy )
    {
        GetShapeById( id ).SetDrawingStrategy( newDrawingStrategy );

        RenderPicture();
    }

    public void ChangeColor( string id, string newColor )
    {
        GetShapeById( id ).SetColor( newColor );

        RenderPicture();
    }

    public void MoveShape( string id, double dx, double dy )
    {
        GetShapeById( id ).Move( _canvas, dx, dy );

        RenderPicture();
    }

    public void MovePicture( double dx, double dy )
    {
        foreach ( var shapeData in _shapes )
        {
            shapeData.Shape.Move( _canvas, dx, dy );
        }

        RenderPicture();
    }

    private void RenderPicture()
    {
        foreach ( var shapeData in _shapes )
        {
            if ( shapeData.Draw )
            {
                DrawShapeCanvas( shapeData.Shape );
            }
        }
    }

    private void DrawShapeCanvas( Shape shape )
    {
        shape.Draw( _canvas );
    }

    private ShapeData FindShapeDataByIdThrow( string id )
    {
        var result = FindShapeDataById( id );

        if ( result == null )
        {
            throw new Exception( $"Shape with id \"{id}\" doesn't exist" );
        }

        return result;
    }

    private ShapeData? FindShapeDataById( string id )
    {
        foreach ( var shapeTuple in _shapes )
        {
            if ( shapeTuple.Id == id )
            {
                return shapeTuple;
            }
        }

        return null;
    }

    private Shape GetShapeById( string id )
    {
        var shapeData = FindShapeDataByIdThrow( id );

        return shapeData.Shape;
    }
}

public class ShapeData
{
    public string Id { get; }
    public Shape Shape { get; }
    public bool Draw { get; set; }

    public ShapeData( string id, Shape shape, bool draw )
    {
        Id = id;
        Shape = shape;
        Draw = draw;
    }
}