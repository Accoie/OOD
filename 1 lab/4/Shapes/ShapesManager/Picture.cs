using System.Text;
using Shapes.Canvas;
using Shapes.ShapesManager.DrawingStrategy;

namespace Shapes.ShapesManager;

public class Picture
{
    private List<ShapeData> _shapes = new List<ShapeData>();
    private ICanvas _canvas;

    public Picture( ICanvas canvas )
    {
        _canvas = canvas;
    }

    public string GetShapesInfo()
    {
        StringBuilder stringBuilder = new StringBuilder();
        int i = 1;

        foreach ( var shapeData in _shapes )
        {
            stringBuilder.AppendLine( $"{i}. {shapeData.id} {shapeData.shape.GetInfo()} " );
            i++;
        }

        return stringBuilder.ToString();
    }

    public string GetShapeColorById( string id )
    {
        var shapeData = FindShapeDataById( id );

        if ( shapeData is null )
        {
            throw new Exception( "Shape with this id doesn't exist" );
        }

        return shapeData.shape.GetColor();
    }

    private ShapeData? FindShapeDataById( string id )
    {
        foreach ( var shapeTuple in _shapes )
        {
            if ( shapeTuple.id == id )
            {
                return shapeTuple;
            }
        }
        return null;
    }

    private Shape GetShapeById( string id )
    {
        var shapeData = FindShapeDataById( id );

        if ( shapeData is null )
        {
            throw new Exception( "Shape with this id doesn't exist" );
        }

        return shapeData.shape;
    }

    public void AddShape( string id, BaseDrawingStrategy drawingStrategy )
    {
        if ( FindShapeDataById( id ) is not null )
        {
            throw new Exception( "Shape with this id already exists" );
        }

        ShapeData shapeData = new ShapeData
        {
            id = id,
            shape = new Shape( drawingStrategy ),
            draw = false
        };

        _shapes.Add( shapeData );
    }

    public void DeleteShape( string id )
    {
        var shape = FindShapeDataById( id );

        if ( shape is null )
        {
            throw new Exception( "Shape with this id doesn't exist" );
        }

        _shapes.Remove( shape );
    }

    public void DrawPicture()
    {
        foreach ( var shapeData in _shapes )
        {
            shapeData.draw = true;
            DrawShapeCanvas( shapeData.shape );
        }
    }

    public void DrawShape( string id )
    {
        foreach ( var shapeData in _shapes )
        {
            if( shapeData.id == id )
            {
                shapeData.draw = true;
            }
            if ( shapeData.draw )
            {
                DrawShapeCanvas(shapeData.shape);
            }
        }
    }

    public void ChangeShape( string id, BaseDrawingStrategy newDrawingStrategy )
    {
        GetShapeById( id ).SetDrawingStrategy( newDrawingStrategy );
    }

    public void ChangeColor( string id, string newColor )
    {
        GetShapeById( id ).SetColor( newColor );
    }

    public void MoveShape( string id, double dx, double dy )
    {
        GetShapeById( id ).Move( _canvas, dx, dy );
        DrawShape( id );
    }

    public void MovePicture( double dx, double dy )
    {
        foreach ( var shapeData in _shapes )
        {
            shapeData.shape.Move( _canvas, dx, dy );

            if ( shapeData.draw )
            {
                DrawShapeCanvas( shapeData.shape );
            }
        }
    }

    private void DrawShapeCanvas( Shape shape )
    {
        shape.Draw( _canvas );
    }
}