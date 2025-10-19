using Editor.Documents;

namespace Editor.Commands;

public class InsertImageCommand : AbstractCommand
{
    private readonly string _path;
    private readonly int _width;
    private readonly int _height;
    private readonly int? _position;

    public InsertImageCommand( IDocument document, string path, int width, int height, int? position = null ) : base( document )
    {
        _path = path;
        _width = width;
        _height = height;
        _position = position;
    }

    protected override void DoExecute()
    {
        _document.InsertImage( _path, _width, _height, _position );
    }

    protected override void DoUnexecute()
    {
        _document.DeleteItem( _position );
    }
}