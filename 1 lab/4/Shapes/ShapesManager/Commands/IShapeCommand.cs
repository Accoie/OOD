namespace Shapes.ShapesManager.Commands;

public interface IShapeCommand
{
    CommandContext Context { get; }

    void Execute( string[] args );
}