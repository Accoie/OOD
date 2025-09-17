
using Shapes.ShapesManager.DrawingStrategy;

namespace Shapes.ShapesManager;

public class ShapesManager
{
    private Picture _picture;
    private ShapeParser shapeParser = new();

    public ShapesManager( Picture picture )
    {
        _picture = picture;
    }

    public void HandleCommandString( string commandStr )
    {
        string[] commandItems = commandStr.Split( ' ' );

        switch ( commandItems[ 0 ] )
        {
            case "AddShape":
                {
                    string id = commandItems[ 1 ];
                    string color = commandItems[ 2 ];
                    string[] unparsedParams = commandItems.Skip( 3 ).ToArray();
                    BaseDrawingStrategy drawingStrategy = shapeParser.Parse( unparsedParams, color );
                    _picture.AddShape( id, drawingStrategy );
                    break;
                }
            case "ChangeShape":
                {
                    string id = commandItems[ 1 ];
                    string[] unparsedParams = commandItems.Skip( 2 ).ToArray();

                    BaseDrawingStrategy drawingStrategy = shapeParser.Parse( unparsedParams, _picture.GetShapeColorById( id ) );
                    _picture.ChangeShape( id, drawingStrategy );
                    break;
                }
            case "ChangeColor":
                {
                    string id = commandItems[ 1 ];
                    string color = shapeParser.ValidateColor( commandItems[ 2 ] );
                    _picture.ChangeColor( id, color );
                    break;
                }
            case "DeleteShape":
                {
                    string id = commandItems[ 1 ];
                    _picture.DeleteShape( id );
                    break;
                }
            case "MoveShape":
                {
                    string id = commandItems[ 1 ];
                    double dx = shapeParser.ParseDouble(commandItems[ 2 ] );
                    double dy = shapeParser.ParseDouble( commandItems[ 3 ] );
                    _picture.MoveShape( id, dx, dy );
                    break;
                }
            case "MovePicture":
                {
                    double dx = shapeParser.ParseDouble( commandItems[ 1 ] );
                    double dy = shapeParser.ParseDouble( commandItems[ 2 ] );
                    _picture.MovePicture( dx, dy );
                    break;
                }
            case "DrawShape":
                {
                    string id = commandItems[ 1 ];
                    _picture.DrawShape( id );
                    break;
                }
            case "DrawPicture":
                _picture.DrawPicture();
                break;
            case "List":
                Console.WriteLine(_picture.GetShapesInfo());
                break;
        }
    }
}