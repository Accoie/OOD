namespace Shapes.ShapesManager.Commands;

public interface IShapeCommand
{
    void Execute( string[] args );
}